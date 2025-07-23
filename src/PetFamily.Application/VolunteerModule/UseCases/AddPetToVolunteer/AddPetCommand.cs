using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.VolunteerModule.UseCases.AddPetToVolunteer;

public record AddPetCommand(
    Guid Id,
    string Moniker,
    SpeciesDto Species,
    DescriptionDto Description,
    BreedDto Breed,
    string Coloration,
    string HealthInfo,
    AddressDto Address,
    double Weight,
    double Height,
    TelephoneNumberDto OwnerTelephoneNumber,
    bool IsCastrated,
    DateTime BirthDate,
    bool IsVaccinated);
