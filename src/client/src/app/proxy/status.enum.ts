import { mapEnumToOptions } from '@abp/ng.core';

export enum Status {
  Active = 1,
  DeActive = -1,
}

export const statusOptions = mapEnumToOptions(Status);
