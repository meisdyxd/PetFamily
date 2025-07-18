using PetFamily.Application.VolunteerModule;
using PetFamily.Domain.VolunteerManagement;

namespace PetFamily.Infrastructure.Persistence.VolunteerModule;

public class VolunteerRepository: IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Create(
        Volunteer volunteer, 
        CancellationToken cancellationToken)
    {
        await _dbContext.Volunteers.AddAsync(volunteer);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }
}
