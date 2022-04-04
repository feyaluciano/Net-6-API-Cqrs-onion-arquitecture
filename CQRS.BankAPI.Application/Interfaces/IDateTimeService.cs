namespace CQRS.BankAPI.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }

    }
}
