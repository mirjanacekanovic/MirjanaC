using Microsoft.EntityFrameworkCore;
using MirjanaC.Domain.Entities;

namespace MrijanaC.Application.Interfaces
{
    public interface IMirjanaContext
    {
        DbSet<Company> Companies { get; }
        DbSet<Employee> Employees { get; }
        DbSet<SystemLog> SystemLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
