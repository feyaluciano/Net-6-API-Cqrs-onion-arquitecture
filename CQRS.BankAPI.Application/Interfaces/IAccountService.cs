using CQRS.BankAPI.Application.DTOS.Request;
using CQRS.BankAPI.Application.DTOS.Response;
using CQRS.BankAPI.Application.Wrappers;

namespace CQRS.BankAPI.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string IpAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);

    }
}
