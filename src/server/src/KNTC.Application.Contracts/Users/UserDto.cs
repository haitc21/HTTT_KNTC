using System.Collections.Generic;
using Volo.Abp.Identity;

namespace KNTC.Users;

public class UserDto : IdentityUserDto
{
    public IList<string> Roles { get; set; }
    public UserInfoDto UserInfo { get; set; }
    public byte[] AvatarContent { get; set; }
}