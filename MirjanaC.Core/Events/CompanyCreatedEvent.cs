using MirjanaC.Domain.Common;
using MirjanaC.Domain.Entities;

namespace MirjanaC.Domain.Events
{
    public class CompanyCreatedEvent : DomainEvent
    {
        public CompanyCreatedEvent(Company company)
        {
            Company = company;
        }

        public Company Company { get; }
    }
}
