import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface BaseListFilterDto extends PagedAndSortedResultRequestDto {
  keyword?: string;
}
