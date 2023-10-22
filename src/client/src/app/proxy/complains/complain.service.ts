import type { ComplainDto, CreateComplainDto, GetComplainListDto, UpdateComplainDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ComplainService {
  apiName = 'Default';
  

  create = (input: CreateComplainDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ComplainDto>({
      method: 'POST',
      url: '/api/app/complain',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/complain/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/complain/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ComplainDto>({
      method: 'GET',
      url: `/api/app/complain/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getExcel = (input: GetComplainListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/complain/excel',
      params: { maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, linhVuc: input.linhVuc, mangLinhVuc: input.mangLinhVuc, ketQua: input.ketQua, giaiDoan: input.giaiDoan, congKhai: input.congKhai, luuTru: input.luuTru, nguoiNopDon: input.nguoiNopDon, trangThai: input.trangThai, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetComplainListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ComplainDto>>({
      method: 'GET',
      url: '/api/app/complain',
      params: { maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, linhVuc: input.linhVuc, mangLinhVuc: input.mangLinhVuc, ketQua: input.ketQua, giaiDoan: input.giaiDoan, congKhai: input.congKhai, luuTru: input.luuTru, nguoiNopDon: input.nguoiNopDon, trangThai: input.trangThai, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateComplainDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ComplainDto>({
      method: 'PUT',
      url: `/api/app/complain/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
