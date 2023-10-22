import type { CreateAndUpdateFileAttachmentDto, FileAttachmentDto, GetFileAttachmentListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';

@Injectable({
  providedIn: 'root',
})
export class FileAttachmentService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateFileAttachmentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileAttachmentDto>({
      method: 'POST',
      url: '/api/app/file-attachment',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/file-attachment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  download = (fileAttachmentId: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'POST',
      url: `/api/app/file-attachment/download/${fileAttachmentId}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileAttachmentDto>({
      method: 'GET',
      url: `/api/app/file-attachment/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getExcel = (input: GetFileAttachmentListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/app/file-attachment/excel',
      params: { idHoSo: input.idHoSo, loaiVuViec: input.loaiVuViec, giaiDoan: input.giaiDoan, hinhThuc: input.hinhThuc, congKhai: input.congKhai, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetFileAttachmentListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FileAttachmentDto>>({
      method: 'GET',
      url: '/api/app/file-attachment',
      params: { idHoSo: input.idHoSo, loaiVuViec: input.loaiVuViec, giaiDoan: input.giaiDoan, hinhThuc: input.hinhThuc, congKhai: input.congKhai, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateAndUpdateFileAttachmentDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileAttachmentDto>({
      method: 'PUT',
      url: `/api/app/file-attachment/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });
  

  upload = (fileAttachmentId: string, file: IFormFile, config?: Partial<Rest.Config>) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: `/api/app/file-attachment/upload/${fileAttachmentId}`,
      body: file,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
