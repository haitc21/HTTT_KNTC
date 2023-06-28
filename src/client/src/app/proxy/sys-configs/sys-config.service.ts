import type { CreateSysConfigDto, GetSysConfigListDto, SysConfigCacheItem, SysConfigDto, UpdateSysConfigDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SysConfigService {
  apiName = 'Default';
  

  create = (input: CreateSysConfigDto) =>
    this.restService.request<any, SysConfigDto>({
      method: 'POST',
      url: '/api/app/sys-config',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/sys-config/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/sys-config/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, SysConfigDto>({
      method: 'GET',
      url: `/api/app/sys-config/${id}`,
    },
    { apiName: this.apiName });
  

  getByName = (name: string) =>
    this.restService.request<any, SysConfigCacheItem>({
      method: 'GET',
      url: '/api/app/sys-config/by-name',
      params: { name },
    },
    { apiName: this.apiName });
  

  getList = (input: GetSysConfigListDto) =>
    this.restService.request<any, PagedResultDto<SysConfigDto>>({
      method: 'GET',
      url: '/api/app/sys-config',
      params: { keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: number, input: UpdateSysConfigDto) =>
    this.restService.request<any, SysConfigDto>({
      method: 'PUT',
      url: `/api/app/sys-config/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
