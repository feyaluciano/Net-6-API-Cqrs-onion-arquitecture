using CQRS.BankAPI.Application.Features.Customers.Commands.CreateCustomerCommand;
using CQRS.BankAPI.Application.Features.Customers.Commands.DeleteCustomerCommand;
using CQRS.BankAPI.Application.Features.Customers.Commands.UpdateCustomerCommand;
using CQRS.BankAPI.Application.Features.Customers.Queries.GetAllCustomers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.BankAPI.WebAPI.Controllers.v1
{
    [ApiVersion("1")]
    public class CustomerController : BaseApiController
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        //GET api/<controller>

        [HttpGet]
        [Authorize(Policy = "Admin")]

        public async Task<IActionResult> GetAll([FromQuery] GetAllCustomerParameter filters)
        {
            var res = await Mediator!.Send(new GetAllCustomerQuery
            {
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                FirstName = filters.FirstName,
                LastName = filters.LastName,
            });
            return Ok(res);
        }


        //GET api/<controller>/id

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await Mediator!.Send(new GetCustomerByIdQuery
            {
                Id = id
            });
            return Ok(res);
        }


        //POST api/<controller>

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateCustomerCommand command) 
        {
            var res = await Mediator!.Send(command);
            return Ok(res);
        }
        //PUT api/<controller>

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(int id,UpdateCustomerCommand command)
        {
            if(id != command.Id)
                return BadRequest();

            var res = await Mediator!.Send(command);
            return Ok(res);
        }

        //DELETE api/<controller>

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize (Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
           
            var res = await Mediator!.Send(new DeleteCustomerCommand { Id = id});
            return Ok(res);
        }
    }
}
