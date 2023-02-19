import type { CrateAndUpdateUserDto, GetUserListDto, SetPasswordDto, UserDto, UserInfoDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IdentityRoleDto, IdentityUserDto, IdentityUserUpdateRolesDto } from '../volo/abp/identity/models';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  apiName = 'Default';
  

  create = (input: CrateAndUpdateUserDto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/users',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/users/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/users/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, UserDto>({
      method: 'GET',
      url: `/api/app/users/${id}`,
    },
    { apiName: this.apiName });
  

  getAssignableRoles = (id: string) =>
    this.restService.request<any, ListResultDto<IdentityRoleDto>>({
      method: 'GET',
      url: `/api/app/users/${id}/assignable-roles`,
    },
    { apiName: this.apiName });
  

  getAvatar = (userId?: string) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: `/api/app/users/avatar/${userId}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetUserListDto) =>
    this.restService.request<any, PagedResultDto<UserDto>>({
      method: 'GET',
      url: '/api/app/users',
      params: { email: input.email, phoneNumber: input.phoneNumber, roleId: input.roleId, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getRoles = (id: string) =>
    this.restService.request<any, ListResultDto<IdentityRoleDto>>({
      method: 'GET',
      url: `/api/app/users/${id}/roles`,
    },
    { apiName: this.apiName });
  

  getUserInfo = (userId: string) =>
    this.restService.request<any, UserDto>({
      method: 'GET',
      url: `/api/app/users/user-info/${userId}`,
    },
    { apiName: this.apiName });
  

  setPassword = (userId: string, input: SetPasswordDto) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/users/set-password/${userId}`,
      body: input,
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CrateAndUpdateUserDto) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'PUT',
      url: `/api/app/users/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateRoles = (id: string, input: IdentityUserUpdateRolesDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/users/${id}/roles`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateUserInfo = (userId: string, input: CrateAndUpdateUserDto) =>
    this.restService.request<any, UserInfoDto>({
      method: 'PUT',
      url: `/api/app/users/user-info/${userId}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
