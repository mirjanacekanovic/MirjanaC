namespace MirjanaC.Domain.Common.Interfaces
{
    public interface IAuditableEntity : IEntity
    {
        DateTime? CreatedDate { get; set; }
    }
}
