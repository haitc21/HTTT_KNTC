import { mapEnumToOptions } from '@abp/ng.core';

export enum LoaiKhieuNai {
  KhieuNai = 1,
  KhieuKien = 2,
}

export const loaiKhieuNaiOptions = mapEnumToOptions(LoaiKhieuNai);
