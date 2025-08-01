using PetFamily.Contracts.VolunteerContracts.DTOs;
using System.Net;

namespace PetFamily.Contracts.VolunteerContracts.Request;

public record AddPetRequest(
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
