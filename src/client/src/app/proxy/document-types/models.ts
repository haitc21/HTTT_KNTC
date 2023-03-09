import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { Status } from '../status.enum';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateDocumentTypeDto extends EntityDto<number> {
  documentTypeCode: string;
  documentTypeName: string;
  description?: string;
  orderIndex: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface DocumentTypeDto extends FullAuditedEntityDto<number> {
  documentTypeCode: string;
  documentTypeName: string;
  description: string;
  orderIndex: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface DocumentTypeLookupDto extends EntityDto<number> {
  documentTypeName?: string;
}

export interface GetDocumentTypesListDto extends BaseListFilterDto {
  status?: Status;
}
