using System;
using Volo.Abp.Identity;
using System.Collections.Generic;

namespace KNTC.Users;

public class GetUserListDto : GetIdentityUsersInput
{
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Guid? RoleId { get; set; }
}