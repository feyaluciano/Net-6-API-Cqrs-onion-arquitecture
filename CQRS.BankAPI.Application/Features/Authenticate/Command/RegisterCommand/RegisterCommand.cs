using CQRS.BankAPI.Application.DTOS.Request;
using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Wrappers;
using MediatR;

namespace CQRS.BankAPI.Application.Features.Authenticate.Command.RegisterCommand
{
    public class RegisterCommand : IRequest<Response<string>>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Origin { get; set; }
        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<string>>
        {
            private readonly IAccountService _accountService;

            public RegisterCommandHandler(IAccountService accountService)
            {
                _accountService = accountService;
            }
            public async Task<Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                return await _accountService.RegisterAsync(new RegisterRequest{
                Email = request.Email,
                Password = request.Password,
                ConfirmPassword= request.ConfirmPassword,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                },request.Origin);
            }
        }
    }
}
