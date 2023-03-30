import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { LinhVuc } from '../linh-vuc.enum';
import type { LoaiKhieuNai } from '../loai-khieu-nai.enum';
import type { LoaiKetQua } from '../loai-ket-qua.enum';
import type { CreateAndUpdateFileAttachmentDto, FileAttachmentDto } from '../file-attachments/models';
import type { BaseListFilterDto } from '../models';

export interface ComplainDto extends AuditedEntityDto<string> {
  maHoSo?: string;
  linhVuc: LinhVuc;
  tieuDe?: string;
  nguoiDeNghi?: string;
  cccdCmnd?: string;
  ngaySinh?: string;
  dienThoai?: string;
  email?: string;
  diaChiThuongTru?: string;
  diaChiLienHe?: string;
  maTinhTP: number;
  maQuanHuyen: number;
  maXaPhuongTT: number;
  thoiGianTiepNhan?: string;
  thoiGianHenTraKQ?: string;
  noiDungVuViec?: string;
  boPhanDangXL?: string;
  soThua?: string;
  toBanDo?: string;
  dienTich: number;
  loaiDat: number;
  diaChiThuaDat?: string;
  tinhThuaDat: number;
  huyenThuaDat: number;
  xaThuaDat: number;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  ghiChu?: string;
  loaiKhieuNai1?: LoaiKhieuNai;
  ngayKhieuNai1?: string;
  ngayTraKQ1?: string;
  thamQuyen1?: string;
  soQD1?: string;
  ketQua1?: LoaiKetQua;
  loaiKhieuNai2?: LoaiKhieuNai;
  ngayKhieuNai2?: string;
  ngayTraKQ2?: string;
  thamQuyen2?: string;
  soQD2?: string;
  ketQua2?: LoaiKetQua;
  ketQua?: LoaiKetQua;
  congKhai: boolean;
  fileAttachments: FileAttachmentDto[];
  concurrencyStamp?: string;
}

export interface CreateComplainDto {
  maHoSo: string;
  linhVuc: LinhVuc;
  tieuDe: string;
  nguoiDeNghi: string;
  cccdCmnd: string;
  ngaySinh: string;
  dienThoai: string;
  email?: string;
  diaChiThuongTru: string;
  diaChiLienHe: string;
  maTinhTP: number;
  maQuanHuyen: number;
  maXaPhuongTT: number;
  thoiGianTiepNhan: string;
  thoiGianHenTraKQ: string;
  noiDungVuViec: string;
  boPhanDangXL: string;
  soThua: string;
  toBanDo: string;
  dienTich: number;
  loaiDat: number;
  diaChiThuaDat: string;
  tinhThuaDat: number;
  huyenThuaDat: number;
  xaThuaDat: number;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  ghiChu?: string;
  loaiKhieuNai1?: LoaiKhieuNai;
  ngayKhieuNai1?: string;
  ngayTraKQ1?: string;
  thamQuyen1?: string;
  soQD1?: string;
  ketQua1?: LoaiKetQua;
  loaiKhieuNai2?: LoaiKhieuNai;
  ngayKhieuNai2?: string;
  ngayTraKQ2?: string;
  thamQuyen2?: string;
  soQD2?: string;
  ketQua2?: LoaiKetQua;
  congKhai: boolean;
  fileAttachments: CreateAndUpdateFileAttachmentDto[];
}

export interface GetComplainListDto extends BaseListFilterDto {
  maTinhTP?: number;
  maQuanHuyen?: number;
  maXaPhuongTT?: number;
  fromDate?: string;
  toDate?: string;
  linhVuc?: LinhVuc;
  ketQua?: LoaiKetQua;
  giaiDoan?: number;
  congKhai?: boolean;
}

export interface UpdateComplainDto extends EntityDto<string> {
  maHoSo: string;
  linhVuc: LinhVuc;
  tieuDe: string;
  nguoiDeNghi: string;
  cccdCmnd: string;
  ngaySinh: string;
  dienThoai: string;
  email?: string;
  diaChiThuongTru: string;
  diaChiLienHe: string;
  maTinhTP: number;
  maQuanHuyen: number;
  maXaPhuongTT: number;
  thoiGianTiepNhan: string;
  thoiGianHenTraKQ: string;
  noiDungVuViec: string;
  boPhanDangXL: string;
  soThua: string;
  toBanDo: string;
  dienTich: number;
  loaiDat: number;
  diaChiThuaDat: string;
  tinhThuaDat: number;
  huyenThuaDat: number;
  xaThuaDat: number;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  ghiChu?: string;
  loaiKhieuNai1?: LoaiKhieuNai;
  ngayKhieuNai1?: string;
  ngayTraKQ1?: string;
  thamQuyen1?: string;
  soQD1?: string;
  ketQua1?: LoaiKetQua;
  loaiKhieuNai2?: LoaiKhieuNai;
  ngayKhieuNai2?: string;
  ngayTraKQ2?: string;
  thamQuyen2?: string;
  soQD2?: string;
  ketQua2?: LoaiKetQua;
  congKhai: boolean;
  concurrencyStamp?: string;
}
