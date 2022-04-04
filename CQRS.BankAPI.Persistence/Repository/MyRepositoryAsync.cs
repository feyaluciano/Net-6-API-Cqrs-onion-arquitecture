using Ardalis.Specification.EntityFrameworkCore;
using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Persistence.Contexts;

namespace CQRS.BankAPI.Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>,IRepositoryAsync<T>,IReadRepositoryAsync<T> where T :class
    {
        public readonly AppBankDbContext dbContext;

        public MyRepositoryAsync(AppBankDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
