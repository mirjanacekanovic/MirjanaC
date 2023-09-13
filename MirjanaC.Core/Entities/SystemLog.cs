using MirjanaC.Domain.Common;

namespace MirjanaC.Domain.Entities
{
    public class SystemLog : BaseAuditableEntity
    {
        public string ResourceType { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;

        public string ResourceAttributes { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}
