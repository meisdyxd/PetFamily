using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public record EmployeeExperience
{
    private EmployeeExperience(int year)
    {
        Year = year;
    }

    public int Year {  get; }

    public static Result<EmployeeExperience, ErrorResult> Create(int year)
    {
        if (year < 0)
            return Errors.General.ValueIsInvalid(nameof(year));

        return new EmployeeExperience(year);
    }
}