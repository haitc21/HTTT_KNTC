import type { CreateAndUpdateUnitDto, GetUnitListDto, UnitDto, UnitLookupDto, UnitTreeLookupDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UnitService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateUnitDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UnitDto>({
      method: 'POST',
      url: '/api/app/unit',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/unit/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/unit/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UnitDto>({
      method: 'GET',
      url: `/api/app/unit/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetUnitListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<UnitDto>>({
      method: 'GET',
      url: '/api/app/unit',
      params: { status: input.status, unitTypeId: input.unitTypeId, parentId: input.parentId, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLookup = (unitTypeId: number, parentId?: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<UnitLookupDto>>({
      method: 'GET',
      url: '/api/app/unit/lookup',
      params: { unitTypeId, parentId },
    },
    { apiName: this.apiName,...config });
  

  getLookupByIds = (unitIds: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<UnitLookupDto>>({
      method: 'GET',
      url: '/api/app/unit/lookup-by-ids',
      params: { unitIds },
    },
    { apiName: this.apiName,...config });
  

  getLookupByParentIds = (unitTypeId: number, parentIds: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<UnitLookupDto>>({
      method: 'GET',
      url: `/api/app/unit/lookup-by-parent-ids/${unitTypeId}`,
      params: { parentIds },
    },
    { apiName: this.apiName,...config });
  

  getTreeLookup = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<UnitTreeLookupDto>>({
      method: 'GET',
      url: `/api/app/unit/${id}/tree-lookup`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateAndUpdateUnitDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, UnitDto>({
      method: 'PUT',
      url: `/api/app/unit/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
