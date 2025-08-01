using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Minio;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.VolunteerModule.UseCases.DeletePhotosPet;

public class DeletePhotosPetHandler
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<DeletePhotosPetHandler> _logger;
    private readonly IFilesProvider _filesProvider;
    private const string Bucket = Constants.StorageBuckets.PhotosBucket;

    public DeletePhotosPetHandler(
        IVolunteerRepository repository,
        ILogger<DeletePhotosPetHandler> logger,
        IFilesProvider filesProvider)
    {
        _repository = repository;
        _logger = logger;
        _filesProvider = filesProvider;
    }

    public async Task<UnitResult<ErrorResult>> Handle(
        DeletePhotosPetCommand command,
        CancellationToken cancellationToken)
    {
        var volunteer = await _repository.GetById(command.Id, cancellationToken);
        if (volunteer == null)
            return Errors.General.RecordNotFound(command.Id);
        
        if(volunteer.Pets.All(p => p.Id != command.PetId))
            return Errors.General.RecordNotFound(command.PetId);

        try
        {
            foreach (var filename in command.Filenames)
            {
                var pet = volunteer.Pets.First(x => x.Id == command.PetId);
                var photo = pet.Photos.FirstOrDefault(p => p.Filename == filename);
                if (photo == null) continue;
                pet.DeletePhoto(photo);
                await _filesProvider.DeleteFileAsync(Bucket, filename, cancellationToken);
            }
            await _repository.Save(volunteer, cancellationToken);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении фотографий питомца");
            return Errors.General.InternalServerError("Minio delete photos");
        }

        return UnitResult.Success<ErrorResult>();
    }
}