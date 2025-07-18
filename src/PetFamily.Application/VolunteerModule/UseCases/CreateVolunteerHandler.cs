using CSharpFunctionalExtensions;
using PetFamily.Contracts.VolunteerContracts.Request;
using PetFamily.Contracts.VolunteerContracts.Response;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases;

public class CreateVolunteerHandler
{
    private IVolunteerRepository _repository;

    public CreateVolunteerHandler(IVolunteerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<CreateVolunteerResponse, Error>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var fullName = FullName.Create(request.Surname, request.Name, request.Patronymic);

        if (fullName.IsFailure)
            return fullName.Error;

        var email = Email.Create(request.Email);

        if (email.IsFailure)
            return email.Error;

        var description = Description.Create(request.Description);

        if (description.IsFailure)
            return description.Error;

        var employeeExperience = EmployeeExperience.Create(request.EmployeeExperience);

        if (employeeExperience.IsFailure)
            return employeeExperience.Error;

        var telephoneNumber = TelephoneNumber.Create(request.TelephoneNumber);

        if (telephoneNumber.IsFailure)
            return telephoneNumber.Error;

        var socialNetworks = request.SocialNetworks?
            .Select(sn => SocialNetwork.Create(sn.Name, sn.Link));

        if (socialNetworks is not null &&
            socialNetworks.Any(sn => sn.IsFailure))
            return socialNetworks.First().Error;

        var validatedSocialNetworks = socialNetworks?.Select(sn => sn.Value);

        var requisits = request.Requisits?
            .Select(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));

        if (requisits is not null &&
            requisits.Any(r => r.IsFailure))
            return requisits.First().Error;

        var validatedRequisits = requisits?.Select(sn => sn.Value);

        var volunteer = Volunteer.Create(
            fullName.Value,
            email.Value,
            description.Value,
            employeeExperience.Value,
            telephoneNumber.Value,
            validatedSocialNetworks,
            validatedRequisits);

        if (volunteer.IsFailure)
            return volunteer.Error;

        var volunteerId = await _repository.Create(volunteer.Value, cancellationToken);

        return new CreateVolunteerResponse(volunteerId);
    }
}
