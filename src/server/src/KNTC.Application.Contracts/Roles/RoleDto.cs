﻿using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KNTC.Roles;

public class RoleDto : EntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public bool IsPublic { get; set; }
    public string? Description { get; set; }
    public string? ConcurrencyStamp { get; set; }
}