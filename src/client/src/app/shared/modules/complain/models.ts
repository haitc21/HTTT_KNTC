import type { EntityDto } from '@abp/ng.core';

import type { ExtensibleEntityDto, ExtensibleFullAuditedEntityDto, ExtensibleObject, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetComplainsInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}

enum LoaiVuViec
{
    KhieuNai = 1,
    ToCao = 2
}
enum LinhVuc
{
    DataDai = 1,
    MoiTruong = 2,
    TaiNguyenNuoc = 3,
    Khoangsan = 4
}
enum LoaiKetQua
{
    Sai = 1,
    CoSai = 2,
    CoDung = 3,
    Dung = 4,
}

export interface ComplainDto extends ExtensibleFullAuditedEntityDto<string> {
  hosoId?: string;
  MaHoSo: string;
  TieuDe: string;
  NguoiDeNghi: string;
  CccdCmnd: string;
  NgayCapCccdCmnd: Date;
  NoiCapCccdCmnd: Date;
  NgaySinh: Date;
  DienThaoi: string;
  Email: string;
  DiaChiThuongTru: string;
  DiaChiLienHe: string;
  MaTinhTP: string;
  MaQuanHuyen: string;
  MaXaPhuongTT: string;
  LoaiVuViec: LoaiVuViec;
  LinhVuc: LinhVuc;
  NgayTiepNhan: Date;
  NgayHenTraKQ: Date;
  NoiDungVuViec: string;
  SoThua: string;
  ToBanDo: string;
  DienTich: string;
  LoaiDat: string;
  DiaChiThuaDat: string;
  TinhThuaDat: string;
  HuyenThuaDat: string;
  XaThuaDat: string;
  DuLieuToaDo: string;
  DuLieuHinhHoc: string;
  SoLanTraKQ: number;
  KetQua: LoaiKetQua;
}

export interface ComplainInfoDto extends EntityDto<string> {
  hosoId?: string;
  dob?: string;
}

export interface ComplainUpdateDto extends ComplainCreateOrUpdateDtoBase {
  password?: string;
  concurrencyStamp?: string;
}

export interface CreateAndUpdateComplainDto extends ComplainUpdateDto {
  dob?: string;
}

export interface ComplainCreateOrUpdateDtoBase extends ExtensibleObject {
  hosoId: string;
  MaHoSo: string;
  TieuDe: string;
  NguoiDeNghi: string;
  CccdCmnd: string;
  NgayCapCccdCmnd: Date;
  NoiCapCccdCmnd: Date;
  NgaySinh: Date;
  DienThaoi: string;
  Email: string;
  DiaChiThuongTru: string;
  DiaChiLienHe: string;
  MaTinhTP: string;
  MaQuanHuyen: string;
  MaXaPhuongTT: string;
  LoaiVuViec: LoaiVuViec;
  LinhVuc: LinhVuc;
  NgayTiepNhan: Date;
  NgayHenTraKQ: Date;
  NoiDungVuViec: string;
  SoThua: string;
  ToBanDo: string;
  DienTich: string;
  LoaiDat: string;
  DiaChiThuaDat: string;
  TinhThuaDat: string;
  HuyenThuaDat: string;
  XaThuaDat: string;
  DuLieuToaDo: string;
  DuLieuHinhHoc: string;
  SoLanTraKQ: number;
  KetQua: LoaiKetQua;
}

export interface SearchComplainDto extends GetComplainsInput {
  keyword?: string;
  MaTinh?: number;
  MaHuyen?: number;
  MaPhuong?: number;
  ThoiGianNop?: Date;
  GiaiDoan?: number;
  TinhTrang?: number;
}

export interface ComplainListDto extends EntityDto<string> {
  hosoId: string;
  MaHoSo: string;
  TieuDe: string;
  NguoiDeNghi: string;
  CccdCmnd: string;
  NgayCapCccdCmnd: Date;
  NoiCapCccdCmnd: Date;
  NgaySinh: Date;
  DienThaoi: string;
  Email: string;
  DiaChiThuongTru: string;
  DiaChiLienHe: string;
  MaTinhTP: string;
  MaQuanHuyen: string;
  MaXaPhuongTT: string;
  LoaiVuViec: LoaiVuViec;
  LinhVuc: LinhVuc;
  NgayTiepNhan: Date;
  NgayHenTraKQ: Date;
  NoiDungVuViec: string;
  SoThua: string;
  ToBanDo: string;
  DienTich: string;
  LoaiDat: string;
  DiaChiThuaDat: string;
  TinhThuaDat: string;
  HuyenThuaDat: string;
  XaThuaDat: string;
  DuLieuToaDo: string;
  DuLieuHinhHoc: string;
  SoLanTraKQ: number;
  KetQua: LoaiKetQua;
}
