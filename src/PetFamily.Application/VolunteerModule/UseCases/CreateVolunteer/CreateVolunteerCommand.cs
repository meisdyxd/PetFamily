using PetFamily.Application.Interfaces;
using PetFamily.Contracts.VolunteerContracts.DTOs;
using PetFamily.Contracts.VolunteerContracts.Response;

namespace PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;

public record CreateVolunteerCommand(
    FullnameDto Fullname,
    EmailDto Email,
    DescriptionDto Description,
    int EmployeeExperience,
    TelephoneNumberDto TelephoneNumber,
    IEnumerable<SocialNetworkDto>? SocialNetworks = null,
    IEnumerable<RequisitDto>? Requisits = null) : ICommand<CreateVolunteerResponse>;