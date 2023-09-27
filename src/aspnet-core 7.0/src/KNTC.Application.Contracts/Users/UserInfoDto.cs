using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace KNTC.Users;

public class UserInfoDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }    
    public DateTime Dob { get; set; }
    public int? UserType { get; set; }
    public int[]? ManagedUnitIds { get; set; }
}