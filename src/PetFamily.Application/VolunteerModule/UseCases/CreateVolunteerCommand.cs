using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.VolunteerModule.UseCases;

public record CreateVolunteerCommand(
    string Surname,
    string Name,
    string? Patronymic,
    string Email,
    string Description,
    int EmployeeExperience,
    string TelephoneNumber,
    IEnumerable<SocialNetworkDto>? SocialNetworks = null,
    IEnumerable<RequisitDto>? Requisits = null);