using System.ComponentModel.DataAnnotations;

namespace PetFamily.Infrastructure.BackgroundServices.Options;

public class DeleteTrashMinioOptions
{
    public static string SectionName = "DeleteTrashMinio";
    [Required]
    public int RepeatTimeHours { get; set; }
}
