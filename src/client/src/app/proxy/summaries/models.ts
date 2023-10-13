import type { LoaiVuViec } from '../loai-vu-viec.enum';
import type { LinhVuc } from '../linh-vuc.enum';
import type { LoaiKetQua } from '../loai-ket-qua.enum';
import type { TrangThai } from '../trang-thai.enum';
import type { BaseListFilterDto } from '../models';

export interface GetSumaryMapDto {
  loaiVuViec?: LoaiVuViec;
  linhVuc?: LinhVuc;
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
  trangThai?: TrangThai;
  keyword?: string;
  nguoiNopDon?: string;
}

export interface GetSummaryListDto extends BaseListFilterDto {
  loaiVuViec?: LoaiVuViec;
  linhVuc?: LinhVuc;
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
  trangThai?: TrangThai;
  nguoiNopDon?: string;
}

export interface SummaryChartDto {
  landComplain: number;
  landComplain_Dung: number;
  landComplain_CoDungCoSai: number;
  landComplain_Sai: number;
  enviromentComplain: number;
  enviromentComplain_Dung: number;
  enviromentComplain_CoDungCoSai: number;
  enviromentComplain_Sai: number;
  waterComplain: number;
  waterComplain_Dung: number;
  waterComplain_CoDungCoSai: number;
  waterComplain_Sai: number;
  mineralComplain: number;
  mineralComplain_Dung: number;
  mineralComplain_CoDungCoSai: number;
  mineralComplain_Sai: number;
  landDenounce: number;
  landDenounce_Dung: number;
  landDenounce_CoDungCoSai: number;
  landDenounce_Sai: number;
  enviromentDenounce: number;
  enviromentDenounce_Dung: number;
  enviromentDenounce_CoDungCoSai: number;
  enviromentDenounce_Sai: number;
  waterDenounce: number;
  waterDenounce_Dung: number;
  waterDenounce_CoDungCoSai: number;
  waterDenounce_Sai: number;
  mineralDenounce: number;
  mineralDenounce_Dung: number;
  mineralDenounce_CoDungCoSai: number;
  mineralDenounce_Sai: number;
  complainByStatus: number[];
  denounceByStatus: number[];
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
  boPhanDangXL?: string;
  thoiGianHenTraKQ?: string;
  ketQua?: LoaiKetQua;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  soThua?: string;
  toBanDo?: string;
  maTinhTP: number;
  maQuanHuyen: number;
  maXaPhuongTT: number;
  congKhai: boolean;
  cccdCmnd?: string;
  trangThai: TrangThai;
  luuTru: boolean;
}

export interface SummaryMapDto {
  id?: string;
  loaiVuViec: LoaiVuViec;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
}
