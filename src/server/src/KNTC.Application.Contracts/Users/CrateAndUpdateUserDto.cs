using System;
using Volo.Abp.Identity;

namespace KNTC.Users;

public class CrateAndUpdateUserDto : IdentityUserUpdateDto
{
    public DateTime? Dob { get; set; }
}