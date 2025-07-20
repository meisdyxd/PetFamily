using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateMainInfoVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;
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

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var volunteer = await handler.Handle(command, cancellationToken);

        if (volunteer.IsFailure)
            return volunteer.Error.ToResponse();

        return Updated();
    }

    [HttpPut("{id:guid}/requisits")]
    public async Task<IActionResult> UpdateRequisits(
        [FromServices] UpdateRequisitsHandler handler,
        [FromBody] UpdateRequisitsRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var volunteer = await handler.Handle(command, cancellationToken);

        if (volunteer.IsFailure)
            return volunteer.Error.ToResponse();

        return Updated();
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromServices] UpdateSocialNetworksHandler handler,
        [FromBody] UpdateSocialNetworksRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var volunteer = await handler.Handle(command, cancellationToken);

        if (volunteer.IsFailure)
            return volunteer.Error.ToResponse();

        return Updated();
    }
}
