using Microsoft.EntityFrameworkCore;
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
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Volunteer?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return volunteer;
    }

    public async Task Save(
        Volunteer volunteer,
        CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
