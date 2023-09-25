import type { BaseMapDto, BaseMapLookupDto, CreateAndUpdateBaseMapDto, GetBaseMapListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BaseMapService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateBaseMapDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BaseMapDto>({
      method: 'POST',
      url: '/api/app/base-map',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/base-map/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/base-map/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BaseMapDto>({
      method: 'GET',
      url: `/api/app/base-map/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetBaseMapListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<BaseMapDto>>({
      method: 'GET',
      url: '/api/app/base-map',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<BaseMapLookupDto>>({
      method: 'GET',
      url: '/api/app/base-map/lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateAndUpdateBaseMapDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, BaseMapDto>({
      method: 'PUT',
      url: `/api/app/base-map/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
