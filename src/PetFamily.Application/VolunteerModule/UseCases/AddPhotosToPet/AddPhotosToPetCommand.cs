using PetFamily.Application.Interfaces;
using PetFamily.Contracts.Contracts.FormFIleDtos;

namespace PetFamily.Application.VolunteerModule.UseCases.AddPhotosToPet;

public record AddPhotosToPetCommand(
    Guid VolunteerId, 
    Guid PetId, 
    List<CreateFileDto> Files): ICommand;