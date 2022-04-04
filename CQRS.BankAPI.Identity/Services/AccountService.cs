using CQRS.BankAPI.Application.DTOS.Request;
using CQRS.BankAPI.Application.DTOS.Response;
using CQRS.BankAPI.Application.Enums;
using CQRS.BankAPI.Application.Exceptions;
using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Wrappers;
using CQRS.BankAPI.Domain.Settings;
using CQRS.BankAPI.Identity.Helpers;
using CQRS.BankAPI.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CQRS.BankAPI.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSetting _jwtSettign;
        private readonly IDateTimeService _dateTimeService;

        public AccountService(UserManager<ApplicationUser> userManager, IOptions<JwtSetting> jwtSettign, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _jwtSettign = jwtSettign.Value;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string IpAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {

                throw new ApiException($"An account with the email {request.Email} was not found");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);
            if(!result.Succeeded)
            {
                throw new ApiException($"The user credentials is not valid.");
            }
            JwtSecurityToken jwtToken = await GenerateToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var roleList = await _userManager.GetRolesAsync(user);
            response.Roles = roleList.ToList();
            response.IsVerified = true;
            var refreshToken =  GenerateRefreshToken(IpAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response,$" User {user.UserName} is authenticated.");
        }

        public async  Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if(userWithSameUserName != null)
            {

                throw new ApiException($"The username {request.UserName} has been registered.");
            }
            var user = new ApplicationUser()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed=true,
                PhoneNumberConfirmed=true

            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {

                throw new ApiException($"The email {request.UserName} has been registered.");
            }
            var result = await _userManager.CreateAsync(user, request.Password);
            
            if(!result.Succeeded)
            {
                throw new ApiException($"{result.Errors}");

            }
            await _userManager.AddToRoleAsync(user, RolesEnum.Basic.ToString());
            return new Response<string>("User created successfully");

        }


        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
           var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {

                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var ipAddress = IpHelper.GetIpAddress();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim("uid",user.Id),
                new Claim("ip",ipAddress),
            }.Union(userClaims).Union(roleClaims);
            var symmKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettign.Key));
            var signignCredentials = new SigningCredentials(symmKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(
                issuer:_jwtSettign.Issuer,
                audience:_jwtSettign.Audience,
                claims:claims,
                expires:DateTime.Now.AddMinutes(_jwtSettign.DurationInMinutes),
                signingCredentials:signignCredentials);
            return jwtToken;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(3),
                Created = DateTime.Now,
                CreatedByIp = ipAddress,
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomByte = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomByte);
            return BitConverter.ToString(randomByte).Replace("-", string.Empty);
        }
    }
}
