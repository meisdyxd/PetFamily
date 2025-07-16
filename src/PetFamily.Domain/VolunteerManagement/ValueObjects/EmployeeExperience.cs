namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public record EmployeeExperience
{
    public EmployeeExperience(int year)
    {
        if (year < 0)
            throw new ArgumentException(nameof(Year), "Опыт сотрудника не может быть отрицательным.");
        Year = year;
    }
    public int Year {  get; }
}