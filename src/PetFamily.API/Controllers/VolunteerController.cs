using Microsoft.AspNetCore.Mvc;
using PetFamily.Contracts.VolunteerContracts.Request;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;
using PetFamily.Domain.VolunteerManager;

namespace PetFamily.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateVolunteerRequest request)
    {
        var fullName = FullName.Create(request.Surname, request.Name, request.Patronymic);

        if (fullName.IsFailure)
            return BadRequest(fullName.Error);

        var email = Email.Create(request.Email);

        if (email.IsFailure)
            return BadRequest(email.Error);

        var description = Description.Create(request.Description);

        if (description.IsFailure)
            return BadRequest(description.Error);

        var employeeExperience = EmployeeExperience.Create(request.EmployeeExperience);

        if (employeeExperience.IsFailure)
            return BadRequest(employeeExperience.Error);

        var telephoneNumber = TelephoneNumber.Create(request.TelephoneNumber);

        if (telephoneNumber.IsFailure)
            return BadRequest(telephoneNumber.Error);

        var socialNetworks = request.SocialNetworks?
            .Select(sn => SocialNetwork.Create(sn.Name, sn.Link));

        if (socialNetworks is not null &&
            socialNetworks.Any(sn => sn.IsFailure))
            return BadRequest(socialNetworks.First().Error);

        var validatedSocialNetworks = socialNetworks?.Select(sn => sn.Value);

        var requisits = request.Requisits?
            .Select(r => Requisit.Create(r.Name, r.Description, r.DetailInstruction));

        if (requisits is not null &&
            requisits.Any(r => r.IsFailure))
            return BadRequest(requisits.First().Error);

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
            return BadRequest(volunteer.Error);

        return Ok(volunteer.Value);
    }
}
