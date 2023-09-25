using System;
using System.Collections.Generic;
using Volo.Abp.Identity;

namespace KNTC.Users;

public class CreateAndUpdateUserDto : IdentityUserUpdateDto
{
    public DateTime Dob { get; set; }
    public int? userType { get; set; }
    public int[]? managedUnitIds { get; set; }
}