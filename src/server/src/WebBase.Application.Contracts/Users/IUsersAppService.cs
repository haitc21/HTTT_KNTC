﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace WebBase.Users;

public interface IUsersAppService
{
    Task DeleteMultipleAsync(IEnumerable<Guid> ids);

    Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);

    Task SetPasswordAsync(Guid userId, SetPasswordDto input);
    Task<UserInfoDto> GetUserInfoAsync(Guid id);
}