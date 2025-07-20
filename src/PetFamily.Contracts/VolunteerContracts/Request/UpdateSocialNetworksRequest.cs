using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Contracts.VolunteerContracts.Request;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto>? SocialNetworks = null);
