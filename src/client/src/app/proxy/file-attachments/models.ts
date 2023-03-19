import type { LoaiVuViec } from '../loai-vu-viec.enum';
import type { AuditedEntityDto } from '@abp/ng.core';
import type { BaseListFilterDto } from '../models';

export interface CreateAndUpdateFileAttachmentDto {
  id?: string;
  loaiVuViec: LoaiVuViec;
  complainId?: string;
  denounceId?: string;
  tenTaiLieu: string;
  giaiDoan: number;
  hinhThuc: number;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc: string;
  noiDungChinh?: string;
  fileName?: string;
  contentType?: string;
  contentLength: number;
  concurrencyStamp?: string;
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
}

export interface GetFileAttachmentListDto extends BaseListFilterDto {
  complainId?: string;
  denounceId?: string;
  tenTaiLieu?: string;
  giaiDoan?: number;
  hinhThuc?: number;
}
