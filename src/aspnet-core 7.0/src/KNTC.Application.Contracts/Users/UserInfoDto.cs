using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace KNTC.Users;

public class UserInfoDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }    
    public DateTime Dob { get; set; }
    public int? userType { get; set; }
    public int[]? managedUnitIds { get; set; }
}