using PetFamily.Application.Interfaces;

namespace PetFamily.Application.VolunteerModule.UseCases.MovePetPosition;

public record MovePetPositionCommand(Guid VolunteerId, Guid PetId, int? Position = null, string? Direction = null)
    : ICommand;