import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { Status } from '../status.enum';
import type { BaseListFilterDto } from '../models';

export interface BaseMapDto extends FullAuditedEntityDto<number> {
  baseMapCode?: string;
  baseMapName?: string;
  description?: string;
  orderIndex?: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface BaseMapLookupDto extends EntityDto<number> {
  baseMapName?: string;
  baseMapCode?: string;
}

export interface CreateAndUpdateBaseMapDto extends EntityDto<number> {
  baseMapCode: string;
  baseMapName: string;
  description?: string;
  orderIndex?: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface GetBaseMapListDto extends BaseListFilterDto {
  status?: Status;
}
