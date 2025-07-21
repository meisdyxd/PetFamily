using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace DomainUnitTests;

public class VolunteerTest
{
    [Fact]
    public void IsFirst_MoveToBegin()
    {
        var fullname = FullName.Create("Карлов", "Артур", null).Value;
        var description = Description.Create(null).Value;
        var email = Email.Create("kararturkar@gmail.com").Value;
        var employeeExp = EmployeeExperience.Create(5).Value;
        var telephone = TelephoneNumber.Create("79527584996").Value;

        var volunteer = Volunteer.Create(
            fullname,
            email,
            description,
            employeeExp,
            telephone,
            null,
            null).Value;

        var moniker = "Хан";
        var species = new Species(Guid.NewGuid());
        var breed = new Breed(Guid.NewGuid());
        var coloration = "Бело-рыжий";
        var healthInfo = "Сердечная недостаточность";
        var address = Address.Create("Россия", "Чувашская республика", "Чебоксары", "ул. Фруктовая, д. 8", "7").Value;
        var weight = 35;
        var height = 50;
        var isCastrated = false;
        var birthdate = DateTime.UtcNow;
        var isVaccinated = true;

        var pet1 = Pet.Create(
            moniker,
            species,
            description,
            breed,
            coloration,
            healthInfo,
            address,
            weight,
            height,
            telephone,
            isCastrated,
            birthdate,
            isVaccinated).Value;

        var pet2 = Pet.Create(
            "Марсик",
            species,
            description,
            breed,
            coloration,
            healthInfo,
            address,
            weight,
            height,
            telephone,
            isCastrated,
            birthdate,
            isVaccinated).Value;

        var pet3 = Pet.Create(
            "Кеша",
            species,
            description,
            breed,
            coloration,
            healthInfo,
            address,
            weight,
            height,
            telephone,
            isCastrated,
            birthdate,
            isVaccinated).Value;

        volunteer.AddPet(pet1);
        volunteer.AddPet(pet2);
        volunteer.AddPet(pet3);
        
        volunteer.RemovePet(volunteer.Pets[1]);
        var sf = "ds";
        volunteer.RestorePet(volunteer.Pets[1], true);
        var bb = "sdf";
        Assert.True(volunteer.Pets[1].Moniker == "Марсик" && volunteer.Pets[0].Moniker == "Хан" && volunteer.Pets[1].SequenceNumber == 1 && volunteer.Pets[0].SequenceNumber == 2);
    }
}