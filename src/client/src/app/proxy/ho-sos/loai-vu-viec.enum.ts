import { mapEnumToOptions } from '@abp/ng.core';

export enum LoaiVuViec {
  KhieuNai = 0,
  ToCao = 1,
}

export const loaiVuViecOptions = mapEnumToOptions(LoaiVuViec);
