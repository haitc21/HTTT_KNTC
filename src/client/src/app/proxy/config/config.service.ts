import type { CreateAndUpdateConfigDto, GetConfigListDto, ConfigDto, ConfigLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import { BaseListFilterDto } from '@proxy/models';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateConfigDto) =>
    this.restService.request<any, ConfigDto>({
      method: 'POST',
      url: '/api/app/config',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/config/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/config/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, ConfigDto>({
      method: 'GET',
      url: `/api/app/config/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetConfigListDto) =>
    this.restService.request<any, PagedResultDto<ConfigDto>>({
      method: 'GET',
      url: '/api/app/config',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  
  getListFilter = (input: BaseListFilterDto) =>
    this.restService.request<any, PagedResultDto<ConfigDto>>({
      method: 'GET',
      url: '/api/app/config/filter',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount, keyword: input.keyword },
    },
    { apiName: this.apiName });
  
  getLookup = () =>
    this.restService.request<any, ListResultDto<ConfigLookupDto>>({
      method: 'GET',
      url: '/api/app/config/lookup',
    },
    { apiName: this.apiName });
  

  update = (id: number, input: CreateAndUpdateConfigDto) =>
    this.restService.request<any, ConfigDto>({
      method: 'PUT',
      url: `/api/app/config/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
