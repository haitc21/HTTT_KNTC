import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';

export interface AllSysConfigCacheItem {
  items: SysConfigCacheItem[];
}

export interface CreateSysConfigDto extends EntityDto<number> {
  name: string;
  value: string;
  description?: string;
  concurrencyStamp?: string;
}

export interface GetSysConfigListDto extends BaseListFilterDto {
}

export interface SysConfigCacheItem {
  name?: string;
  value?: string;
}

export interface SysConfigDto extends EntityDto<number> {
  name?: string;
  value?: string;
  description?: string;
  concurrencyStamp?: string;
}

export interface UpdateSysConfigDto extends EntityDto<number> {
  value: string;
  description?: string;
  concurrencyStamp?: string;
}
