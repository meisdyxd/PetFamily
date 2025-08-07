using PetFamily.Application.Interfaces;

namespace PetFamily.Application.VolunteerModule.UseCases.RestoreVolunteer;

public record RestoreCommand(Guid Id): ICommand;
