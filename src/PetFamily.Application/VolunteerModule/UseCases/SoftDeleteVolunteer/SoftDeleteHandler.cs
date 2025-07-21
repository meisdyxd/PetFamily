using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.UseCases.SoftDeleteVolunteer;

public class SoftDeleteHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<SoftDeleteHandler> _logger;

    public SoftDeleteHandler(
        IVolunteerRepository repository,
        ILogger<SoftDeleteHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        SoftDeleteCommand command,
        CancellationToken cancellationToken)
    {
        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer is null)
            return Errors.General.RecordNotFound(command.Id);

        volunteer.Delete(cascade: true);

        await _repository.Save(volunteer, cancellationToken);
        _logger.LogInformation("Удалён(soft) волонтер с ID: {Id} и его внутренние элементы", volunteer.Id);

        return UnitResult.Success<ErrorResult>();
    }
}
