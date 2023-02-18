import type { GetIdentityUsersInput, IdentityUserDto, IdentityUserUpdateDto } from '../volo/abp/identity/models';
import type { EntityDto } from '@abp/ng.core';

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
  userInfo: UserInfoDto;
}

export interface UserInfoDto extends EntityDto<string> {
  userId?: string;
  dob?: string;
}
