namespace PetFamily.Application.Minio;

public interface IFilesProvider
{
    Task UploadFileAsync(Stream stream, string bucketName, string fileName, CancellationToken cancellationToken);
    Task DeleteFileAsync(string bucketName, string fileName, CancellationToken cancellationToken);
    Task GetPresignedAsync(string bucketName, string fileName, CancellationToken cancellationToken);
}
