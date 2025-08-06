using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.VolunteerModule.Models;

public class VolunteerDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Surname { get; init; } = null!;
    public string Patronymic { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Description { get; init; }  = null!;
    public int EmployeeExperience { get; init; }
    public string TelephoneNumber { get; init; }  = null!;
    public List<SocialNetworkDto> SocialNetworks { get; set; } = [];
    public List<RequisitDto> Requisits { get; set; } = [];
}