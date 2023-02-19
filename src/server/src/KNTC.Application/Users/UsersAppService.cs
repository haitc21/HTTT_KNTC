using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Users;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace KNTC.Users;

public class UsersAppService : IdentityAppServiceBase, IUsersAppService
{
    protected IdentityUserManager UserManager { get; }
    protected IIdentityUserRepository UserRepository { get; }
    protected IRepository<IdentityRole,Guid> RoleRepository { get; }
    protected IOptions<IdentityOptions> IdentityOptions { get; }
    protected IBlobContainer<AvatarContainer> _fileContainer { get; }
    protected IRepository<UserInfo> _userInfoRepo { get; }

    public UsersAppService(
        IdentityUserManager userManager,
        IIdentityUserRepository userRepository,
        IRepository<IdentityRole, Guid> roleRepository,
        IOptions<IdentityOptions> identityOptions,
        IBlobContainer<AvatarContainer> fileContainer,
        IRepository<UserInfo> userInfoRepo)
    {
        UserManager = userManager;
        UserRepository = userRepository;
        RoleRepository = roleRepository;
        IdentityOptions = identityOptions;
        _fileContainer = fileContainer;
        _userInfoRepo = userInfoRepo;
    }

    [Authorize(IdentityPermissions.Users.Default)]
    public virtual async Task<UserDto> GetAsync(Guid id)
    {
        var identityUser = await UserManager.GetByIdAsync(id);
        var result = ObjectMapper.Map<IdentityUser, UserDto>(
            identityUser
        );
        var userInfo = await _userInfoRepo.GetAsync(x => x.UserId == id);
        result.UserInfo = ObjectMapper.Map<UserInfo, UserInfoDto>(userInfo);
        return result;
    }


    [Authorize(IdentityPermissions.Users.Default)]
    public virtual async Task<PagedResultDto<UserDto>> GetListAsync(GetUserListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(IdentityUser.UserName);
        }
        var count = await UserRepository.GetCountAsync(input.Filter,
                                                      input.roleId, null,
                                                      input.PhoneNumber,
                                                      input.Email);
        var list = await UserRepository.GetListAsync(
                                        input.Sorting,
                                        input.MaxResultCount,
                                        input.SkipCount,
                                        input.Filter,
                                        false,
                                        input.roleId,
                                        null,
                                        null,
                                        input.PhoneNumber,
                                        input.Email);
        
