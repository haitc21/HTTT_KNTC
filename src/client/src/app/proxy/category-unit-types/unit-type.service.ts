import type { CreateAndUpdateUnitTypeDto, GetUnitTypeListDto, UnitTypeDto, UnitTypeLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UnitTypeService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateUnitTypeDto) =>
    this.restService.request<any, UnitTypeDto>({
      method: 'POST',
      url: '/api/app/unit-type',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/unit-type/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/unit-type/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, UnitTypeDto>({
      method: 'GET',
      url: `/api/app/unit-type/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetUnitTypeListDto) =>
    this.restService.request<any, PagedResultDto<UnitTypeDto>>({
      method: 'GET',
      url: '/api/app/unit-type',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getLookup = () =>
    this.restService.request<any, ListResultDto<UnitTypeLookupDto>>({
      method: 'GET',
      url: '/api/app/unit-type/lookup',
    },
    { apiName: this.apiName });
  

  update = (id: number, input: CreateAndUpdateUnitTypeDto) =>
    this.restService.request<any, UnitTypeDto>({
      method: 'PUT',
      url: `/api/app/unit-type/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
