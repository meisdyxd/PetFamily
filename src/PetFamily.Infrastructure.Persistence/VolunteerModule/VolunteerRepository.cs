using Microsoft.EntityFrameworkCore;
using PetFamily.Application.VolunteerModule;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Infrastructure.Persistence.Contexts;

namespace PetFamily.Infrastructure.Persistence.VolunteerModule;

public class VolunteerRepository: IVolunteerRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteerRepository(WriteDbContext dbContext)
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

    public async Task<Volunteer?> GetById(
        Guid id, 
        CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);

        return volunteer;
    }

    public async Task Save(
        Volunteer volunteer,
        CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Attach(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(
        Volunteer volunteer, 
        CancellationToken cancellationToken)
    {
        _dbContext.Volunteers.Remove(volunteer);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
