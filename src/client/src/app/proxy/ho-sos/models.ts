import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { LoaiKetQua } from './loai-ket-qua.enum';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';
import type { LoaiVuViec } from './loai-vu-viec.enum';
import type { LinhVuc } from './linh-vuc.enum';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateKQGQHoSoDto extends EntityDto<string> {
  lanGQ: number;
  ngayTraKQ: string;
  thamQuyen: string;
  soQD: string;
  ghiChu?: string;
  ketQua: LoaiKetQua;
}

export interface CreateAndUpdateTepDinhKemHoSoDto extends EntityDto<string> {
  tenTaiLieu: string;
  hinhThuc: string;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc: string;
  noiDungChinh?: string;
  fileContent: IFormFile;
}

export interface CreateHoSoDto {
  maHoSo: string;
  tieuDe: string;
  nguoiDeNghi: string;
  cccdCmnd: string;
  ngayCapCccdCmnd: string;
  noiCapCccdCmnd: string;
  ngaySinh: string;
  dienThaoi: string;
  email?: string;
  diaChiThuongTru: string;
  diaChiLienHe: string;
  maTinhTP: string;
  maQuanHuyen: string;
  maXaPhuongTT: string;
  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  ngayTiepNhan?: string;
  ngayHenTraKQ?: string;
  noiDungVuViec: string;
  soThua: string;
  toBanDo: string;
  dienTich: string;
  loaiDat: string;
  diaChiThuaDat: string;
  tinhThuaDat: string;
  huyenThuaDat: string;
  xaThuaDat: string;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  kqgqHoSos: CreateAndUpdateKQGQHoSoDto[];
  tepDinhKemHoSos: CreateAndUpdateTepDinhKemHoSoDto[];
}

export interface GetHoSoListDto extends BaseListFilterDto {
  loaiVuViec?: LoaiVuViec;
  linhVuc?: LinhVuc;
  ketQua?: LoaiKetQua;
}

export interface HoSoDto extends AuditedEntityDto<string> {
  maHoSo?: string;
  tieuDe?: string;
  nguoiDeNghi?: string;
  cccdCmnd?: string;
  ngayCapCccdCmnd?: string;
  noiCapCccdCmnd?: string;
  ngaySinh?: string;
  dienThaoi?: string;
  email?: string;
  diaChiThuongTru?: string;
  diaChiLienHe?: string;
  maTinhTP?: string;
  maQuanHuyen?: string;
  maXaPhuongTT?: string;
  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  ngayTiepNhan?: string;
  ngayHenTraKQ?: string;
  noiDungVuViec?: string;
  soThua?: string;
  toBanDo?: string;
  dienTich?: string;
  loaiDat?: string;
  diaChiThuaDat?: string;
  tinhThuaDat?: string;
  huyenThuaDat?: string;
  xaThuaDat?: string;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  soLanTraKQ: number;
  ketQua: LoaiKetQua;
  kqgqHoSos: KQGQHoSoDto[];
  tepDinhKemHoSos: TepDinhKemHoSoDto[];
}

export interface KQGQHoSoDto extends AuditedEntityDto<string> {
  idHoSo?: string;
  lanGQ: number;
  ngayTraKQ?: string;
  thamQuyen?: string;
  soQD?: string;
  ghiChu?: string;
  ketQua: LoaiKetQua;
}

export interface TepDinhKemHoSoDto extends AuditedEntityDto<string> {
  idHoSo?: string;
  tenTaiLieu?: string;
  hinhThuc?: string;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc?: string;
  noiDungChinh?: string;
  contentType?: string;
  contentLength?: number;
}

export interface UpdateHoSoDto extends EntityDto<string> {
  maHoSo: string;
  tieuDe: string;
  nguoiDeNghi: string;
  cccdCmnd: string;
  ngayCapCccdCmnd: string;
  noiCapCccdCmnd: string;
  ngaySinh: string;
  dienThaoi: string;
  email?: string;
  diaChiThuongTru: string;
  diaChiLienHe: string;
  maTinhTP: string;
  maQuanHuyen: string;
  maXaPhuongTT: string;
  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  ngayTiepNhan?: string;
  ngayHenTraKQ?: string;
  noiDungVuViec: string;
  soThua: string;
  toBanDo: string;
  dienTich: string;
  loaiDat: string;
  diaChiThuaDat: string;
  tinhThuaDat: string;
  huyenThuaDat: string;
  xaThuaDat: string;
  duLieuToaDo?: string;
  duLieuHinhHoc?: string;
  listKQGQHoSoDeleted: string[];
  kqgqHoSos: CreateAndUpdateKQGQHoSoDto[];
  listTepDinhKemHoSosDeleted: string[];
  tepDinhKemHoSos: CreateAndUpdateTepDinhKemHoSoDto[];
  concurrencyStamp?: string;
}
