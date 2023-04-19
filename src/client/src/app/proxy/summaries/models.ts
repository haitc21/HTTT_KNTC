import type { LoaiKetQua } from '../loai-ket-qua.enum';
import type { BaseListFilterDto } from '../models';
import type { LoaiVuViec } from '../loai-vu-viec.enum';
import type { LinhVuc } from '../linh-vuc.enum';

export interface GetSumaryMapDto {
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
  keyword?: string;
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

export interface SummaryChartDto {
  landComplain: number;
  enviromentComplain: number;
  waterComplain: number;
  mineralComplain: number;
  landDenounce: number;
  enviromentDenounce: number;
  waterDenounce: number;
  mineralDenounce: number;
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
  soThua?: string;
  toBanDo?: string;
  boPhanDangXL?: string;
  ketQua?: LoaiKetQua;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
}

export interface SummaryMapDto {
  id?: string;
  loaiVuViec: LoaiVuViec;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
}
