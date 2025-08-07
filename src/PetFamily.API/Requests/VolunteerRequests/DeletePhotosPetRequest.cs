namespace PetFamily.API.Requests.VolunteerRequests;

public record DeletePhotosPetRequest(IEnumerable<string> Filenames);