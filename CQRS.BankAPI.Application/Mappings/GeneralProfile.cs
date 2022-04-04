using AutoMapper;
using CQRS.BankAPI.Application.DTOS;
using CQRS.BankAPI.Application.Features.Customers.Commands.CreateCustomerCommand;
using CQRS.BankAPI.Domain.Entities;

namespace CQRS.BankAPI.Application.Mappings
{
    public  class GeneralProfile:Profile
    {
        public GeneralProfile()
        {
            #region Commands
            CreateMap<CreateCustomerCommand, Customer>();
            #endregion
            #region DTOs
            CreateMap<Customer, CustomerDTO>();
            #endregion
        }
    }
}
