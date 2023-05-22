using System;
using Volo.Abp.Identity;

namespace KNTC.Users;

public class GetUserListDto : GetIdentityUsersInput
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid? roleId { get; set; }
}