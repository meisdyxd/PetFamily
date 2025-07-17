using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Contracts.VolunteerContracts.Request;

public record CreateVolunteerRequest(
    string Surname,
    string Name,
    string? Patronymic,
    string Email,
    string Description,
    int EmployeeExperience,
    string TelephoneNumber,
    List<SocialNetworkDto>? SocialNetworks = null,
    List<RequisitDto>? Requisits = null);
