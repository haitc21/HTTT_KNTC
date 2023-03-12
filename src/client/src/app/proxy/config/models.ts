import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';
import type { Status } from '../status.enum';

export interface CreateAndUpdateConfigDto extends EntityDto<number> {
  OrganizationCode: string;
  OrganizationName: string;  
  ToaDo?: string;
  tel?: string;
  address?: string;
  description?: string;
  status: Status;
  concurrencyStamp?: string;
}

export interface GetConfigListDto extends BaseListFilterDto {
  status?: Status;
}

export interface ConfigDto extends FullAuditedEntityDto<number> {
  OrganizationCode?: string;
  OrganizationName?: string;
  description?: string;
  ToaDo: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface ConfigLookupDto extends EntityDto<number> {
  OrganizationName?: string;
}
