using Microsoft.EntityFrameworkCore;
using PetFamily.Application.VolunteerModule.Models;
using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Application.Interfaces;

public interface IReadDbContext
{
    IQueryable<VolunteerDto> Volunteers { get; }
    IQueryable<SpeciesDto> Species { get; }
}