using CSharpFunctionalExtensions;
using FluentValidation;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Contracts.VolunteerContracts.Response;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases;

public class CreateVolunteerHandler
{
    private IVolunteerRepository _repository;

    public CreateVolunteerHandler(
        IVolunteerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CreateVolunteerResponse, ErrorResult>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new CreateVolunteerCommandValidator();
        var validation = validator.Validate(command);

        if (!validation.IsValid)
            return validation.ToError();

        var fullName = FullName.Create(command.Surname, command.Name, command.Patronymic).Value;

        var email = Email.Create(command.Email).Value;

        var description = Description.Create(command.Description).Value;

        var employeeExperience = EmployeeExperience.Create(command.EmployeeExperience).Value;

        var telephoneNumber = TelephoneNumber.Create(command.TelephoneNumber).Value;

        var socialNetworks = command.SocialNetworks?
            .Select(sn => SocialNetwork.Create(sn.Name, sn.Link));

        var validatedSocialNetworks = socialNetworks?.Select(sn => sn.Value);

        var requisits = command.Requisits?
            .Select(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));

        var validatedRequisits = requisits?.Select(sn => sn.Value);

        var volunteer = Volunteer.Create(
            fullName,
            email,
            description,
            employeeExperience,
            telephoneNumber,
            validatedSocialNetworks,
            validatedRequisits);

        if (volunteer.IsFailure)
            return volunteer.Error;

        var volunteerId = await _repository.Create(volunteer.Value, cancellationToken);

        return new CreateVolunteerResponse(volunteerId);
    }
}
