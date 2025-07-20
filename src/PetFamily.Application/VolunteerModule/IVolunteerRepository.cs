using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Application.VolunteerModule;

public interface IVolunteerRepository
{
    Task<Guid> Create(Volunteer volunteer, CancellationToken cancellationToken);
    Task<Volunteer?> GetById(Guid id, CancellationToken cancellationToken);
    Task Save(Volunteer volunteer, CancellationToken cancellationToken);
    Task Delete(Volunteer volunteer, CancellationToken cancellationToken);
}
