using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using Volo.Abp.Identity;

namespace KNTC.Users;

public interface IUsersAppService
{
    Task<UserDto> GetAsync(Guid id);
    Task<PagedResultDto<UserListDto>> GetListAsync(GetUserListDto input);
    Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id);
    Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync(Guid id);
    Task<UserDto> GetUserInfoAsync(Guid id);
    Task<IdentityUserDto> CreateAsync(CrateAndUpdateUserDto input);
    Task<IdentityUserDto> UpdateAsync(Guid id, CrateAndUpdateUserDto input);
    Task<UserInfoDto> UpdateUserInfoAsync(Guid userId, CrateAndUpdateUserDto input);
    Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input);
    Task DeleteAsync(Guid id);
    Task DeleteMultipleAsync(IEnumerable<Guid> ids);
    Task SetPasswordAsync(Guid userId, SetPasswordDto input);
    Task<string> UploadAvatarAsync(IRemoteStreamContent file);
    Task<byte[]> GetAvatarAsync(Guid? userId);
}