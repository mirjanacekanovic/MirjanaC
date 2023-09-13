using FluentValidation;
using MirjanaC.Application.Employees.Commands;
using MrijanaC.Application.Interfaces.Repositories;

namespace MrijanaC.Application.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CreateCompanyCommandValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;

            RuleFor(v => v.Name)
                .NotEmpty()
                .MaximumLength(200)
                .MustAsync(BeUniqueName)
                    .WithMessage("'{PropertyName}' must be unique.")
                    .WithErrorCode("Unique");

            RuleFor(c => c.Employees)
                .MustAsync(BeUniqueTitle)
                    .WithMessage("Employee title must be unique within a company")
            .WithErrorCode("Unique");
        }

        public async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return await _companyRepository.BeUniqueTitle(name, cancellationToken);
        }

        public async Task<bool> BeUniqueTitle(List<CreateEmployeeCommand> createEmployeeCommands, CancellationToken cancellationToken)
        {
            var x = createEmployeeCommands
                .GroupBy(o => o.Title)
                .Where(g => g.Count() > 1)
                .Select(g => new { title = g.Key, nrOfOrders = g.Count() });
            var y = x.Any();
            if (y)
                return await Task.FromResult(false);
            else
                return await Task.FromResult(true);

        }
    }
}
