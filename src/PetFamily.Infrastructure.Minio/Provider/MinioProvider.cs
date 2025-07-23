using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.Minio;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Infrastructure.Minio.Provider;

public class MinioProvider : IFilesProvider
{
    private readonly IMinioClient _minioClient;
    private readonly ILogger<MinioProvider> _logger;

    public MinioProvider(
        IMinioClient minioClient,
        ILogger<MinioProvider> logger)
    {
        _minioClient = minioClient;
        _logger = logger;
    }

    public async Task DeleteFileAsync(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var deleteObjectArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName);

        await _minioClient.RemoveObjectAsync(deleteObjectArgs, cancellationToken);
        _logger.LogInformation("Deleted file in {bucket} with name {name}", bucketName, fileName);
    }

    public async Task<string> GetPresignedAsync(string bucketName, string fileName, CancellationToken cancellationToken)
    {
        var presignedObjectArgs = new PresignedGetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithExpiry(86400);

        var result = await _minioClient.PresignedGetObjectAsync(presignedObjectArgs);
        _logger.LogInformation("Generated link to file in {bucket} with name {name}", bucketName, fileName);
        return result;
    }

    public async Task<UnitResult<ErrorResult>> UploadFileAsync(Stream stream, string bucketName, string fileName, CancellationToken cancellationToken)
    {
        if (!await ExistBucketAsync(bucketName, cancellationToken))
            await CreateBucketAsync(bucketName, cancellationToken);

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithStreamData(stream)
            .WithObject(fileName)
            .WithObjectSize(stream.Length)
            .WithContentType("application/octet-stream");

        var result = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);
        if ((int)result.ResponseStatusCode > 299)
            return Errors.Minio.UploadError("Ошибка загрузки файла", result.ResponseStatusCode.ToString());

        _logger.LogInformation("Uploaded file in {bucket} with name {name}", bucketName, fileName);
        return UnitResult.Success<ErrorResult>();
    }

    public async Task CreateBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var bucket = new MakeBucketArgs()
            .WithBucket(bucketName);

        await _minioClient.MakeBucketAsync(bucket, cancellationToken);
    }

    public async Task<bool> ExistBucketAsync(string bucketName, CancellationToken cancellationToken)
    {
        var bucket = new BucketExistsArgs()
            .WithBucket(bucketName);

        return await _minioClient.BucketExistsAsync(bucket, cancellationToken);
    }
}
