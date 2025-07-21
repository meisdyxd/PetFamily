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
        DeleteByCascade = false;
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
        Pets.Count(p => !p.IsDeleted && p.Status == PetStatus.Homeless);

    public int CountPetsFindHome() =>
        Pets.Count(p => !p.IsDeleted && p.Status == PetStatus.FindHome);

    public int CountPetsPendingHelp() =>
        Pets.Count(p => !p.IsDeleted && p.Status == PetStatus.PendingHelp);

    public int NextSequenceNumberForPet() => !_pets.Any(p => !p.IsDeleted)
            ? 1
            : _pets.Where(p => !p.IsDeleted).Max(p => p.SequenceNumber) + 1;

    public void RestorePet(Pet pet, bool innnerCascadeDeleted = false)
    {
        pet.SetSequenceNumber(NextSequenceNumberForPet());
        pet.Restore(false);
    }

    public void AddPet(Pet pet)
    {
        _pets.Add(pet);
        pet.SetSequenceNumber(NextSequenceNumberForPet());
    }

    public void MovePetToBegin(Pet pet)
    {
        MovePetToPosition(pet, 1);
    }

    public void MovePetToEnd(Pet pet)
    {
        var maxSeqNumber = _pets.Where(p => !p.IsDeleted)
            .Max(p => p.SequenceNumber);
        MovePetToPosition(pet, maxSeqNumber);
    }

    public UnitResult<ErrorResult> MovePetToPosition(Pet pet, int toPosition)
    {
        if (pet.SequenceNumber == toPosition)
            return UnitResult.Success<ErrorResult>();

        if (_pets.Count(p => !p.IsDeleted) < toPosition)
            return Errors.General.ValueIsInvalid(nameof(toPosition));

        if (pet.SequenceNumber > toPosition)
            _pets
                .Where(p => !p.IsDeleted)
                .Where(p => p.SequenceNumber != pet.SequenceNumber)
                .Where(p => p.SequenceNumber < pet.SequenceNumber)
                .ToList()
                .ForEach(p => p.SetSequenceNumber(p.SequenceNumber + 1));
        else
        {
            _pets
                .Where(p => !p.IsDeleted)
                .Where(p => p.SequenceNumber != pet.SequenceNumber)
                .Where(p => p.SequenceNumber > pet.SequenceNumber)
                .ToList()
                .ForEach(p => p.SetSequenceNumber(p.SequenceNumber - 1));
        }

        pet.SetSequenceNumber(toPosition);

        return UnitResult.Success<ErrorResult>();
    }

    public void RemovePet(Pet pet)
    {
        var deleteNumber = pet.SequenceNumber;

        pet.SetSequenceNumber(-1);
        pet.Delete(cascade: true);
        ReorderAfterDeletePet(deleteNumber);
    }

    public void ReorderAfterDeletePet(int deleteNumber)
    {
        var petsToReorder = _pets.Where(p => p.SequenceNumber > deleteNumber);
        foreach(var pet in petsToReorder)
        {
            var currentSequenceNumber = pet.SequenceNumber;
            pet.SetSequenceNumber(currentSequenceNumber - 1);
        }
    }
}
