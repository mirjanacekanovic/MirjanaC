using MirjanaC.Domain.Common;
using MirjanaC.Domain.Enums;

namespace MirjanaC.Domain.Entities
{
    public class Employee : BaseAuditableEntity
    {
        public Position Title { get; set; }
        public string Email { get; set; }
        public List<Company> Companies { get; set; } = new();
    }
}
