using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;

public record UpdateSocialNetworksCommand(
    Guid Id,
    IEnumerable<SocialNetworkDto>? SocialNetworks = null);
