import type { CreateAndUpdateSpatialDataDto, GetSpatialDataListDto, SpatialDataDto, SpatialDataLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SpatialDataService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateSpatialDataDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SpatialDataDto>({
      method: 'POST',
      url: '/api/app/spatial-data',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/spatial-data/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/spatial-data/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SpatialDataDto>({
      method: 'GET',
      url: `/api/app/spatial-data/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getGeoJson = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, string[]>({
      method: 'GET',
      url: '/api/app/spatial-data/geo-json',
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetSpatialDataListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SpatialDataDto>>({
      method: 'GET',
      url: '/api/app/spatial-data',
      params: { keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<SpatialDataLookupDto>>({
      method: 'GET',
      url: '/api/app/spatial-data/lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateAndUpdateSpatialDataDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SpatialDataDto>({
      method: 'PUT',
      url: `/api/app/spatial-data/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
