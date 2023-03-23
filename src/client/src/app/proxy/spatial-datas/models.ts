import type { EntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateSpatialDataDto extends EntityDto<number> {
  objectId: number;
  tenToChuc?: string;
  quyen?: number;
  soToBD?: string;
  geoJson?: string;
}

export interface GetSpatialDataListDto extends BaseListFilterDto {
}

export interface SpatialDataDto extends EntityDto<number> {
  objectId: number;
  tenToChuc?: string;
  quyen?: number;
  soToBD?: string;
  geoJson?: string;
}

export interface SpatialDataLookupDto extends EntityDto<number> {
}
