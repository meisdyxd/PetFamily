using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.UseCases.ForceDeleteVolunteer;

public class ForceDeleteHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<ForceDeleteHandler> _logger;

    public ForceDeleteHandler(
        IVolunteerRepository repository,
        ILogger<ForceDeleteHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        ForceDeleteCommand command,
        CancellationToken cancellationToken)
    {
        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer is null)
            return Errors.General.RecordNotFound(command.Id);

        await _repository.Delete(volunteer, cancellationToken);
        _logger.LogInformation("Удалён(hard) волонтер с ID: {Id} и его внутренние элементы", volunteer.Id);

        return UnitResult.Success<ErrorResult>();
    }
}
