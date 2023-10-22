import type { CreateAndUpdateUnitTypeDto, GetUnitTypeListDto, UnitTypeDto, UnitTypeLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UnitTypeService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateUnitTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UnitTypeDto>({
      method: 'POST',
      url: '/api/app/unit-type',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/unit-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/unit-type/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UnitTypeDto>({
      method: 'GET',
      url: `/api/app/unit-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetUnitTypeListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<UnitTypeDto>>({
      method: 'GET',
      url: '/api/app/unit-type',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<UnitTypeLookupDto>>({
      method: 'GET',
      url: '/api/app/unit-type/lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateAndUpdateUnitTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UnitTypeDto>({
      method: 'PUT',
      url: `/api/app/unit-type/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
