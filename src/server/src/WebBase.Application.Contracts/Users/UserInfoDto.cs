using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace WebBase.Users;

public class UserInfoDto : EntityDto<Guid>
{
    public Guid UserId { get; set; }
    public DateTime Dob { get; set; }
}