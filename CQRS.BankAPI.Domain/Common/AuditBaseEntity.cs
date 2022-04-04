namespace CQRS.BankAPI.Domain.Common
{
    public abstract class AuditBaseEntity
    {
        public virtual int Id { get; set; }
        public virtual string? CreatedBy { get; set; }
        public virtual DateTime? CreatedOn { get; set; }
        public virtual string? LastModifiedBy { get; set; }
        public virtual DateTime? LastModified { get; set; }
    }
}
