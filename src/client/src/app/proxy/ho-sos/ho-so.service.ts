import type { CreateHoSoDto, GetHoSoListDto, HoSoDto, UpdateHoSoDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class HoSoService {
  apiName = 'Default';
  

  create = (input: CreateHoSoDto) =>
    this.restService.request<any, HoSoDto>({
      method: 'POST',
      url: '/api/app/ho-so',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/ho-so/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/ho-so/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  dowload = (idTepDinhKem: string) =>
    this.restService.request<any, number[]>({
      method: 'POST',
      url: '/api/app/ho-so/dowload',
      params: { idTepDinhKem },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, HoSoDto>({
      method: 'GET',
      url: `/api/app/ho-so/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetHoSoListDto) =>
    this.restService.request<any, PagedResultDto<HoSoDto>>({
      method: 'GET',
      url: '/api/app/ho-so',
      params: { loaiVuViec: input.loaiVuViec, linhVuc: input.linhVuc, ketQua: input.ketQua, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: UpdateHoSoDto) =>
    this.restService.request<any, HoSoDto>({
      method: 'PUT',
      url: `/api/app/ho-so/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
