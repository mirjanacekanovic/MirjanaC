using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MirjanaC.Domain.Enums;
using MrijanaC.Application.Interfaces;
using MrijanaC.Application.Interfaces.Repositories;

namespace MirjanaC.Application.Employees.Commands
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        private readonly IMirjanaContext _context;
        private readonly ICompanyRepository _companyRepository;

        public CreateEmployeeCommandValidator(IMirjanaContext context,
            ICompanyRepository companyRepository)
        {
            _context = context;
            _companyRepository = companyRepository;

            RuleFor(c => c.Email)
                .NotEmpty()
                .MaximumLength(200)
                .MustAsync(BeUniqueTitle)
                    .WithMessage("'{PropertyName}' must be unique.")
                    .WithErrorCode("Unique");

            RuleFor(v => v)
                .MustAsync((x, cancellationToken) => BeUniqueInCompany(x.CompanyIds, x.Title, cancellationToken))
                    .WithMessage("Employee title must be unique within a company")
                    .WithErrorCode("Unique");
        }

        public async Task<bool> BeUniqueTitle(string email, CancellationToken cancellationToken)
        {
            return await _context.Employees
                .AllAsync(l => l.Email != email, cancellationToken);
        }

        public async Task<bool> BeUniqueInCompany(List<int> companyIds, Position position, CancellationToken cancellationToken)
        {
            if (companyIds.Any())
            {

                return await _companyRepository.GetByPosition(companyIds, position, cancellationToken);
            }
            else
                return false;
        }
    }
}
