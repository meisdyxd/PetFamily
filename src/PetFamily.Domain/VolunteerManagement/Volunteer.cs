using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.ValueObjects;
using PetStatus = PetFamily.Domain.VolunteerManagement.Enums.PetStatus;

namespace PetFamily.Domain.VolunteerManagement;

public class Volunteer: SoftDeletableEnity<Guid>
{
    private List<SocialNetwork> _socialNetworks = [];
    private List<Requisit> _requisits = [];
    private List<Pet> _pets = [];

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

    public static Result<Volunteer, ErrorResult> Create(
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

    public UnitResult<ErrorResult> UpdateMainInfo(
        FullName fullName,
        Email email,
        Description description,
        EmployeeExperience employeeExperience,
        TelephoneNumber telephoneNumber)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        EmployeeExperience = employeeExperience;
        TelephoneNumber = telephoneNumber;

        return UnitResult.Success<ErrorResult>();
    }

    public UnitResult<ErrorResult> UpdateRequisits(
        IEnumerable<Requisit>? requisits)
    {
        _requisits = requisits is null 
            ? [] 
            : requisits.ToList();

        return UnitResult.Success<ErrorResult>();
    }

    public UnitResult<ErrorResult> UpdateSocialNetworks(
        IEnumerable<SocialNetwork>? socialNetworks)
    {
        _socialNetworks = socialNetworks is null
            ? []
            : socialNetworks.ToList();

        return UnitResult.Success<ErrorResult>();
    }

    public override void Delete(DateTime? deletionDate = null, bool cascade = false)
    {
        var date = deletionDate ?? DateTime.UtcNow;
        DeleteByCascade = cascade;
        DeletionDate = date;
        IsDeleted = true;

        foreach (var pet in _pets.Where(p => p.IsDeleted == false))
            pet.Delete(deletionDate: date, cascade: cascade);
    }

    public override void Restore(bool innnerCascadeDeleted = false)
    {
        IsDeleted = false;
        DeletionDate = null;
        DeleteByCascade = false;

        if (innnerCascadeDeleted)
            foreach (var pet in _pets.Where(p => p.DeleteByCascade))
                pet.Restore(innnerCascadeDeleted);
    }

    public int CountPetsHomeless() =>
        Pets.Count(p => p.Status == PetStatus.Homeless);

    public int CountPetsFindHome() =>
        Pets.Count(p => p.Status == PetStatus.FindHome);

    public int CountPetsPendingHelp() =>
        Pets.Count(p => p.Status == PetStatus.PendingHelp);
}
