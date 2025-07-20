using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Contracts.VolunteerContracts.Request;

public record UpdateRequisitsRequest(IEnumerable<RequisitDto>? Requisits = null);
