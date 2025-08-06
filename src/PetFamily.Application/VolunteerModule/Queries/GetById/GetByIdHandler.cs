using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Extensions;
using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Contracts.Contracts;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.Queries.GetById;

public class GetByIdHandler : IQueryHandler<GetByIdQuery, VolunteerDto>
{
    private readonly IReadDbContext _readDbContext;

    public GetByIdHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<VolunteerDto, ErrorResult>> Handle(
        GetByIdQuery query, 
        CancellationToken cancellationToken)
    {
        var volunteer = await _readDbContext.Volunteers
            .FirstOrDefaultAsync(v => v.Id == query.Id, cancellationToken);
        if(volunteer is null)
            return Errors.General.RecordNotFound(query.Id);
        return volunteer;
    }
}