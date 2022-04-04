using CQRS.BankAPI.Application.Parameters;

namespace CQRS.BankAPI.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomerParameter :  RequestParameter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
