import { mapEnumToOptions } from '@abp/ng.core';

export enum LinhVuc {
  DataDai = 0,
  MoiTruong = 1,
  TaiNguyenNuoc = 2,
  Khoángan = 3,
}

export const linhVucOptions = mapEnumToOptions(LinhVuc);
