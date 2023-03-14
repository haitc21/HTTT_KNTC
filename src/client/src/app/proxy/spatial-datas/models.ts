import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateSpatialDataDto extends EntityDto<number> {
  geoJson?: string;
}

export interface GetSpatialDataListDto extends BaseListFilterDto {
}

export interface SpatialDataDto extends FullAuditedEntityDto<number> {
  geoJson?: string;
}

export interface SpatialDataLookupDto extends EntityDto<number> {
}
