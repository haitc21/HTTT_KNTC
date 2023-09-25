import { mapEnumToOptions } from '@abp/ng.core';

export enum UserType {
  QuanLyTinh = 1,//Mặc định là quản lý toàn Tỉnh
  QuanLyHuyen = 2,
  QuanLyXa = 3,
}

export const userTypeOptions = mapEnumToOptions(UserType);
