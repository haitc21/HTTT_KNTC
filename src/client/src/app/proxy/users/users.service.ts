import type { CreateAndUpdateUserDto, GetUserListDto, SetPasswordDto, UserDto, UserInfoDto, UserListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';
import type { ChangePasswordInput } from '../volo/abp/account/models';
import type { IdentityRoleDto, IdentityUserDto, IdentityUserUpdateRolesDto } from '../volo/abp/identity/models';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  apiName = 'Default';
  

  changePassword = (input: ChangePasswordInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/users/change-password',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  create = (input: CreateAndUpdateUserDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/users',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/users/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/users/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UserDto>({
      method: 'GET',
      url: `/api/app/users/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getAssignableRoles = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<IdentityRoleDto>>({
      method: 'GET',
      url: `/api/app/users/${id}/assignable-roles`,
    },
    { apiName: this.apiName,...config });
  

  getAvatar = (userId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: `/api/app/users/avatar/${userId}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetUserListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<UserListDto>>({
      method: 'GET',
      url: '/api/app/users',
      params: { email: input.email, phoneNumber: input.phoneNumber, roleId: input.roleId, filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getRoles = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<IdentityRoleDto>>({
      method: 'GET',
      url: `/api/app/users/${id}/roles`,
    },
    { apiName: this.apiName,...config });
  

  getUserInfo = (userId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UserDto>({
      method: 'GET',
      url: `/api/app/users/user-info/${userId}`,
    },
    { apiName: this.apiName,...config });
  

  register = (input: CreateAndUpdateUserDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'POST',
      url: '/api/app/users/register',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  setPassword = (userId: string, input: SetPasswordDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/users/set-password/${userId}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateAndUpdateUserDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, IdentityUserDto>({
      method: 'PUT',
      url: `/api/app/users/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateRoles = (id: string, input: IdentityUserUpdateRolesDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/users/${id}/roles`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  updateUserInfo = (userId: string, input: CreateAndUpdateUserDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UserInfoDto>({
      method: 'PUT',
      url: `/api/app/users/user-info/${userId}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  uploadAvatar = (file: IFormFile, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: '/api/app/users/upload-avatar',
      body: file,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
