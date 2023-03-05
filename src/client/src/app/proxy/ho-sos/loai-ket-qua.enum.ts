import { mapEnumToOptions } from '@abp/ng.core';

export enum LoaiKetQua {
  CoDung = 1,
  Dung = 2,
  Sai = -2,
  CoSai = -1,
}

export const loaiKetQuaOptions = mapEnumToOptions(LoaiKetQua);
