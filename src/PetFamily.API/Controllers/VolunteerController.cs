﻿using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.VolunteerModule.UseCases.CreateVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.ForceDeleteVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.RestoreVolunteer;
using PetFamily.Application.VolunteerModule.UseCases.SoftDeleteVolunteer;
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
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Created(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<IActionResult> UpdateMainInfo(
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return NoContent();
    }

    [HttpPut("{id:guid}/requisits")]
    public async Task<IActionResult> UpdateRequisits(
        [FromServices] UpdateRequisitsHandler handler,
        [FromBody] UpdateRequisitsRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return NoContent();
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<IActionResult> UpdateSocialNetworks(
        [FromServices] UpdateSocialNetworksHandler handler,
        [FromBody] UpdateSocialNetworksRequest request,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return NoContent();
    }

    [HttpDelete("{id:guid}/soft")]
    public async Task<IActionResult> SoftDelete(
        [FromServices] SoftDeleteHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new SoftDeleteCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return NoContent();
    }

    [HttpDelete("{id:guid}/force")]
    public async Task<IActionResult> ForceDelete(
        [FromServices] ForceDeleteHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new ForceDeleteCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return NoContent();
    }

    [HttpGet("{id:guid}/restore")]
    public async Task<IActionResult> Restore(
        [FromServices] RestoreHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var command = new RestoreCommand(id);
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return NoContent();
    }
}
