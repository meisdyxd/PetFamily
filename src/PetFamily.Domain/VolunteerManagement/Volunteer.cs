using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;
using PetStatus = PetFamily.Domain.VolunteerManagement.Enums.PetStatus;

namespace PetFamily.Domain.VolunteerManager;

public class Volunteer: Entity<Guid>
{
    private readonly List<SocialNetwork> _socialNetworks = [];
    private readonly List<Requisit> _requisits = [];
    private readonly List<Pet> _pets = [];

    //EF Core
    protected Volunteer(Guid id) : base(id) { }

    private Volunteer(
        FullName fullName,
        Email email,
        Description description,
        EmployeeExperience employeeExperience,
        TelephoneNumber telephoneNumber,
        IEnumerable<SocialNetwork>? socialNetworks,
        IEnumerable<Requisit>? requisits)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        EmployeeExperience = employeeExperience;
        TelephoneNumber = telephoneNumber;
        _socialNetworks = socialNetworks!.ToList();
        _requisits = requisits!.ToList();
    }

    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public Description Description { get; private set; }
    public EmployeeExperience EmployeeExperience { get; private set; }
    public TelephoneNumber TelephoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisit> Requisits => _requisits;
    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Volunteer, ErrorEventArgs> Create(
        FullName fullName,
        Email email,
        Description description,
        EmployeeExperience employeeExperience,
        TelephoneNumber telephoneNumber,
        IEnumerable<SocialNetwork>? socialNetworks,
        IEnumerable<Requisit>? requisits)
    {
        return new Volunteer(
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
