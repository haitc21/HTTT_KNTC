import type { CreateAndUpdateDocumentTypeDto, DocumentTypeDto, DocumentTypeLookupDto, GetDocumentTypesListDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DocumentTypeService {
  apiName = 'Default';
  

  create = (input: CreateAndUpdateDocumentTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DocumentTypeDto>({
      method: 'POST',
      url: '/api/app/document-type',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/document-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  deleteMultiple = (ids: number[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: '/api/app/document-type/multiple',
      params: { ids },
    },
    { apiName: this.apiName,...config });
  

  get = (id: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DocumentTypeDto>({
      method: 'GET',
      url: `/api/app/document-type/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetDocumentTypesListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<DocumentTypeDto>>({
      method: 'GET',
      url: '/api/app/document-type',
      params: { status: input.status, keyword: input.keyword, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<DocumentTypeLookupDto>>({
      method: 'GET',
      url: '/api/app/document-type/lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: number, input: CreateAndUpdateDocumentTypeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DocumentTypeDto>({
      method: 'PUT',
      url: `/api/app/document-type/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
