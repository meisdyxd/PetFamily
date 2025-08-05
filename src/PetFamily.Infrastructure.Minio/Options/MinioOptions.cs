using System.ComponentModel.DataAnnotations;

namespace PetFamily.Infrastructure.Minio.Options;

public class MinioOptions
{
    public static string SectionName = "Minio";
    [Required]
    public string Endpoint { get; set; } = null!;
    [Required]
    public string AccessToken { get; set; } = null!;
    [Required]
    public string SecretKey { get; set;} = null!;
    [Required]
    public bool SSL { get; set; }
    [Required]
    public int RepeatTimeHoursClean { get; set; }
}
