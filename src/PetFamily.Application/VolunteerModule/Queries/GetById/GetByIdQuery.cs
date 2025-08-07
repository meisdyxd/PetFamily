using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Contracts.Contracts;

namespace PetFamily.Application.VolunteerModule.Queries.GetById;

public class GetByIdQuery: IQuery<VolunteerDto>
{
    public GetByIdQuery(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; set; }
}