using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Contracts.Contracts;

namespace PetFamily.Application.VolunteerModule.Queries.GetWithPagination;

public class GetWithPaginationQuery: IQuery<PaginatedResponse<VolunteerDto>>
{
    public GetWithPaginationQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
    
    public int Page { get; set; }
    public int PageSize { get; set; }
}