import type { CreateAndUpdateLandTypeDto, GetLandTypeListDto, LandTypeDto, LandTypeLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class LandTypeService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateLandTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LandTypeDto>({
      method: 'POST',
      url: '/api/app/land-type',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/land-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/land-type/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LandTypeDto>({
      method: 'GET',
      url: `/api/app/land-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetLandTypeListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<LandTypeDto>>({
      method: 'GET',
      url: '/api/app/land-type',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<LandTypeLookupDto>>({
      method: 'GET',
      url: '/api/app/land-type/lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateAndUpdateLandTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, LandTypeDto>({
      method: 'PUT',
      url: `/api/app/land-type/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
