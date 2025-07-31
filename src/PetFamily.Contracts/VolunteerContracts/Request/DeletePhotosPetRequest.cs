namespace PetFamily.Contracts.VolunteerContracts.Request;

public record DeletePhotosPetRequest(IEnumerable<string> Filenames);