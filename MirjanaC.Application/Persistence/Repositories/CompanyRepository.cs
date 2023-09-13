using Microsoft.EntityFrameworkCore;
using MirjanaC.Application.Employees.Commands;
using MirjanaC.Domain.Entities;
using MirjanaC.Domain.Enums;
using MrijanaC.Application.Interfaces.Repositories;

namespace MirjanaC.Persistence.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IGenericRepository<Company> _repository;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyRepository(IGenericRepository<Company> repository, 
            IGenericRepository<Employee> employeeRepository, 
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Company> Get(List<int> companyIds)
        {
            List<Company> companies = _repository.Entities
                .Where(x => companyIds.Contains(x.Id))
                .ToList();
            return companies;
        }

        public async Task<bool> GetByPosition(List<int> companyIds, Position position, CancellationToken cancellationToken)
        {
            return await _repository.Entities
                .AllAsync(c => !(companyIds.Contains(c.Id) && c.Employees.Any(e => e.Title == position)), cancellationToken);
        }

        public async Task<Employee> CreateEmployee(CreateEmployeeCommand command)
        {
            Employee? employee = _employeeRepository.Entities.FirstOrDefault(e => e.Email == command.Email);
            if (employee != null)
                return employee;

            employee = new Employee()
            {
                Title = command.Title,
                Email = command.Email,
                Companies = Get(command.CompanyIds)
            };
            await _unitOfWork.Repository<Employee>().AddAsync(employee);

            return employee;
        }

        public async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
        {
            return await _repository.Entities
                .AllAsync(l => l.Name != name, cancellationToken);
        }
    }
}
