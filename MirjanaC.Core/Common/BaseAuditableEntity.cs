using MirjanaC.Domain.Common.Interfaces;

namespace MirjanaC.Domain.Common
{
    public abstract class BaseAuditableEntity : BaseEntity, IAuditableEntity
    {
        public DateTime? CreatedDate { get; set; }
    }
}
