using System;
using Volo.Abp.Identity;

namespace KNTC.Users;

public class CreateAndUpdateUserDto : IdentityUserUpdateDto
{
    public DateTime? Dob { get; set; }
    public UserType? UserType { get; set; }
    public int[]? ManagedUnitIds { get; set; }
}