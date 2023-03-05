import type { CreateAndUpdateComplainDto, SearchComplainDto, ComplainDto, ComplainInfoDto, ComplainListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ComplainService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateComplainDto) =>
    this.restService.request<any, ComplainDto>({
      method: 'POST',
      url: '/api/app/complains',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/complains/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: string[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/complains/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, ComplainDto>({
      method: 'GET',
      url: `/api/app/complains/${id}`,
    },
    { apiName: this.apiName });
  
  getAvatar = (complainId: string) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: `/api/app/complains/avatar/${complainId}`,
    },
    { apiName: this.apiName });
  

  getList = (input: SearchComplainDto) =>
    this.restService.request<any, PagedResultDto<ComplainListDto>>({
      method: 'GET',
      url: '/api/app/complains',
      params: {         
        filter: input.filter, 
        MaTinh: input.MaTinh,
        MaHuyen: input.MaHuyen,
        MaPhuong: input.MaPhuong,
        ThoiGianNop: input.ThoiGianNop,
        GiaiDoan: input.GiaiDoan,
        TinhTrang: input.TinhTrang,
        sorting: input.sorting, 
        skipCount: input.skipCount, 
        maxResultCount: input.maxResultCount 
      },
    },
    { apiName: this.apiName });

  getComplainInfo = (complainId: string) =>
    this.restService.request<any, ComplainDto>({
      method: 'GET',
      url: `/api/app/complains/complain-info/${complainId}`,
    },
    { apiName: this.apiName });
  
  update = (id: string, input: CreateAndUpdateComplainDto) =>
    this.restService.request<any, ComplainDto>({
      method: 'PUT',
      url: `/api/app/complains/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  updateComplainInfo = (complainId: string, input: CreateAndUpdateComplainDto) =>
    this.restService.request<any, ComplainInfoDto>({
      method: 'PUT',
      url: `/api/app/complains/complain-info/${complainId}`,
      body: input,
    },
    { apiName: this.apiName });
  

  constructor(private restService: RestService) {}
}
