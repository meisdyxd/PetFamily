using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;
using PetStatus = PetFamily.Domain.VolunteerManagement.Enums.PetStatus;

namespace PetFamily.Domain.VolunteerManager;

public class Volunteer: Entity<Guid>
{
    //EF Core
    protected Volunteer(Guid id) : base(id) { }

    private Volunteer(
        FullName fullName,
        Email email,
        Description description,
        EmployeeExperience employeeExperience,
        TelephoneNumber telephoneNumber,
        IEnumerable<SocialNetwork> socialNetworks,
        IEnumerable<Requisit> requisits)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        EmployeeExperience = employeeExperience;
        TelephoneNumber = telephoneNumber;
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Description Description { get; private set; }
    public EmployeeExperience EmployeeExperience { get; private set; }
    public TelephoneNumber TelephoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; private set; } = [];
    public IReadOnlyList<Requisit> Requisits { get; private set; } = [];
    public IReadOnlyList<Pet> Pets { get; private set; } = [];

    public Volunteer Create(
        FullName fullName,
        Email email,
        Description description,
        EmployeeExperience employeeExperience,
        TelephoneNumber telephoneNumber,
        IReadOnlyList<SocialNetwork>? socialNetworks = null,
        IReadOnlyList<Requisit>? requisits = null)
    {
        return new(
            fullName, 
            email, 
            description, 
            employeeExperience, 
            telephoneNumber, 
            socialNetworks ?? [], 
            requisits ?? []);
    }

    public int CountPetsHomeless() =>
        Pets.Count(p => p.Status == PetStatus.Homeless);

    public int CountPetsFindHome() =>
        Pets.Count(p => p.Status == PetStatus.FindHome);

    public int CountPetsPendingHelp() =>
        Pets.Count(p => p.Status == PetStatus.PendingHelp);
}
