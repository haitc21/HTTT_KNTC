import type { LoaiVuViec } from '../loai-vu-viec.enum';
import type { AuditedEntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateFileAttachmentDto {
  id?: string;
  loaiVuViec: LoaiVuViec;
  idHoSo?: string;
  tenTaiLieu: string;
  giaiDoan: number;
  hinhThuc: number;
  thoiGianBanHanh: string;
  ngayNhan: string;
  thuTuButLuc: string;
  noiDungChinh: string;
  fileName: string;
  contentType?: string;
  contentLength: number;
  concurrencyStamp?: string;
  congKhai: boolean;
  choPhepDownload: boolean;
}

export interface FileAttachmentDto extends AuditedEntityDto<string> {
  complainId?: string;
  denounceId?: string;
  tenTaiLieu?: string;
  giaiDoan: number;
  hinhThuc: number;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc?: string;
  noiDungChinh?: string;
  fileName?: string;
  contentType?: string;
  contentLength?: number;
  congKhai: boolean;
  choPhepDownload: boolean;
}

export interface GetFileAttachmentListDto extends BaseListFilterDto {
  idHoSo?: string;
  loaiVuViec: LoaiVuViec;
  giaiDoan?: number;
  hinhThuc?: number;
  congKhai?: boolean;
}
