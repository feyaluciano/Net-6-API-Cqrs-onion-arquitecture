using CQRS.BankAPI.Application.Interfaces;

namespace CQRS.BankAPI.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
