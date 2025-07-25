﻿namespace Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }

    public long? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public long? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = default;
    public bool IsActive { get; set; } = default;
}
