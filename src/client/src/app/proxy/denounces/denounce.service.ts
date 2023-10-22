import type { CreateDenounceDto, DenounceDto, GetDenounceListDto, UpdateDenounceDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DenounceService {
  apiName = 'Default';
  

  create = (input: CreateDenounceDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DenounceDto>({
      method: 'POST',
      url: '/api/app/denounce',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/denounce/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/denounce/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DenounceDto>({
      method: 'GET',
      url: `/api/app/denounce/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getExcel = (input: GetDenounceListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/denounce/excel',
      params: { maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, linhVuc: input.linhVuc, ketQua: input.ketQua, congKhai: input.congKhai, luuTru: input.luuTru, trangThai: input.trangThai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetDenounceListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DenounceDto>>({
      method: 'GET',
      url: '/api/app/denounce',
      params: { maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, linhVuc: input.linhVuc, ketQua: input.ketQua, congKhai: input.congKhai, luuTru: input.luuTru, trangThai: input.trangThai, nguoiNopDon: input.nguoiNopDon, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateDenounceDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DenounceDto>({
      method: 'PUT',
      url: `/api/app/denounce/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
