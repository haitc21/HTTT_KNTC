import type { CreateAndUpdateDocumentTypeDto, DocumentTypeDto, DocumentTypeLookupDto, GetDocumentTypesListDto } from './models';
import { RestService } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DocumentTypeService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateDocumentTypeDto) =>
    this.restService.request<any, DocumentTypeDto>({
      method: 'POST',
      url: '/api/app/document-type',
      body: input,
    },
    { apiName: this.apiName });
  

  delete = (id: number) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/document-type/${id}`,
    },
    { apiName: this.apiName });
  

  deleteMultiple = (ids: number[]) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/document-type/multiple',
      params: { ids },
    },
    { apiName: this.apiName });
  

  get = (id: number) =>
    this.restService.request<any, DocumentTypeDto>({
      method: 'GET',
      url: `/api/app/document-type/${id}`,
    },
    { apiName: this.apiName });
  

  getList = (input: GetDocumentTypesListDto) =>
    this.restService.request<any, PagedResultDto<DocumentTypeDto>>({
      method: 'GET',
      url: '/api/app/document-type',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName });
  

  getLookup = () =>
    this.restService.request<any, ListResultDto<DocumentTypeLookupDto>>({
      method: 'GET',
      url: '/api/app/document-type/lookup',
    },
    { apiName: this.apiName });
  

  update = (id: number, input: CreateAndUpdateDocumentTypeDto) =>
    this.restService.request<any, DocumentTypeDto>({
      method: 'PUT',
      url: `/api/app/document-type/${id}`,
      body: input,
    },
    { apiName: this.apiName });

  constructor(private restService: RestService) {}
}
