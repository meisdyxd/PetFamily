using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Extensions;
using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Contracts.Contracts;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.Queries.GetWithPagination;

public class GetWithPaginationHandler : IQueryHandler<GetWithPaginationQuery, PaginatedResponse<VolunteerDto>>
{
    private readonly IReadDbContext _readDbContext;

    public GetWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<Result<PaginatedResponse<VolunteerDto>, ErrorResult>> Handle(
        GetWithPaginationQuery query, 
        CancellationToken cancellationToken)
    {
        var queryVolunteer = _readDbContext.Volunteers;

        var totalItems = await queryVolunteer.CountAsync(cancellationToken);
        
        var paginatedVolunteers = await queryVolunteer
            .AsPaginated(query.PageSize, query.Page)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        var response = new PaginatedResponse<VolunteerDto>
        {
            Items = paginatedVolunteers,
            PageSize = query.PageSize,
            CurrentPage = query.Page,
            TotalItems = totalItems
        };
        return response;
    }
}