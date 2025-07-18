using Microsoft.AspNetCore.Mvc;
using PetFamily.Application.VolunteerModule.UseCases;
using PetFamily.Contracts.VolunteerContracts.Request;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request)
    {
        var tokenSource = new CancellationTokenSource();

        var volunteer = await handler.Handle(request, tokenSource.Token);

        if (volunteer.IsFailure)
            return BadRequest(volunteer.Error);

        return Ok(volunteer.Value);
    }
}
