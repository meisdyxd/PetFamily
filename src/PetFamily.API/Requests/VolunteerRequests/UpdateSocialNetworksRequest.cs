using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.API.Requests.VolunteerRequests;

public record UpdateSocialNetworksRequest(IEnumerable<SocialNetworkDto>? SocialNetworks = null);
