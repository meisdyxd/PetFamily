using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Application.VolunteerModule;

public interface IVolunteerRepository
{
    Task<Guid> Create(Volunteer volunteer, CancellationToken cancellationToken);
}
