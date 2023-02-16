using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace WebBase.Users;

public class UserInfoDto : ExtensibleFullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Dob { get; set; }
    public string ConcurrencyStamp { get; set; }
}