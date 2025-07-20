using FluentValidation;
using PetFamily.Application.Validations;
using PetFamily.Application.VolunteerModule.UseCases.UpdateSocialNetworksVolunteer;
using PetFamily.Domain.VolunteerManagement.ValueObjects;

namespace PetFamily.Application.VolunteerModule.ValidationRules;

public class UpdateSocialNetworksCommandValidator : AbstractValidator<UpdateSocialNetworksCommand>
{
    public UpdateSocialNetworksCommandValidator()
    {
        RuleForEach(v => v.SocialNetworks)
            .MustBeValueObject(sn => SocialNetwork.Create(sn.Name, sn.Link));
    }
}
