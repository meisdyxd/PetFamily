using PetFamily.Application.VolunteerModule.UseCases;
using PetFamily.Contracts.VolunteerContracts.Request;

namespace PetFamily.Contracts.VolunteerContracts.Extensions;

public static class RequestExtensions
{
    public static CreateVolunteerCommand ToCommand(this CreateVolunteerRequest request)
    {
        return new(
            request.Surname,
            request.Name,
            request.Patronymic,
            request.Email,
            request.Description,
            request.EmployeeExperience,
            request.TelephoneNumber,
            request.SocialNetworks,
            request.Requisits);
    }
}
