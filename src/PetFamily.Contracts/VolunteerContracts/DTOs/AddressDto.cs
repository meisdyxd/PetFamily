namespace PetFamily.Contracts.VolunteerContracts.DTOs;

public record AddressDto(
        string Country,
        string Region,
        string City,
        string Street,
        string Flat);