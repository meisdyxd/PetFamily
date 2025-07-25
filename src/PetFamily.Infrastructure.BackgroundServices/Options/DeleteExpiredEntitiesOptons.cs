﻿using System.ComponentModel.DataAnnotations;

namespace PetFamily.Infrastructure.BackgroundServices.DeleteExpiredEntities;

public class DeleteExpiredEntitiesOptons
{
    public static string SectionName = "DeleteExpiredEntities";
    [Required]
    public int LifeTimeDays { get; set; }
    [Required]
    public int RepeatTimeHours { get; set; }
}
