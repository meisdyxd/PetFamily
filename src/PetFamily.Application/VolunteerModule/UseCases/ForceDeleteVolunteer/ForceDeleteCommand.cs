using PetFamily.Application.Interfaces;

namespace PetFamily.Application.VolunteerModule.UseCases.ForceDeleteVolunteer;

public record ForceDeleteCommand(Guid Id) : ICommand;
