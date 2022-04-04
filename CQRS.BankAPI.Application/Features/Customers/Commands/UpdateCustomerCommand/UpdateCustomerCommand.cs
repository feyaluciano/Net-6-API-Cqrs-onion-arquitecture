using AutoMapper;
using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Wrappers;
using CQRS.BankAPI.Domain.Entities;
using MediatR;

namespace CQRS.BankAPI.Application.Features.Customers.Commands.UpdateCustomerCommand
{
    public class UpdateCustomerCommand :IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string FirtsName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


    }
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Customer> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateCustomerCommandHandler(IRepositoryAsync<Customer> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

     
      
        public async Task<Response<int>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var record = await _repositoryAsync.GetByIdAsync(request.Id);
            if (record == null)
            {
                throw new KeyNotFoundException($"No records found with Id: {request.Id}");
            }

            record.FirtsName = request.FirtsName;
            record.LastName = request.LastName;
            record.BirthDate = request.BirthDate;
            record.PhoneNumber = request.PhoneNumber;
            record.Email = request.Email;
            record.Address = request.Address;

             await _repositoryAsync.UpdateAsync(record);

            return new Response<int>(record.Id);
        }
    }

}
