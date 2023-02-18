using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace KNTC.Users;

public class UserInfoDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }
    public DateTime Dob { get; set; }
}