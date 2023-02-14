using System;
using Volo.Abp.Application.Dtos;

namespace WebBase.Users;

public class UserInfoDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Dob { get; set; }
    public string Avatar { get; set; }
}