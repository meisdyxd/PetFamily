using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement.Enums;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Domain.VolunteerManager;

public class Pet : Entity<Guid>
{
    private readonly List<Requisit> _requisits = [];

    //ef core
    protected Pet(Guid id): base(id) { }

    private Pet(string moniker,
        Species species, 
        Description description, 
        Breed breed, 
        string coloration, 
        string healthInfo, 
        Address address, 
        double weight, 
        double height, 
        TelephoneNumber ownerTelephoneNumber, 
        bool isCastrated, 
        DateTime birthDate, 
        bool isVaccinated)
    {
        Moniker = moniker;
        Species = species;
        Description = description;
        Breed = breed;
        Coloration = coloration;
        HealthInfo = healthInfo;
        Address = address;
        Weight = weight;
        Height = height;
        OwnerTelephoneNumber = ownerTelephoneNumber;
        IsCastrated = isCastrated;
        BirthDate = birthDate;
        IsVaccinated = isVaccinated;
        Status = PetStatus.PendingHelp;
        CreatedAt = DateTime.UtcNow;
    }

    public string Moniker { get; private set; }
    public Species Species { get; private set; }
    public Description Description { get; private set; }
    public Breed Breed { get; private set; }
    public string Coloration { get; private set; }
    public string HealthInfo { get; private set; }
    public Address Address { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public TelephoneNumber OwnerTelephoneNumber { get; private set; }
    public bool IsCastrated { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; }
    public PetStatus Status { get; private set; }
    public IReadOnlyList<Requisit> Requisits => _requisits;
    public DateTime CreatedAt { get; private set; }

    public static Result<Pet, Error> Create(
        string moniker, 
        Species species, 
        Description description, 
        Breed breed, 
        string coloration, 
        string healthInfo, 
        Address address,
        double weight,
        double height,
        TelephoneNumber telephoneNumber,
        bool isCastrated,
        DateTime birthDate,
        bool isVaccinated)
    {
        if (weight <= 0)
            return Errors.General.ValueIsInvalid(nameof(weight));

        if (height <= 0)
            return Errors.General.ValueIsInvalid(nameof(height));

        return new Pet(
            moniker,
            species,
            description,
            breed,
            coloration,
            healthInfo,
            address,
            weight,
            height,
            telephoneNumber,
            isCastrated,
            birthDate,
            isVaccinated);
    }
}