using MirjanaC.Application.Employees.Commands;
using MirjanaC.Domain.Entities;
using MirjanaC.Domain.Enums;

namespace MrijanaC.Application.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        public List<Company> Get(List<int> companyIds);
        public Task<bool> GetByPosition(List<int> companyIds, Position position, CancellationToken cancellationToken);
        public Task<Employee> CreateEmployee(CreateEmployeeCommand command);
        public Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken);
    }
}
