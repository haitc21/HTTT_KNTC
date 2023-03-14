import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { Status } from '../status.enum';
import type { BaseListFilterDto } from '../models';

export interface ConfigDto extends FullAuditedEntityDto<number> {
  organizationCode?: string;
  organizationName?: string;
  toaDo?: string;
  tel?: string;
  address?: string;
  description?: string;
  status: Status;
  concurrencyStamp?: string;
}

export interface ConfigLookupDto extends EntityDto<number> {
  configName?: string;
}

export interface CreateAndUpdateConfigDto extends EntityDto<number> {
  organizationCode: string;
  organizationName: string;
  toaDo?: string;
  tel?: string;
  address?: string;
  description?: string;
  status: number;
  concurrencyStamp?: string;
}

export interface GetConfigListDto extends BaseListFilterDto {
  status?: Status;
}
