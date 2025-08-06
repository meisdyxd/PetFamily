using PetFamily.Application.Interfaces;

namespace PetFamily.Application.VolunteerModule.UseCases.SoftDeleteVolunteer;

public record SoftDeleteCommand(Guid Id) : ICommand;
