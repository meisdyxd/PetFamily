using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.API.Requests.VolunteerRequests;

public record CreateVolunteerRequest(
    FullnameDto Fullname,
    EmailDto Email,
    DescriptionDto Description,
    int EmployeeExperience,
    TelephoneNumberDto TelephoneNumber,
    IEnumerable<SocialNetworkDto>? SocialNetworks = null,
    IEnumerable<RequisitDto>? Requisits = null);
