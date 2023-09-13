using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MirjanaC.Domain.Entities;

namespace MirjanaC.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.HasIndex(t => t.Email)
                    .IsUnique();
            builder.Property(t => t.Email)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
