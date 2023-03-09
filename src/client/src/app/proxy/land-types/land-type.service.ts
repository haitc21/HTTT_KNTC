import type { CreateAndUpdateLandTypeDto, GetLandTypeListDto, LandTypeDto, LandTypeLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LandTypeService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateLandTypeDto) =>
    this.restService.request<any, LandTypeDto>({
      method: 'POST',
      url: '/api/app/land-type',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/land-type/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/land-type/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, LandTypeDto>({
      method: 'GET',
      url: `/api/app/land-type/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetLandTypeListDto) =>
    this.restService.request<any, PagedResultDto<LandTypeDto>>({
      method: 'GET',
      url: '/api/app/land-type',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getLookup = () =>
    this.restService.request<any, ListResultDto<LandTypeLookupDto>>({
      method: 'GET',
      url: '/api/app/land-type/lookup',
    },
    { apiName: this.apiName });
  

  update = (id: number, input: CreateAndUpdateLandTypeDto) =>
    this.restService.request<any, LandTypeDto>({
      method: 'PUT',
      url: `/api/app/land-type/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
