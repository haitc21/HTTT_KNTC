using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Identity;

namespace WebBase.Users;

public class UpdateUserInfoDto : IdentityUserUpdateDto
{
    public DateTime Dob { get; set; }
}