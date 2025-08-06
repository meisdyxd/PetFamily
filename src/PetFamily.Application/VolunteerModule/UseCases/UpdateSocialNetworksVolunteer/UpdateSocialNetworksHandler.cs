using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;

public class UpdateSocialNetworksHandler : ICommandHandler<UpdateSocialNetworksCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateSocialNetworksHandler> _logger;

    public UpdateSocialNetworksHandler(
        IVolunteerRepository repository,
        ILogger<UpdateSocialNetworksHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        UpdateSocialNetworksCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateSocialNetworksCommandValidator();
        var validation = await validator.ValidateAsync(command, cancellationToken);

        if (!validation.IsValid)
            return validation.ToError();

        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer is null)
            return Errors.General.RecordNotFound(command.Id);

        var socialNetworks = command.SocialNetworks?
            .Select(sn => SocialNetwork.Create(sn.Name, sn.Link));

        var validatedSocialNetworks = socialNetworks?.Select(sn => sn.Value);

        volunteer.UpdateSocialNetworks(validatedSocialNetworks);

        await _repository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Обновлены социальные сети волонтера с ID: '{id}'", volunteer.Id);

        return UnitResult.Success<ErrorResult>();
    }
}
