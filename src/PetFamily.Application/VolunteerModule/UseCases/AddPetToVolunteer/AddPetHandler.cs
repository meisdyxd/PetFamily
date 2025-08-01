using CSharpFunctionalExtensions;
using PetFamily.Application.SpeciesModule;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases.AddPetToVolunteer;

public class AddPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ISpeciesRepository _speciesRepository;

    public AddPetHandler(
        IVolunteerRepository volunteerRepository,
        ISpeciesRepository speciesRepository)
    {
        _volunteerRepository = volunteerRepository;
        _speciesRepository = speciesRepository;
    }

    public async Task<Result<Guid, ErrorResult>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new AddPetCommandValidator();
        var validation = validator.Validate(command);

        if (!validation.IsValid)
            return validation.ToError();

        var volunteer = await _volunteerRepository.GetById(command.Id, cancellationToken);
        if (volunteer == null)
            return Errors.General.RecordNotFound(command.Id);

        var existSpeciesAndBreed = await _speciesRepository.ExistBreed(command.Species.Id, command.Breed.Id, cancellationToken);
        if (!existSpeciesAndBreed)
            return Errors.General.RecordNotFound(command.Species.Id);

        var moniker = command.Moniker;
        var speciesVO = new Species(command.Species.Id);
        var description = Description.Create(command.Description.Value).Value;
        var breed = new Breed(command.Breed.Id);
        var coloration = command.Coloration;
        var healthInfo = command.HealthInfo;
        var address = Address.Create(
            command.Address.Country,
            command.Address.Region,
            command.Address.City,
            command.Address.Street,
            command.Address.Flat).Value;
        var weight = command.Weight;
        var height = command.Height;
        var telephoneNumber = TelephoneNumber.Create(command.OwnerTelephoneNumber.Value).Value;
        var isCastrated = command.IsCastrated;
        var birthDate = command.BirthDate;
        var isVaccinated = command.IsVaccinated;

        var pet = Pet.Create(
            moniker,
            speciesVO,
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

        if (pet.IsFailure)
            return Errors.General.ValueIsInvalid(nameof(pet));

        volunteer.AddPet(pet.Value);

        await _volunteerRepository.Save(volunteer, cancellationToken);

        return pet.Value.Id;
    }
}
