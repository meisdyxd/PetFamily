using Microsoft.AspNetCore.Mvc;
using PetFamily.API.Extensions;
using PetFamily.Application.Minio;

namespace PetFamily.API.Controllers;

public class FileController : MainController
{
    private readonly IFilesProvider _filesProvider;

    public FileController(IFilesProvider filesProvider)
    {
        _filesProvider = filesProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Upload(
        [FromRoute] string bucket,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        using var stream = file.OpenReadStream();
        
        var result = await _filesProvider.UploadFileAsync(
            stream, 
            bucket, 
            $"{Guid.NewGuid().ToString()}_{file.FileName}", 
            cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Created(true);
    }

    [HttpGet("presigned")]
    public async Task<IActionResult> GetPresigned(
        [FromQuery] string bucket,
        [FromQuery] string file,
        CancellationToken cancellationToken)
    {
        var result = await _filesProvider.GetPresignedAsync(bucket, file, cancellationToken);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromQuery] string bucket,
        [FromQuery] string file,
        CancellationToken cancellationToken)
    {
        await _filesProvider.DeleteFileAsync(bucket, file, cancellationToken);

        return NoContent();
    }
}
