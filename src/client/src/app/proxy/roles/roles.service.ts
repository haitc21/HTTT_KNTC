import type { CreateUpdateRoleDto, RoleDto, RoleLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto, PagedResultRequestDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { BaseListFilterDto } from '../models';
import type { GetPermissionListResultDto, UpdatePermissionsDto } from '../volo/abp/permission-management/models';

@Injectable({
  providedIn: 'root',
})
export class RolesService {
  apiName = 'Default';
  

  create = (input: CreateUpdateRoleDto) =>
    this.restService.request<any, RoleDto>({
      method: 'POST',
      url: '/api/app/roles',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/roles/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/roles/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, RoleDto>({
      method: 'GET',
      url: `/api/app/roles/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: PagedResultRequestDto) =>
    this.restService.request<any, PagedResultDto<RoleDto>>({
      method: 'GET',
      url: '/api/app/roles',
      params: { maxResultCount: input.maxResultCount, skipCount: input.skipCount },
    },
    { apiName: this.apiName });
  

  getListAll = () =>
    this.restService.request<any, RoleDto[]>({
      method: 'GET',
      url: '/api/app/roles/all',
    },
    { apiName: this.apiName });
  

  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<RoleDto>>({
      method: 'GET',
      url: '/api/app/roles/filter',
      params: { keyword: input.keyword, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getPermissions = (providerName: string, providerKey: string) =>
    this.restService.request<any, GetPermissionListResultDto>({
      method: 'GET',
      url: '/api/app/roles/permissions',
      params: { providerName, providerKey },
    },
    { apiName: this.apiName });
  

  getRoleLookup = () =>
    this.restService.request<any, ListResultDto<RoleLookupDto>>({
      method: 'GET',
      url: '/api/app/roles/role-lookup',
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateUpdateRoleDto) =>
    this.restService.request<any, RoleDto>({
      method: 'PUT',
      url: `/api/app/roles/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updatePermissions = (providerName: string, providerKey: string, input: UpdatePermissionsDto) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: '/api/app/roles/permissions',
      params: { providerName, providerKey },
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
