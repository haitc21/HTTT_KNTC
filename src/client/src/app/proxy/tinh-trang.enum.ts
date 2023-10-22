import { mapEnumToOptions } from '@abp/ng.core';

export enum TinhTrang {
  ChuaXuLy = 0,
  DangXuLy = 1,
  DaXuLy = 2,
  SapDenHan = 3,
  QuaHan = 4,
}

export const tinhTrangOptions = mapEnumToOptions(TinhTrang);
