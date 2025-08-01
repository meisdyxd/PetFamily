namespace PetFamily.Application.VolunteerModule.UseCases.DeletePhotosPet;

public record DeletePhotosPetCommand(Guid Id, Guid PetId, IEnumerable<string> Filenames);