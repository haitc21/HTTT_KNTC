import type { GetIdentityUsersInput, IdentityUserDto, IdentityUserUpdateDto } from '../volo/abp/identity/models';
import type { ExtensibleFullAuditedEntityDto } from '@abp/ng.core';

export interface CrateAndUpdateUserDto extends IdentityUserUpdateDto {
  dob?: string;
}

export interface GetUserListDto extends GetIdentityUsersInput {
  email?: string;
  phoneNumber?: string;
  roleId?: string;
}

export interface SetPasswordDto {
  newPassword?: string;
  confirmNewPassword?: string;
}

export interface UserDto extends IdentityUserDto {
  roles: string[];
  dob?: string;
}

export interface UserInfoDto extends ExtensibleFullAuditedEntityDto<string> {
  userId?: string;
  name?: string;
  surname?: string;
  email?: string;
  userName?: string;
  phoneNumber?: string;
  dob?: string;
  concurrencyStamp?: string;
}
