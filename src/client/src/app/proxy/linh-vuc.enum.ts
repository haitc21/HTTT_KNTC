import { mapEnumToOptions } from '@abp/ng.core';

export enum LinhVuc {
  TatCa = 0,
  DatDai = 1,
  MoiTruong = 2,
  KhoangSan = 3,
  TaiNguyenNuoc = 4,
}

export const linhVucOptions = mapEnumToOptions(LinhVuc);
