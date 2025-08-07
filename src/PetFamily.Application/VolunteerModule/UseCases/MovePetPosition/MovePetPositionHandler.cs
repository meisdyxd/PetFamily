using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Interfaces;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.UseCases.MovePetPosition;

public class MovePetPositionHandler: ICommandHandler<MovePetPositionCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<MovePetPositionHandler> _logger;

    public MovePetPositionHandler(
        IVolunteerRepository repository,
        ILogger<MovePetPositionHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        MovePetPositionCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new MovePetPositionCommanValdiator();
        var validation = await validator.ValidateAsync(command, cancellationToken);
        
        if (!validation.IsValid)
            return validation.ToError();
        
        var volunteer = await _repository.GetById(command.VolunteerId, cancellationToken);
        if(volunteer == null)
            return Errors.General.RecordNotFound(command.VolunteerId);
        
        var pet = volunteer.Pets.SingleOrDefault(p => p.Id == command.PetId);
        if (pet == null)
            return Errors.General.RecordNotFound(command.PetId);

        if (command.Direction is null)
        {
            var operationResult = volunteer.MovePetToPosition(pet, command.Position!.Value);
            if(operationResult.IsFailure)
                return operationResult.Error;
        }
        else
        {
            if (command.Direction == Constants.Pet.DIRECTION_BEGIN)
                volunteer.MovePetToBegin(pet);
            else
                volunteer.MovePetToEnd(pet);
        }
        
        await _repository.Save(volunteer, cancellationToken);
        _logger.LogInformation("Изменена позиция питомца с ID '{id}' на {position}",
            command.PetId, 
            command.Position?.ToString() ?? command.Direction);
        
        return UnitResult.Success<ErrorResult>();
    }
}