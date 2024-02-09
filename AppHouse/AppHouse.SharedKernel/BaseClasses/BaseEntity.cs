namespace AppHouse.SharedKernel.BaseClasses
{
    public class BaseEntity(Guid? id = null, DateTime? dateCreated = null, bool? isActive = null)
    {
        public Guid Id { get; set; } = id ?? Guid.NewGuid();
        public DateTime DateCreated { get; set; } = dateCreated ?? DateTime.UtcNow;
        public bool IsActive { get; set; } = isActive ?? true;
    }
}
