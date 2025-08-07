using Microsoft.AspNetCore.Mvc;

namespace PetFamily.API.Requests.VolunteerRequests;

public record MovePetPositionRequest(
    [FromQuery] int? Position,
    [FromQuery] string? Direction);