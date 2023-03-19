import { ListResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { DocumentTypeLookupDto, DocumentTypeService } from '@proxy/document-types';
import { CreateAndUpdateFileAttachmentDto, FileAttachmentDto } from '@proxy/file-attachments';
import { ConfirmationService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { DIALOG_MD } from 'src/app/shared/constants/sizes.const';
import { Actions } from 'src/app/shared/enums/actions.enum';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { FileAttachmentDetailComponent } from './detial/file-attachment-detail.component';

@Component({
  selector: 'app-file-attachment',
  templateUrl: './file-attachment.component.html',
  styleUrls: ['./file-attachment.component.scss'],
})
export class FileAttachmentComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  blockedPanel = false;
  items: FileAttachmentDto[] = [];
  listNewFile: CreateAndUpdateFileAttachmentDto[] = [];
  listUpdateFile: CreateAndUpdateFileAttachmentDto[] = [];
  listFileDeleted: string[] = [];

  documentTypeOptions: DocumentTypeLookupDto[] = [];
  giaiDoanOptions = [
    { value: 0, text: 'Tất cả' },
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];

  giaiDoan: number;
  hinhThuc: string;
  Actions = Actions;

  constructor(
    private documentTypeService: DocumentTypeService,
    private dialogService: DialogService,
    private utilService: UtilityService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.getOptions();
  }
  getOptions() {
    this.toggleBlockUI(true);
    this.documentTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<DocumentTypeLookupDto>) => {
          this.documentTypeOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }
  showAddModal() {
    const ref = this.dialogService.open(FileAttachmentDetailComponent, {
      header: 'Thêm file gắn kèm',
      width: DIALOG_MD,
    });

    ref.onClose.subscribe((data: CreateAndUpdateFileAttachmentDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.listNewFile.push(data);
        let item = {
          giaiDoan: data.giaiDoan,
          tenTaiLieu: data.tenTaiLieu,
          hinhThuc: data.hinhThuc,
          thoiGianBanHanh: data.thoiGianBanHanh,
          ngayNhan: data.ngayNhan,
          thuTuButLuc: data.thuTuButLuc,
          noiDungChinh: data.noiDungChinh,
          fileName: data.fileName,
        } as FileAttachmentDto;
        this.items.push(item);
      }
    });
  }
  showEditModal(item: FileAttachmentDto) {
    const ref = this.dialogService.open(FileAttachmentDetailComponent, {
      header: 'Thêm file gắn kèm',
      width: DIALOG_MD,
      data: {
        item: item,
      },
    });

    ref.onClose.subscribe((data: CreateAndUpdateFileAttachmentDto) => {
      if (data) {
        if (data.id) this.listUpdateFile.push(data);
        else {
          let index = this.listNewFile.indexOf(data);
          if (index !== -1) this.listNewFile[index] = data;
        }

        item.giaiDoan = data.giaiDoan;
        item.tenTaiLieu = data.tenTaiLieu;
        item.hinhThuc = data.hinhThuc;
        item.thoiGianBanHanh = data.thoiGianBanHanh;
        item.ngayNhan = data.ngayNhan;
        item.thuTuButLuc = data.thuTuButLuc;
        item.noiDungChinh = data.noiDungChinh;
        item.fileName = data.fileName;

        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
      }
    });
  }

  deleteRow(item) {
    if (!item) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteRowConfirm(item);
      },
    });
  }

  deleteRowConfirm(item: FileAttachmentDto) {
    let index = this.items.indexOf(item);
    if (index !== -1) {
      this.items.splice(index, 1);
      if (item.id) {
        this.listFileDeleted.push(item.id);
      }
      this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
    }
  }

  getHinhThuc(id): string {
    let documentType = this.documentTypeOptions.find(x => x.id === id);
    return documentType?.documentTypeName ?? "";
  }
  getGiaiDoan(id): string {
    let giaiDoan = this.giaiDoanOptions.find(x => x.value === id);
    return giaiDoan?.text ?? "";
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 300);
    }
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
