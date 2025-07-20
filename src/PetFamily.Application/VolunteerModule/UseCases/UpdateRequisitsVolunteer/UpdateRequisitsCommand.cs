using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;

public record UpdateRequisitsCommand(
    Guid Id,
    IEnumerable<RequisitDto>? Requisits = null);
