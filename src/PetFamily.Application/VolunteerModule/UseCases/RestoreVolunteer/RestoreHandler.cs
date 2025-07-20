using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.UseCases.RestoreVolunteer;

public class RestoreHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<RestoreHandler> _logger;

    public RestoreHandler(
        IVolunteerRepository repository,
        ILogger<RestoreHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        RestoreCommand command,
        CancellationToken cancellationToken)
    {
        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer is null)
            return Errors.General.RecordNotFound(command.Id);

        volunteer.Restore(innnerCascadeDeleted: true);

        await _repository.Save(volunteer, cancellationToken);
        _logger.LogInformation("Восстановлен волонтер с ID: {Id} и его внутренние элементы", volunteer.Id);

        return UnitResult.Success<ErrorResult>();
    }
}
