import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { Status } from '../status.enum';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateLandTypeDto extends EntityDto<number> {
  landTypeCode: string;
  landTypeName: string;
  description?: string;
  orderIndex: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface GetLandTypeListDto extends BaseListFilterDto {
  status?: Status;
}

export interface LandTypeDto extends FullAuditedEntityDto<number> {
  landTypeCode?: string;
  landTypeName?: string;
  description?: string;
  orderIndex: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface LandTypeLookupDto extends EntityDto<number> {
  landTypeName?: string;
}
