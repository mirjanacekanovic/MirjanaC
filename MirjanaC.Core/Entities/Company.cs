using MirjanaC.Domain.Common;

namespace MirjanaC.Domain.Entities
{
    public class Company : BaseAuditableEntity
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}
