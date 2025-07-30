using CSharpFunctionalExtensions;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.UseCases.AddPhotosToPet;

public class AddPhotosToPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;

    public AddPhotosToPetHandler(IVolunteerRepository volunteerRepository)
    {
        _volunteerRepository = volunteerRepository;
    }
    
    public async Task<UnitResult<ErrorResult>> Handle(
        AddPhotosToPetCommand command,
        CancellationToken cancellationToken)
    {
        var vallidator = new AddPhotosToPetValidator();
        var validation = await vallidator.ValidateAsync(command, cancellationToken);
        if(validation.IsValid)
            return validation.ToError();
        
        var volunteer = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer == null)
            return Errors.General.RecordNotFound(command.VolunteerId);
        
        if(volunteer.Pets.All(p => p.Id != command.PetId))
            return Errors.General.RecordNotFound(command.PetId);
        
        
    }
}