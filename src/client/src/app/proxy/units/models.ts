import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { Status } from '../status.enum';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateUnitDto extends EntityDto<number> {
  unitCode: string;
  unitName: string;
  shortName: string;
  unitTypeId: number;
  parentId?: number;
  description?: string;
  orderIndex?: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface GetUnitListDto extends BaseListFilterDto {
  status?: Status;
  unitTypeId?: number;
  parentId?: number;
}

export interface UnitDto extends FullAuditedEntityDto<number> {
  unitCode?: string;
  unitName?: string;
  shortName?: string;
  unitTypeId: number;
  parentId?: number;
  description?: string;
  orderIndex?: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface UnitLookupDto extends EntityDto<number> {
  unitName?: string;
}
