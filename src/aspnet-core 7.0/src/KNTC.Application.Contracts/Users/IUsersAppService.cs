using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace KNTC.Users;

public interface IUsersAppService
{
    Task<UserDto> GetAsync(Guid id);

    Task<PagedResultDto<UserListDto>> GetListAsync(GetUserListDto input);

    Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);

    Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync(Guid id);

    Task<UserDto> GetUserInfoAsync(Guid id);

    Task<IdentityUserDto> CreateAsync(CreateAndUpdateUserDto input);

    Task<IdentityUserDto> UpdateAsync(Guid id, CreateAndUpdateUserDto input);

    Task<UserInfoDto> UpdateUserInfoAsync(Guid userId, CreateAndUpdateUserDto input);

    Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);

    Task DeleteAsync(Guid id);

    Task DeleteMultipleAsync(IEnumerable<Guid> ids);

    Task SetPasswordAsync(Guid userId, SetPasswordDto input);

    Task<string> UploadAvatarAsync(IFormFile file);

    Task<byte[]> GetAvatarAsync(Guid? userId);
}