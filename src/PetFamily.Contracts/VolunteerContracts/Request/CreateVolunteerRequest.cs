﻿using PetFamily.Contracts.VolunteerContracts.DTOs;

namespace PetFamily.Contracts.VolunteerContracts.Request;

public record CreateVolunteerRequest(
    FullnameDto Fullname,
    EmailDto Email,
    DescriptionDto Description,
    int EmployeeExperience,
    TelephoneNumberDto TelephoneNumber,
    IEnumerable<SocialNetworkDto>? SocialNetworks = null,
    IEnumerable<RequisitDto>? Requisits = null);
