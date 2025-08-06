using PetFamily.Application.VolunteerModule.Queries.GetWithPagination;
using PetFamily.Application.VolunteerModule.UseCases.AddPetToVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateMainInfoVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;
using PetFamily.Contracts.Contracts;

namespace PetFamily.API.Requests.VolunteerRequests;

public static class RequestExtensions
{
    public static CreateVolunteerCommand ToCommand(this CreateVolunteerRequest request)
    {
        return new(
            request.Fullname,
            request.Email,
            request.Description,
            request.EmployeeExperience,
            request.TelephoneNumber,
            request.SocialNetworks,
            request.Requisits);
    }

    public static UpdateMainInfoCommand ToCommand(
        this UpdateMainInfoRequest request,
        Guid Id)
    {
        return new(
            Id,
            request.Fullname,
            request.Email,
            request.Description,
            request.EmployeeExperience,
            request.TelephoneNumber);
    }

    public static UpdateRequisitsCommand ToCommand(
        this UpdateRequisitsRequest request,
        Guid Id)
    {
        return new(
            Id,
            request.Requisits);
    }

    public static UpdateSocialNetworksCommand ToCommand(
        this UpdateSocialNetworksRequest request,
        Guid Id)
    {
        return new(
            Id,
            request.SocialNetworks);
    }

    public static AddPetCommand ToCommand(
        this AddPetRequest request, 
        Guid Id)
    {
        return new(
            Id,
            request.Moniker,
            request.Species,
            request.Description,
            request.Breed,
            request.Coloration,
            request.HealthInfo,
            request.Address,
            request.Weight,
            request.Height,
            request.OwnerTelephoneNumber,
            request.IsCastrated,
            request.BirthDate,
            request.IsVaccinated);
    }

    public static GetWithPaginationQuery ToQuery(
        this GetWithPaginationRequest request,
        PaginatedRequest paginatedRequest)
    {
        return new(
            paginatedRequest.Page, 
            paginatedRequest.PageSize);
    }
}
