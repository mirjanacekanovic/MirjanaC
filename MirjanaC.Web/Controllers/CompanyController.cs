using MediatR;
using Microsoft.AspNetCore.Mvc;
using MrijanaC.Application.Companies.Commands.CreateCompany;

namespace MirjanaC.Web.Controllers
{
    public class CompanyController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateCompanyCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
