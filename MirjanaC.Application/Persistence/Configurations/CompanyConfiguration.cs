using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MirjanaC.Domain.Entities;

namespace MirjanaC.Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.HasIndex(t => t.Name)
                    .IsUnique();
            builder.Property(t => t.Name)
                .HasMaxLength(250)
                .IsRequired();
        }
    }
}
