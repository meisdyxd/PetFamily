namespace PetFamily.API.Contracts.Requests;

public record AddPhotosToPetRequest(string Title, string Description, IFormFileCollection Files);