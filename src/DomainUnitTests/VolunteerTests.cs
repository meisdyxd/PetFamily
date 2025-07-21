using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerManagement;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace DomainUnitTests;

public class VolunteerTests
{
    private Volunteer CreateVolunteer()
    {
        var fullname = FullName.Create("������", "�����", null).Value;
        var email = Email.Create("test@example.com").Value;
        var description = Description.Create(null).Value;
        var experience = EmployeeExperience.Create(5).Value;
        var phone = TelephoneNumber.Create("79999999999").Value;

        return Volunteer.Create(
            fullname,
            email,
            description,
            experience,
            phone,
            null,
            null
        ).Value;
    }

    private Pet CreatePet(string moniker)
    {
        var species = new Species(Guid.NewGuid());
        var breed = new Breed(Guid.NewGuid());
        var description = Description.Create(null).Value;
        var address = Address.Create("������", "������", "�����", "�����", "1").Value;
        var phone = TelephoneNumber.Create("79999999999").Value;

        return Pet.Create(
            moniker,
            species,
            description,
            breed,
            "�����",
            "������",
            address,
            10.0,
            20.0,
            phone,
            false,
            DateTime.UtcNow.AddYears(-1),
            true
        ).Value;
    }

    private Volunteer CreateVolunteerWithPets(params string[] monikers)
    {
        var volunteer = CreateVolunteer();
        foreach (var name in monikers)
        {
            volunteer.AddPet(CreatePet(name));
        }
        return volunteer;
    }


    [Fact]
    public void AddPet_FirstPet_SequenceNumberIs1()
    {
        var volunteer = CreateVolunteer();
        var pet = CreatePet("���");

        volunteer.AddPet(pet);

        Assert.Equal(1, pet.SequenceNumber);
        Assert.Single(volunteer.Pets);
    }

    [Fact]
    public void AddPet_MultiplePets_IncrementsSequenceNumber()
    {
        var volunteer = CreateVolunteer();
        var pet1 = CreatePet("���");
        var pet2 = CreatePet("������");

        volunteer.AddPet(pet1);
        volunteer.AddPet(pet2);

        Assert.Equal(1, pet1.SequenceNumber);
        Assert.Equal(2, pet2.SequenceNumber);
    }

    [Fact]
    public void RemovePet_DeletesPetAndReordersSequence()
    {
        var volunteer = CreateVolunteerWithPets("���", "������", "����");
        var petToRemove = volunteer.Pets[1]; // ������

        volunteer.RemovePet(petToRemove);

        Assert.True(petToRemove.IsDeleted);
        Assert.Equal(-1, petToRemove.SequenceNumber);

        Assert.Equal(1, volunteer.Pets[0].SequenceNumber); // ���
        Assert.Equal(2, volunteer.Pets[2].SequenceNumber); // ����
    }

    [Fact]
    public void RemovePet_LastPet_DoesNotReorder()
    {
        var volunteer = CreateVolunteerWithPets("���");
        var petToRemove = volunteer.Pets[0];

        volunteer.RemovePet(petToRemove);

        Assert.True(petToRemove.IsDeleted);
        Assert.Equal(-1, petToRemove.SequenceNumber);
    }

    [Fact]
    public void MovePetToBegin_MovesToFirstPosition()
    {
        var volunteer = CreateVolunteerWithPets("���", "������", "����");
        var petToMove = volunteer.Pets[2]; // ���� (Seq=3)

        volunteer.MovePetToBegin(petToMove);

        // �������� �������
        Assert.Equal(1, petToMove.SequenceNumber);
        Assert.Equal("���", volunteer.Pets[0].Moniker);
        Assert.Equal(3, volunteer.Pets[1].SequenceNumber);
        Assert.Equal(1, volunteer.Pets[2].SequenceNumber);
    }

    [Fact]
    public void MovePetToBegin_AlreadyFirst_NoChanges()
    {
        var volunteer = CreateVolunteerWithPets("���", "������");
        var firstPet = volunteer.Pets[0];

        volunteer.MovePetToBegin(firstPet);

        Assert.Equal(1, firstPet.SequenceNumber);
        Assert.Equal(2, volunteer.Pets[1].SequenceNumber);
    }

    [Fact]
    public void MovePetToEnd_MovesToLastPosition()
    {
        var volunteer = CreateVolunteerWithPets("���", "������", "����");
        var petToMove = volunteer.Pets[0]; // ���

        volunteer.MovePetToEnd(petToMove);

        Assert.Equal(3, petToMove.SequenceNumber);
        Assert.Equal("���", volunteer.Pets[0].Moniker);
        Assert.Equal(1, volunteer.Pets[1].SequenceNumber); 
        Assert.Equal(2, volunteer.Pets[2].SequenceNumber); 
    }

    [Fact]
    public void MovePetToEnd_AlreadyLast_NoChanges()
    {
        var volunteer = CreateVolunteerWithPets("���", "������");
        var lastPet = volunteer.Pets[1];

        volunteer.MovePetToEnd(lastPet);

        Assert.Equal(2, lastPet.SequenceNumber);
        Assert.Equal(1, volunteer.Pets[0].SequenceNumber);
    }

    [Fact]
    public void MovePetToPosition_ValidPosition_ReordersPets()
    {
        var volunteer = CreateVolunteerWithPets("���", "������", "����");
        var petToMove = volunteer.Pets[0];

        volunteer.MovePetToPosition(petToMove, 2);

        Assert.Equal(2, petToMove.SequenceNumber);
        Assert.Equal("���", volunteer.Pets[0].Moniker); 
        Assert.Equal("������", volunteer.Pets[1].Moniker); 
        Assert.Equal("����", volunteer.Pets[2].Moniker);
        Assert.Equal(1, volunteer.Pets[1].SequenceNumber);
        Assert.Equal(3, volunteer.Pets[2].SequenceNumber);
    }

    [Fact]
    public void MovePetToPosition_InvalidPosition_ReturnsError()
    {
        var volunteer = CreateVolunteerWithPets("���", "������");
        var petToMove = volunteer.Pets[0];

        var result = volunteer.MovePetToPosition(petToMove, 5);

        Assert.True(result.IsFailure);
    }

    [Fact]
    public void RestorePet_RestoresDeletedPet()
    {
        var volunteer = CreateVolunteerWithPets("���");
        var pet = volunteer.Pets[0];

        // ������� ������� ��� ������� (����� ����� ���� ������������)
        pet.Delete(cascade: false);
        pet.SetSequenceNumber(-1);

        volunteer.RestorePet(pet, innnerCascadeDeleted: true);

        Assert.False(pet.IsDeleted);
        Assert.Equal(1, pet.SequenceNumber); // ����� �����
    }
}