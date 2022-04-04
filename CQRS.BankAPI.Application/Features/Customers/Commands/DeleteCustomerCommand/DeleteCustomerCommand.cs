using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Wrappers;
using CQRS.BankAPI.Domain.Entities;
using MediatR;

namespace CQRS.BankAPI.Application.Features.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerCommand :IRequest<Response<int>>
    {
     public int Id { get; set; }


    }
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Customer> _repositoryAsync;
        public DeleteCustomerCommandHandler(IRepositoryAsync<Customer> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
         
        }

     
      
        public async Task<Response<int>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new KeyNotFoundException($"No records found with Id: {request.Id}");
            }

            await _repositoryAsync.DeleteAsync(record);

            return new Response<int>(record.Id);
        }
    }

}
