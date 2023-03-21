import type { CreateAndUpdateUnitDto, GetUnitListDto, UnitDto, UnitLookupDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class UnitService {
  apiName = 'Default';

  create = (input: CreateAndUpdateUnitDto) =>
    this.restService.request<any, UnitDto>(
      {
        method: 'POST',
        url: '/api/app/unit',
        body: input,
      },
      { apiName: this.apiName }
    );

  delete = (id: number) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/unit/${id}`,
      },
      { apiName: this.apiName }
    );

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/unit/multiple',
        params: { ids },
      },
      { apiName: this.apiName }
    );

  get = (id: number) =>
    this.restService.request<any, UnitDto>(
      {
        method: 'GET',
        url: `/api/app/unit/${id}`,
      },
      { apiName: this.apiName }
    );

  getList = (input: GetUnitListDto) =>
    this.restService.request<any, PagedResultDto<UnitDto>>(
      {
        method: 'GET',
        url: '/api/app/unit',
        params: {
          status: input.status,
          keyword: input.keyword,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
        },
      },
      { apiName: this.apiName }
    );

  getLookup = (unitTypeId: number, parentId?: number) =>
    this.restService.request<any, ListResultDto<UnitLookupDto>>(
      {
        method: 'GET',
        url: '/api/app/unit/lookup',
        params: { unitTypeId, parentId },
      },
      { apiName: this.apiName }
    );

  base64ToArrayBuffer(base64) {
    let binaryString = window.atob(base64);
    let binaryLen = binaryString.length;
    let bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
      var ascii = binaryString.charCodeAt(i);
      bytes[i] = ascii;
    }
    return bytes;
  }
  update = (id: number, input: CreateAndUpdateUnitDto) =>
    this.restService.request<any, UnitDto>(
      {
        method: 'PUT',
        url: `/api/app/unit/${id}`,
        body: input,
      },
      { apiName: this.apiName }
    );

  constructor(private restService: RestService) {}
}
