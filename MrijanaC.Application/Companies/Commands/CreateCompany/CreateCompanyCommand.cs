using MediatR;
using MirjanaC.Application.Employees.Commands;
using MirjanaC.Domain.Entities;
using MirjanaC.Domain.Events;
using MrijanaC.Application.Common.Mappings;
using MrijanaC.Application.Interfaces.Repositories;

namespace MrijanaC.Application.Companies.Commands.CreateCompany
{
    public record CreateCompanyCommand : IRequest<int>, IMapFrom<Company>
    {
        public string Name { get; set; } = string.Empty;
        public List<CreateEmployeeCommand> Employees { get; set; } = new();
    }

    internal class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyRepository _companyRepository;


        public CreateCompanyCommandHandler(IUnitOfWork unitOfWork, 
            ICompanyRepository companyRepository)
        {
            _unitOfWork = unitOfWork;
            _companyRepository = companyRepository;
        }

        public async Task<int> Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
        {
            var company = new Company()
            {
                Name = command.Name,
            };
            foreach (var item in command.Employees)
            {
                Employee employee = await _companyRepository.CreateEmployee(item);
                company.Employees.Add(employee);
            }

            company.DomainEvents.Add(new CompanyCreatedEvent(company));
            await _unitOfWork.Repository<Company>().AddAsync(company);

            await _unitOfWork.Save(cancellationToken);

            return company.Id;
        }
    }
}
