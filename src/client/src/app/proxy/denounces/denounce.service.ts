import type { CreateDenounceDto, DenounceDto, GetDenounceListDto, UpdateDenounceDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DenounceService {
  apiName = 'Default';
  

  create = (input: CreateDenounceDto) =>
    this.restService.request<any, DenounceDto>({
      method: 'POST',
      url: '/api/app/denounce',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/denounce/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/denounce/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  dowload = (idTepDinhKem: string) =>
    this.restService.request<any, number[]>({
      method: 'POST',
      url: '/api/app/denounce/dowload',
      params: { idTepDinhKem },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, DenounceDto>({
      method: 'GET',
      url: `/api/app/denounce/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetDenounceListDto) =>
    this.restService.request<any, PagedResultDto<DenounceDto>>({
      method: 'GET',
      url: '/api/app/denounce',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, ketQua: input.ketQua, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: UpdateDenounceDto) =>
    this.restService.request<any, DenounceDto>({
      method: 'PUT',
      url: `/api/app/denounce/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}