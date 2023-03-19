import type { CreateAndUpdateFileAttachmentDto, FileAttachmentDto, GetFileAttachmentListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';

@Injectable({
  providedIn: 'root',
})
export class FileAttachmentService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateFileAttachmentDto) =>
    this.restService.request<any, FileAttachmentDto>({
      method: 'POST',
      url: '/api/app/file-attachment',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: string) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/file-attachment/${id}`,
    },
    { apiName: this.apiName });
  

  dowload = (fileAttachmentId: string) =>
    this.restService.request<any, number[]>({
      method: 'POST',
      url: `/api/app/file-attachment/dowload/${fileAttachmentId}`,
    },
    { apiName: this.apiName });
  

  get = (id: string) =>
    this.restService.request<any, FileAttachmentDto>({
      method: 'GET',
      url: `/api/app/file-attachment/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetFileAttachmentListDto) =>
    this.restService.request<any, PagedResultDto<FileAttachmentDto>>({
      method: 'GET',
      url: '/api/app/file-attachment',
      params: { complainId: input.complainId, denounceId: input.denounceId, giaiDoan: input.giaiDoan, hinhThuc: input.hinhThuc, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  update = (id: string, input: CreateAndUpdateFileAttachmentDto) =>
    this.restService.request<any, FileAttachmentDto>({
      method: 'PUT',
      url: `/api/app/file-attachment/${id}`,
      body: input,
    },
    { apiName: this.apiName });
  

  upload = (fileAttachmentId: string, file: IFormFile) =>
    this.restService.request<any, string>({
      method: 'POST',
      responseType: 'text',
      url: `/api/app/file-attachment/upload/${fileAttachmentId}`,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
