using PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateMainInfoVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;
using PetFamily.Contracts.VolunteerContracts.Request;

namespace PetFamily.Contracts.VolunteerContracts.Extensions;

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
}