        var result = new PagedResultDto<UserDto>(
            count,
            ObjectMapper.Map<List<IdentityUser>, List<UserDto>>(list)
        );
        foreach (var item in result.Items)
        {
            var userInfo = await _userInfoRepo.GetAsync(x => x.UserId == item.Id);
            item.UserInfo = ObjectMapper.Map<UserInfo, UserInfoDto>(userInfo);
        }
        return result;
    }

    [Authorize(IdentityPermissions.Users.Default)]
    public virtual async Task<ListResultDto<IdentityRoleDto>> GetRolesAsync(Guid id)
    {
        //TODO: Should also include roles of the related OUs.

        var roles = await UserRepository.GetRolesAsync(id);

        return new ListResultDto<IdentityRoleDto>(
            ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles)
        );
    }

    [Authorize(IdentityPermissions.Users.Default)]
    public virtual async Task<ListResultDto<IdentityRoleDto>> GetAssignableRolesAsync(Guid id)
    {
        var assignEdRoles = (await UserRepository.GetRolesAsync(id)).Select(x => x.Id);
        var roles = await RoleRepository.GetListAsync(x => !assignEdRoles.Contains(x.Id));
        return new ListResultDto<IdentityRoleDto>(
            ObjectMapper.Map<List<IdentityRole>, List<IdentityRoleDto>>(roles));
    }
    [Authorize]
    public async Task<UserDto> GetUserInfoAsync(Guid userId)
    {
        var identityUser = await UserManager.GetByIdAsync(userId);
        var result = ObjectMapper.Map<IdentityUser, UserDto>(
            identityUser
        );
        var userInfo = await _userInfoRepo.GetAsync(x => x.UserId == userId);
        result.UserInfo = ObjectMapper.Map<UserInfo, UserInfoDto>(userInfo);
        return result;
    }
    [Authorize(IdentityPermissions.Users.Create)]
    public virtual async Task<IdentityUserDto> CreateAsync(CrateAndUpdateUserDto input)
    {
        await IdentityOptions.SetAsync();

        var user = new IdentityUser(
            GuidGenerator.Create(),
            input.UserName,
            input.Email
        );
        // add default roles
        var roleDefault = await RoleRepository.GetListAsync(x => x.IsDefault);
        if (roleDefault != null)
        {
            input.RoleNames = roleDefault.Select(x => x.Name).ToArray();
        }
        input.MapExtraPropertiesTo(user);

        (await UserManager.CreateAsync(user, input.Password)).CheckErrors();
        await UpdateUserFromInput(user, input);
        (await UserManager.UpdateAsync(user)).CheckErrors();

        var userInfo = new UserInfo(GuidGenerator.Create())
        {
            UserId = user.Id
        };
        await _userInfoRepo.InsertAsync(userInfo);

        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
    }

    [Authorize(IdentityPermissions.Users.Update)]
    public virtual async Task<IdentityUserDto> UpdateAsync(Guid id, CrateAndUpdateUserDto input)
    {
        await IdentityOptions.SetAsync();

        var user = await UserManager.GetByIdAsync(id);

        user.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);

        (await UserManager.SetUserNameAsync(user, input.UserName)).CheckErrors();

        await UpdateUserFromInput(user, input);
        input.MapExtraPropertiesTo(user);

        (await UserManager.UpdateAsync(user)).CheckErrors();

        if (!input.Password.IsNullOrEmpty())
        {
            (await UserManager.RemovePasswordAsync(user)).CheckErrors();
            (await UserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
        }
        var userInfo = await _userInfoRepo.GetAsync(x => x.UserId == id);
        userInfo.Dob = input.Dob;
        await _userInfoRepo.UpdateAsync(userInfo);
        await CurrentUnitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
    }
    [Authorize]
    public async Task<UserInfoDto> UpdateUserInfoAsync(Guid userId, CrateAndUpdateUserDto input)
    {
        await hasViewUserInfo(userId);
        var user = await UserManager.GetByIdAsync(userId);
        user.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        user.Name = input.Name;
        user.Surname = input.Surname;
        if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
        {
            (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
        }
        input.MapExtraPropertiesTo(user);
        (await UserManager.UpdateAsync(user)).CheckErrors();

        var userInfo = await _userInfoRepo.GetAsync(x => x.UserId == userId);
        userInfo.Dob = input.Dob;
        await _userInfoRepo.UpdateAsync(userInfo);
        await UnitOfWorkManager.Current.SaveChangesAsync();
        return ObjectMapper.Map<UserInfo, UserInfoDto>(userInfo);
    }

    [Authorize(IdentityPermissions.Users.Update)]
    public virtual async Task UpdateRolesAsync(Guid id, IdentityUserUpdateRolesDto input)
    {
        var user = await UserManager.GetByIdAsync(id);
        (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
        await UserRepository.UpdateAsync(user);
    }


    [Authorize(IdentityPermissions.Users.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        if (CurrentUser.Id == id)
        {
            throw new BusinessException(code: IdentityErrorCodes.UserSelfDeletion);
        }

        var user = await UserManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return;
        }

        (await UserManager.DeleteAsync(user)).CheckErrors();
        var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == id);
        if (userInfo != null)
        {
            await _userInfoRepo.DeleteAsync(userInfo);
        }
        await CurrentUnitOfWork.SaveChangesAsync();
    }

    [Authorize(IdentityPermissions.Users.Delete)]
    public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
    {
        await UserRepository.DeleteManyAsync(ids);
        foreach (var id in ids)
        {
            var userInfo = await _userInfoRepo.FindAsync(x => x.UserId == id);
            if (userInfo != null)
            {
                await _userInfoRepo.DeleteAsync(userInfo);
            }
        }
        await UnitOfWorkManager.Current.SaveChangesAsync();
    }

    [Authorize(IdentityPermissions.Users.Update)]
    public async Task SetPasswordAsync(Guid userId, SetPasswordDto input)
    {
        var user = await UserManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new EntityNotFoundException(typeof(IdentityUser), userId);
        }
        var token = await UserManager.GeneratePasswordResetTokenAsync(user);
        var result = await UserManager.ResetPasswordAsync(user, token, input.NewPassword);
        if (!result.Succeeded)
        {
            List<Microsoft.AspNetCore.Identity.IdentityError> errorList = result.Errors.ToList();
            string errors = "";

            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToString();
            }
            throw new UserFriendlyException(errors);
        }
    }

    [Authorize]
    public async Task<string> UploadAvatarAsync(IRemoteStreamContent file)
    {
        if (file == null) throw new UserFriendlyException("Vui lòng chộn ảnh đại diện");
        try
        {
            //var blobName = String.Concat(CurrentUser.GetId().ToString(), ".", GetFileExtension(avatarStream.FileName));
            var blobName = CurrentUser.GetId().ToString();
            var stream = file.GetStream();
            await _fileContainer.SaveAsync(blobName, stream, overrideExisting: true);
            return blobName;
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException(ex.Message);
        }

    }
    [Authorize]
    public async Task<byte[]> GetAvatarAsync(Guid? userId)
    {
        userId = userId ?? CurrentUser.GetId();
        return await _fileContainer.GetAllBytesOrNullAsync(userId.ToString());
    }

    #region private method
    private async Task hasViewUserInfo(Guid userId)
    {
        var curUserId = CurrentUser.GetId();
        if (curUserId != userId)
        {
            var roles = await UserRepository.GetRolesAsync(curUserId);
            if (!roles.Any(x => x.Name.Contains("admin")))
            {
                throw new AbpException("bạn không có quyền xem thông tin cá nhân của ngời khác");
            }

        }
    }

    protected virtual async Task UpdateUserFromInput(IdentityUser user, IdentityUserCreateOrUpdateDtoBase input)
    {
        if (!string.Equals(user.Email, input.Email, StringComparison.InvariantCultureIgnoreCase))
        {
            (await UserManager.SetEmailAsync(user, input.Email)).CheckErrors();
        }

        if (!string.Equals(user.PhoneNumber, input.PhoneNumber, StringComparison.InvariantCultureIgnoreCase))
        {
            (await UserManager.SetPhoneNumberAsync(user, input.PhoneNumber)).CheckErrors();
        }

        (await UserManager.SetLockoutEnabledAsync(user, input.LockoutEnabled)).CheckErrors();

        user.Name = input.Name;
        user.Surname = input.Surname;
        (await UserManager.UpdateAsync(user)).CheckErrors();
        user.SetIsActive(input.IsActive);
        if (input.RoleNames != null)
        {
            (await UserManager.SetRolesAsync(user, input.RoleNames)).CheckErrors();
        }
    }
    private static string GetFileExtension(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            return string.Empty;
        }

        int lastDotIndex = fileName.LastIndexOf('.');
        if (lastDotIndex == -1)
        {
            return string.Empty;
        }

        return fileName.Substring(lastDotIndex + 1);
    }
    #endregion
}