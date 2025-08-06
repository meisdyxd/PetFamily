using PetFamily.Application.Interfaces;
using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.VolunteerModule.UseCases.UpdateMainInfoVolunteer;

public record UpdateMainInfoCommand(
    Guid Id,
    FullnameDto Fullname,
    EmailDto Email,
    DescriptionDto Description,
    int EmployeeExperience,
    TelephoneNumberDto TelephoneNumber) : ICommand;
