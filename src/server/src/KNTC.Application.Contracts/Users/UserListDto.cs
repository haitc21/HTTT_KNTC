using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.Users;

public class UserListDto : EntityDto<Guid>
{
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Dob { get; set; }
    public byte[] AvatarContent { get; set; }
    public bool IsActive { get; set; }
}