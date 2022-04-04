using FluentValidation;

namespace CQRS.BankAPI.Application.Features.Authenticate.Command.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.FirstName)
               .NotEmpty()
               .MaximumLength(80);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .MaximumLength(200);
          
            RuleFor(p => p.Email)
                .NotEmpty()
               .EmailAddress()
               .MaximumLength(100);

            RuleFor(p => p.UserName)
                .NotEmpty()
               .EmailAddress()
               .MaximumLength(100);

            RuleFor(p => p.Password)
             .NotEmpty()
            .EmailAddress()
            .MaximumLength(20);

            RuleFor(p => p.ConfirmPassword)
           .Equal(p => p.Password);
        }
    }
}
