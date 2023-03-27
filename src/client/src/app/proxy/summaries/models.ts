import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { LoaiVuViec } from '../loai-vu-viec.enum';
import type { LinhVuc } from '../linh-vuc.enum';
import type { LoaiKetQua } from '../loai-ket-qua.enum';

export interface GetSummaryListDto extends PagedAndSortedResultRequestDto {
  landComplain: boolean;
  enviromentComplain: boolean;
  waterComplain: boolean;
  mineralComplain: boolean;
  landDenounce: boolean;
  enviromentDenounce: boolean;
  waterDenounce: boolean;
  mineralDenounce: boolean;
}

export interface SummaryDto {
  id?: string;
  maHoSo?: string;
  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  tieuDe?: string;
  nguoiNopDon?: string;
  dienThoai?: string;
  thoiGianTiepNhan?: string;
  thoiGianHenTraKQ?: string;
  boPhanDangXL?: string;
  ketQua?: LoaiKetQua;
}
