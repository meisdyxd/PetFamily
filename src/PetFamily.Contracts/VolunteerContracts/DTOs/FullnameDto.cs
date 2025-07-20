namespace PetFamily.Contracts.VolunteerContracts.DTOs;

public record FullnameDto(
    string Surname,
    string Name,
    string? Patronymic);
