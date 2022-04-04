using CQRS.BankAPI.Application.DTOS.Response;
using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Wrappers;
using MediatR;

namespace CQRS.BankAPI.Application.Features.Authenticate.Command.AuthenticateCommand
{
    public class AuthenticateCommand:IRequest<Response<AuthenticationResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IpAddress { get; set; }

        public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, Response<AuthenticationResponse>>
        {
            private readonly IAccountService _accountService;
            public AuthenticateHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }
            public async Task<Response<AuthenticationResponse>> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
            {
                return await _accountService.AuthenticateAsync(new DTOS.Request.AuthenticationRequest
                {
                    Email = request.Email,
                    Password = request.Password,


                },request.IpAddress);
            }
        }
    }
}
