import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';

export interface CreateAndUpdateFileAttachmentDto extends EntityDto<string> {
  giaiDoan: number;
  tenTaiLieu?: string;
  hinhThuc: number;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc: string;
  noiDungChinh?: string;
  fileName?: string;
  contentType?: string;
  contentLength: number;
}

export interface FileAttachmentDto extends AuditedEntityDto<string> {
  idHoSo?: string;
  tenTaiLieu?: string;
  giaiDoan: number;
  hinhThuc: number;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc?: string;
  noiDungChinh?: string;
  fileName?: string;
}
