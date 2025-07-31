using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Minio;
using PetFamily.Application.VolunteerModule.Extensions;
using PetFamily.Application.VolunteerModule.ValidationRules;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.UseCases.AddPhotosToPet;

public class AddPhotosToPetHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IFilesProvider _filesProvider;
    private readonly ILogger<AddPhotosToPetHandler> _logger;
    private const string Bucket = Constants.StorageBuckets.PhotosBucket;

    public AddPhotosToPetHandler(
        IVolunteerRepository volunteerRepository,
        IFilesProvider filesProvider,
        ILogger<AddPhotosToPetHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _filesProvider = filesProvider;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorResult>> Handle(
        AddPhotosToPetCommand command,
        CancellationToken cancellationToken)
    {
        var validator = new AddPhotosToPetValidator();
        var validation = await validator.ValidateAsync(command, cancellationToken);
        if(!validation.IsValid)
            return validation.ToError();
        
        var volunteer = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteer == null)
            return Errors.General.RecordNotFound(command.VolunteerId);
        
        if(volunteer.Pets.All(p => p.Id != command.PetId))
            return Errors.General.RecordNotFound(command.PetId);
        
        if (!await _filesProvider.ExistBucketAsync(Bucket, cancellationToken))
            await _filesProvider.CreateBucketAsync(Bucket, cancellationToken);
        var semaphore = new SemaphoreSlim(5, 5);
        var uploadedFiles = new List<string>();
        var errors = new List<ErrorResponse>();
        try
        {
            var tasks = command.Files.Select(async file =>
            {
                await semaphore.WaitAsync(cancellationToken);
                try
                {
                    var filename = $"{Guid.NewGuid()}{Path.GetExtension(file.Filename)}";
                    var result = await _filesProvider.UploadFileAsync(
                        file.Stream,
                        Bucket,
                        filename,
                        cancellationToken);

                    if (result.IsSuccess)
                    {
                        lock (uploadedFiles)
                        {
                            uploadedFiles.Add(filename);
                        }
                    }
                    else
                    {
                        errors.Add(new ErrorResponse(
                            $"Ошибка загрузки фотографии '{file.Filename}'",
                            "error.upload",
                            "ServiceUnavailable"));
                    }
                }
                catch (Exception ex)
                {
                    errors.Add(new ErrorResponse(
                        ex.Message,
                        "error.upload",
                        "ServiceUnavailable"));
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
            if (errors.Count > 0)
            {
                await RollbackUploads(uploadedFiles, cancellationToken);
                _logger.LogError("Ошибка во время загрузки фотографий. Было удалено {count} фотографий", errors.Count);
                return ErrorResult.Create(errors);
            }
            var pet = volunteer.Pets.First(x => x.Id == command.PetId);
            uploadedFiles.ForEach(filename => pet.AddPhoto(new Photo(filename)));
            await _volunteerRepository.Save(volunteer, cancellationToken);
        }
        catch (Exception ex)
        {
            await RollbackUploads(uploadedFiles, cancellationToken);
            _logger.LogError("Ошибка во время загрузки фотографий. Было удалено {count} фотографий", errors.Count);
        }

        return UnitResult.Success<ErrorResult>();
    }
    
    private async Task RollbackUploads(
        List<string> uploadedFiles, 
        CancellationToken cancellationToken)
    {
        var deleteTasks = uploadedFiles.Select(filename => 
            _filesProvider.DeleteFileAsync(Bucket, filename, cancellationToken));
    
        await Task.WhenAll(deleteTasks);
    }
}