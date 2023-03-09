import { mapEnumToOptions } from '@abp/ng.core';

export enum LoaiKetQua {
  Dung = 1,
  CoDungCoSai = 2,
  Sai = -1,
}

export const loaiKetQuaOptions = mapEnumToOptions(LoaiKetQua);
