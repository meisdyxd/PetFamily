using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.API.Requests.VolunteerRequests;

public record UpdateMainInfoRequest(
    FullnameDto Fullname,
    EmailDto Email,
    DescriptionDto Description,
    int EmployeeExperience,
    TelephoneNumberDto TelephoneNumber);
