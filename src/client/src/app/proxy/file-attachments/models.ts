import type { AuditedEntityDto, EntityDto } from '@abp/ng.core';
import type { IFormFile } from '../microsoft/asp-net-core/http/models';

export interface CreateAndUpdateFileAttachmentDto extends EntityDto<string> {
  tenTaiLieu: string;
  hinhThuc: string;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc: string;
  noiDungChinh?: string;
  fileContent: IFormFile;
}

export interface FileAttachmentDto extends AuditedEntityDto<string> {
  idHoSo?: string;
  tenTaiLieu?: string;
  hinhThuc?: string;
  thoiGianBanHanh?: string;
  ngayNhan?: string;
  thuTuButLuc?: string;
  noiDungChinh?: string;
  fileName?: string;
}
