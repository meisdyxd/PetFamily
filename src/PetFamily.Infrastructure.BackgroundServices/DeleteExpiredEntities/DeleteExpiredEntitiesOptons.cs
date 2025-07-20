namespace PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;

public class DeleteExpiredEntitiesOptons
{
    public static string SectionName = "DeleteExpiredEntities";
    public int LifeTimeDays { get; set; }
}
