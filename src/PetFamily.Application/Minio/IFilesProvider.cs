using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Application.Minio;

public interface IFilesProvider
{
    Task<UnitResult<ErrorResult>> UploadFileAsync(Stream stream, string bucketName, string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string bucketName, string fileName, CancellationToken cancellationToken);
    Task<string> GetPresignedAsync(string bucketName, string fileName, CancellationToken cancellationToken);
    Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken);
    Task<bool> ExistBucketAsync(string bucketName, CancellationToken cancellationToken);
}
