using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.VolunteerModule.UseCases;
using PetFamily.Contracts.VolunteerContracts.Extensions;
using PetFamily.Contracts.VolunteerContracts.Request;

namespace PetFamily.API.Controllers;


public class VolunteerController : MainController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var volunteer = await handler.Handle(command, cancellationToken);

        if (volunteer.IsFailure)
            return volunteer.Error.ToResponse();

        return Created(volunteer.Value);
    }
}
