using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Contracts.VolunteerContracts.Request;

public record UpdateMainInfoRequest(
    FullnameDto Fullname,
    EmailDto Email,
    DescriptionDto Description,
    int EmployeeExperience,
    TelephoneNumberDto TelephoneNumber);
