using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Shared;

public abstract class SoftDeletableEnity<TId>: Entity<TId>
    where TId : IComparable<TId>
{
    protected SoftDeletableEnity() { }

    protected SoftDeletableEnity(TId id): base(id) { }

    public bool IsDeleted { get; set; }
    public bool DeleteByCascade { get; set; }
    public DateTime? DeletionDate { get; set; }

    public virtual void Delete(DateTime? deletionDate = null, bool cascade = false)
    {
        IsDeleted = true;
        DeletionDate = deletionDate ?? DateTime.UtcNow;
        DeleteByCascade = cascade;
    }

    public virtual void Restore(bool innnerCascadeDeleted = false)
    {
        IsDeleted = false;
        DeletionDate = null;
        DeleteByCascade = false;
    }
}
