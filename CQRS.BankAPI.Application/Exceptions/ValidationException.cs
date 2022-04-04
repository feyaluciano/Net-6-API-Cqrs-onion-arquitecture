using FluentValidation.Results;

namespace CQRS.BankAPI.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; private set; }
        public ValidationException() : base("Se han producido uno o más errores de validación ")
        {
            Errors = new();
        }
        public ValidationException(IEnumerable<ValidationFailure> failures): this()
        {
            Errors.AddRange(failures.Select(error => error.ErrorMessage));
        }

    }
}
