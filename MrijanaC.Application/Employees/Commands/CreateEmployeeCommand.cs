using MediatR;
using MirjanaC.Domain.Entities;
using MirjanaC.Domain.Enums;
using MirjanaC.Domain.Events;
using MrijanaC.Application.Common.Mappings;
using MrijanaC.Application.Interfaces.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MirjanaC.Application.Employees.Commands
{
    public record CreateEmployeeCommand : IRequest<int>, IMapFrom<Employee>
    {
        public Position Title { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<int> CompanyIds { get; set; } = new();
    }

    internal class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;

        public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork, 
            ICompanyRepository companyRepository)
        {
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<int> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = _companyRepository.CreateEmployee(command);

            //employee.DomainEvents.Add(new EmployeeCreatedEvent(employee));
            await _unitOfWork.Save(cancellationToken);

            return employee.Id;
        }
    }

}
