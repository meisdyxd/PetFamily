namespace PetFamily.Application.Channels.Models;

public sealed record FileMetadata
{
    public FileMetadata(string filename, string bucket)
    {
        Filename = filename;
        Bucket = bucket;
    }

    public string Filename { get; init; }
    public string Bucket { get; init; }
}
