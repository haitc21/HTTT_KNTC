import type { LoaiKetQua } from '../loai-ket-qua.enum';
import type { BaseListFilterDto } from '../models';
import type { LoaiVuViec } from '../loai-vu-viec.enum';
import type { LinhVuc } from '../linh-vuc.enum';

export interface GetSumaryMapDto {
  keyword?: string;
  landComplain: boolean;
  enviromentComplain: boolean;
  waterComplain: boolean;
  mineralComplain: boolean;
  landDenounce: boolean;
  enviromentDenounce: boolean;
  waterDenounce: boolean;
  mineralDenounce: boolean;
  maTinhTP?: number;
  maQuanHuyen?: number;
  maXaPhuongTT?: number;
  fromDate?: string;
  toDate?: string;
  ketQua?: LoaiKetQua;
  congKhai?: boolean;
}

export interface GetSummaryListDto extends BaseListFilterDto {
  landComplain: boolean;
  enviromentComplain: boolean;
  waterComplain: boolean;
  mineralComplain: boolean;
  landDenounce: boolean;
  enviromentDenounce: boolean;
  waterDenounce: boolean;
  mineralDenounce: boolean;
  maTinhTP?: number;
  maQuanHuyen?: number;
  maXaPhuongTT?: number;
  fromDate?: string;
  toDate?: string;
  ketQua?: LoaiKetQua;
  congKhai?: boolean;
}

export interface SummaryDto {
  id?: string;
  maHoSo?: string;
  nguoiNopDon?: string;
  dienThoai?: string;
  diaChiLienHe?: string;
  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  tieuDe?: string;
  thoiGianTiepNhan?: string;
  thoiGianHenTraKQ?: string;
  boPhanDangXL?: string;
  ketQua?: LoaiKetQua;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
}
