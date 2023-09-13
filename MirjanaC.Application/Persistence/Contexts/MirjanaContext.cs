using Microsoft.EntityFrameworkCore;
using MirjanaC.Application.Interfaces;
using MirjanaC.Domain.Common;
using MirjanaC.Domain.Entities;
using MrijanaC.Application.Interfaces;
using System.Reflection;

namespace MirjanaC.Persistence.Contexts
{
    public class MirjanaContext : DbContext, IMirjanaContext
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IDomainEventService _domainEventService;
        public MirjanaContext(DbContextOptions<MirjanaContext> options,
          IDateTimeService dateTimeService,
          IDomainEventService domainEventService)
            : base(options)
        {
            _dateTimeService = dateTimeService;
            _domainEventService = domainEventService;
        }

        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<SystemLog> SystemLogs => Set<SystemLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTimeService.NowUtc;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModified = _dateTimeService.NowUtc;
                        break;
                }
            }

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}

