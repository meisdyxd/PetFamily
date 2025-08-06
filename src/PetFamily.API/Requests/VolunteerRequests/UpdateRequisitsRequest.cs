using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.API.Requests.VolunteerRequests;

public record UpdateRequisitsRequest(IEnumerable<RequisitDto>? Requisits = null);
