import { mapEnumToOptions } from '@abp/ng.core';

export enum LoaiVuViec {
  TatCa = 0,
  KhieuNai = 1,
  ToCao = 2,
}

export const loaiVuViecOptions = mapEnumToOptions(LoaiVuViec);
