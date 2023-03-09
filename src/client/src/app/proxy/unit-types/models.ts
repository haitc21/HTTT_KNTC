import type { EntityDto, FullAuditedEntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';
import type { Status } from '../status.enum';

export interface CreateAndUpdateUnitTypeDto extends EntityDto<number> {
  unitTypeCode: string;
  unitTypeName: string;
  description?: string;
  orderIndex: number;
  status: number;
  concurrencyStamp?: string;
}

export interface GetUnitTypeListDto extends BaseListFilterDto {
  status?: Status;
}

export interface UnitTypeDto extends FullAuditedEntityDto<number> {
  unitTypeCode?: string;
  unitTypeName?: string;
  description?: string;
  orderIndex: number;
  status: Status;
  concurrencyStamp?: string;
}

export interface UnitTypeLookupDto extends EntityDto<number> {
  unitTypeName?: string;
}
