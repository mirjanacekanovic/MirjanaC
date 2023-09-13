using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MirjanaC.Domain.Entities;

namespace MirjanaC.Infrastructure.Persistence.Configurations
{
    public class SystemLogConfiguration : IEntityTypeConfiguration<SystemLog>
    {
        public void Configure(EntityTypeBuilder<SystemLog> builder)
        {
            builder.Ignore(e => e.DomainEvents);

        }
    }
}
