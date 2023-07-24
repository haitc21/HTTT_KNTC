import type { AllSysConfigCacheItem, CreateSysConfigDto, GetSysConfigListDto, SysConfigCacheItem, SysConfigDto, UpdateSysConfigDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SysConfigService {
  apiName = 'Default';
  

  create = (input: CreateSysConfigDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SysConfigDto>({
      method: 'POST',
      url: '/api/app/sys-config',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/sys-config/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/sys-config/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SysConfigDto>({
      method: 'GET',
      url: `/api/app/sys-config/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getAllConfigs = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, AllSysConfigCacheItem>({
      method: 'GET',
      url: '/api/app/sys-config/configs',
    },
    { apiName: this.apiName,...config });
  

  getByName = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SysConfigCacheItem>({
      method: 'GET',
      url: '/api/app/sys-config/by-name',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSysConfigListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SysConfigDto>>({
      method: 'GET',
      url: '/api/app/sys-config',
      params: { keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: UpdateSysConfigDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SysConfigDto>({
      method: 'PUT',
      url: `/api/app/sys-config/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
