using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Error;

namespace PetFamily.Domain.VolunteerManagement.ValueObjects;

public record SocialNetwork
{
    //ef core
    private SocialNetwork() { }

    private SocialNetwork(string name, string link)
    {
        Name = name;
        Link = link;
    }

    public string Name { get; }
    public string Link { get; }

    public static Result<SocialNetwork, ErrorResult> Create(string name, string link)
    {
        if (string.IsNullOrWhiteSpace(name) ||
            name.Length > Constants.SocialNetwork.MAX_NAME_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(name));

        if (string.IsNullOrWhiteSpace(link) ||
            name.Length > Constants.SocialNetwork.MAX_LINK_LENGTH)
            return Errors.General.ValueIsInvalid(nameof(link));

        return new SocialNetwork(name, link);
    }
}