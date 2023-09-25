import { mapEnumToOptions } from '@abp/ng.core';

export enum TrangThai {
  TiepNhan = 0,
  DangXuLy = 1,
  DaThuLy = 2,
  DaKetLuan = 3,
  RutDon = 4,
  TraLaiDon = 5,
  ChuyenDon = 6,
}

export const trangThaiOptions = mapEnumToOptions(TrangThai);
