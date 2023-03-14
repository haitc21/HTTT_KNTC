import type { ComplainDto, CreateComplainDto, GetComplainListDto, UpdateComplainDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';

@Injectable({
  providedIn: 'root',
})
export class ComplainService {
  apiName = 'Default';
  

  create = (input: CreateComplainDto) =>
    this.restService.request<any, ComplainDto>({
      method: 'POST',
      url: '/api/app/complain',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/complain/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/complain/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  dowload = (idTepDinhKem: string) =>
    this.restService.request<any, number[]>({
      method: 'POST',
      url: '/api/app/complain/dowload',
      params: { idTepDinhKem },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ComplainDto>({
      method: 'GET',
      url: `/api/app/complain/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetComplainListDto) =>
    this.restService.request<any, PagedResultDto<ComplainDto>>({
      method: 'GET',
      url: '/api/app/complain',
      params: { maTinhTP: input.maTinhTP, maQuanHuyen: input.maQuanHuyen, maXaPhuongTT: input.maXaPhuongTT, fromDate: input.fromDate, toDate: input.toDate, linhVuc: input.linhVuc, ketQua: input.ketQua, giaiDoan: input.giaiDoan, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: UpdateComplainDto) =>
    this.restService.request<any, ComplainDto>({
      method: 'PUT',
      url: `/api/app/complain/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  upload = (idTepDinhKem: string, file: IFormFile) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/complain/upload',
      params: { idTepDinhKem },
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
