import type { GetIdentityUsersInput, IdentityUserDto, IdentityUserUpdateDto } from '../volo/abp/identity/models';
import type { EntityDto } from '@abp/ng.core';

export interface CreateAndUpdateUserDto extends IdentityUserUpdateDto {
  dob?: string;
  userType?: number;
  managedUnitIds: number[];
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
  avatarContent: number[];
}

export interface UserInfoDto extends EntityDto<string> {
  userId?: string;
  dob?: string;
  userType?: number;
  managedUnitIds: number[];
}

export interface UserListDto extends EntityDto<string> {
  userName?: string;
  name?: string;
  surname?: string;
  email?: string;
  phoneNumber?: string;
  userType?: number;
  dob?: string;
  avatarContent: number[];
  isActive: boolean;
}
