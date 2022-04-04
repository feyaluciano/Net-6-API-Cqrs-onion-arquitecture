using AutoMapper;
using CQRS.BankAPI.Application.DTOS;
using CQRS.BankAPI.Application.Interfaces;
using CQRS.BankAPI.Application.Specification;
using CQRS.BankAPI.Application.Wrappers;
using CQRS.BankAPI.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace CQRS.BankAPI.Application.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomerQuery : IRequest<PagedResponse<List<CustomerDTO>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public class GetAllCustomerQueryHandle : IRequestHandler<GetAllCustomerQuery, PagedResponse<List<CustomerDTO>>>
        {
            private readonly IRepositoryAsync<Customer> _repositoryAsync;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;
            public GetAllCustomerQueryHandle(IRepositoryAsync<Customer> repositoryAsync, IMapper mapper , IDistributedCache distributedCache)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }
            public async Task<PagedResponse<List<CustomerDTO>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
            {
                var cacheKey = $"customerList_ps{request.PageSize}_pn{request.PageNumber}_fn{request.FirstName}_ln{request.LastName}";
                string serializedCustomerList;
                List<Customer> customerList = new();
                
                var redisCustomerList = await _distributedCache.GetAsync(cacheKey);

                if (redisCustomerList != null)
                {
                    serializedCustomerList =Encoding.UTF8.GetString(redisCustomerList);
                    customerList = JsonSerializer.Deserialize<List<Customer>>(serializedCustomerList);

                }
                else
                {
                   customerList = await _repositoryAsync.ListAsync(new PagedCustomerSpecification(request.PageSize, request.PageNumber, request.FirstName, request.LastName));
                   serializedCustomerList = JsonSerializer.Serialize(customerList);
                   redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);

                    var opts = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                                                                 .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await _distributedCache.SetAsync(cacheKey,redisCustomerList,opts);
                }


                var customersdto = _mapper.Map<List<CustomerDTO>>(customerList);
                return new PagedResponse<List<CustomerDTO>>(customersdto, request.PageNumber, request.PageSize);
            }
        }
    }
}
