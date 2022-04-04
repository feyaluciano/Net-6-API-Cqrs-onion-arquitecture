using FluentValidation;

namespace CQRS.BankAPI.Application.Features.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty()
                .MaximumLength(80);

            RuleFor(p => p.LastName)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(p => p.BirthDate).NotEmpty();

            RuleFor(p => p.PhoneNumber).NotEmpty()
                .Matches(@"^\d{4}-\d{4}$").MaximumLength(80);

            RuleFor(p => p.Email)
                .NotEmpty()
               .EmailAddress()
               .MaximumLength(100);

            RuleFor(p => p.Address)
            .NotEmpty()
           .MaximumLength(300);
        }
    }
}
