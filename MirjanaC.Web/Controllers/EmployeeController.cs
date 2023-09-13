using MediatR;
using Microsoft.AspNetCore.Mvc;
using MirjanaC.Application.Employees.Commands;

namespace MirjanaC.Web.Controllers
{
    public class EmployeeController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateEmployeeCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
