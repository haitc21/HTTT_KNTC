import { mapEnumToOptions } from '@abp/ng.core';

export enum LoaiKhieuNai {
  TatCa = 0,
  KhieuNai = 1,
  KhieuKien = 2,
}

export const loaiKhieuNaiOptions = mapEnumToOptions(LoaiKhieuNai);
