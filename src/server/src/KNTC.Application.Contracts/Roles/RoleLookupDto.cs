using System;
using Volo.Abp.Application.Dtos;

namespace KNTC.Roles;

public class RoleLookupDto : EntityDto<Guid>
{
    public string Name { get; set; }
}