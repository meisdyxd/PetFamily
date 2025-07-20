using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases.UpdateRequisitsVolunteer;

public class UpdateRequisitsHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateRequisitsHandler> _logger;

    public UpdateRequisitsHandler(
        IVolunteerRepository repository,
        ILogger<UpdateRequisitsHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        UpdateRequisitsCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateRequisitsCommandValidator();
        var validation = validator.Validate(command);

        if (!validation.IsValid)
            return validation.ToError();

        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer is null)
            return Errors.General.RecordNotFound(command.Id);

        var requisits = command.Requisits?
            .Select(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));

        var validatedRequisits = requisits?.Select(sn => sn.Value);

        volunteer.UpdateRequisits(validatedRequisits);

        await _repository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Обновлены реквизиты волонтера с ID: '{id}'", volunteer.Id);

        return UnitResult.Success<ErrorResult>();
    }
}
