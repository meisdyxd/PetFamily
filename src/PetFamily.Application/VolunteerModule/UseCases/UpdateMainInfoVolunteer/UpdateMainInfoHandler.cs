using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases.UpdateMainInfoVolunteer;

public class UpdateMainInfoHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteerRepository repository,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new UpdateMainInfoCommandValidator();
        var validation = validator.Validate(command);

        if (!validation.IsValid)
            return validation.ToError();

        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer is null)
            return Errors.General.RecordNotFound(command.Id);

        var fullName = FullName.Create(command.Fullname.Surname, command.Fullname.Name, command.Fullname.Patronymic).Value;

        var email = Email.Create(command.Email.Value).Value;

        var description = Description.Create(command.Description.Value).Value;

        var employeeExperience = EmployeeExperience.Create(command.EmployeeExperience).Value;

        var telephoneNumber = TelephoneNumber.Create(command.TelephoneNumber.Value).Value;

        volunteer.UpdateMainInfo(
            fullName,
            email,
            description,
            employeeExperience,
            telephoneNumber);

        await _repository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Обновлена основная информация волонтера с ID: '{id}'", volunteer.Id);

        return UnitResult.Success<ErrorResult>();
    }
}
