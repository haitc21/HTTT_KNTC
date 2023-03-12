import type { CreateAndUpdateSpatialDataDto, GetSpatialDataListDto, SpatialDataDto, SpatialDataLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SpatialDataService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateSpatialDataDto) =>
    this.restService.request<any, SpatialDataDto>({
      method: 'POST',
      url: '/api/app/spatial-data',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/spatial-data/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/spatial-data/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, SpatialDataDto>({
      method: 'GET',
      url: `/api/app/spatial-data/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetSpatialDataListDto) =>
    this.restService.request<any, PagedResultDto<SpatialDataDto>>({
      method: 'GET',
      url: '/api/app/spatial-data',
      params: { keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getLookup = () =>
    this.restService.request<any, ListResultDto<SpatialDataLookupDto>>({
      method: 'GET',
      url: '/api/app/spatial-data/lookup',
    },
    { apiName: this.apiName });
  

  update = (id: number, input: CreateAndUpdateSpatialDataDto) =>
    this.restService.request<any, SpatialDataDto>({
      method: 'PUT',
      url: `/api/app/spatial-data/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
