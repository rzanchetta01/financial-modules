﻿namespace AppHouse.SharedKernel.BaseClasses
{
    public class BaseEntity(Guid? id = null, DateTime? dateCreated = null, bool? isActive = null)
    {
        public Guid Id { get; private set; } = id ?? Guid.NewGuid();
        public DateTime DateCreated { get; private set; } = dateCreated ?? DateTime.UtcNow;
        public bool IsActive { get; private set; } = isActive ?? true;
    }
}
