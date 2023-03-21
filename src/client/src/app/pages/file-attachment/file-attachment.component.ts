import { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { LoaiVuViec } from '@proxy';
import { DocumentTypeLookupDto, DocumentTypeService } from '@proxy/document-types';
import {
  CreateAndUpdateFileAttachmentDto,
  FileAttachmentDto,
  FileAttachmentService,
} from '@proxy/file-attachments';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { DIALOG_MD } from 'src/app/shared/constants/sizes.const';
import { Actions } from 'src/app/shared/enums/actions.enum';
import { FileService } from 'src/app/shared/services/file.service';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { FileAttachmentDetailComponent } from './detial/file-attachment-detail.component';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-file-attachment',
  templateUrl: './file-attachment.component.html',
  styleUrls: ['./file-attachment.component.scss'],
})
export class FileAttachmentComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() loaiVuViec!: LoaiVuViec;
  @Input() mode: 'create' | 'update';
  @Input() complainId: string | null = null;
  @Input() denounceId: string | null = null;

  blockedPanel = false;
  items: FileAttachmentDto[] = [];
  data: FileAttachmentDto[] = [];
  files: File[] = [];
  actionItem: FileAttachmentDto;
  actionMenu: MenuItem[];

  // modal
  visibleAddModal = false;
  visibleUpdateModal = false;
  headerModal = '';
  selectedItem: FileAttachmentDto;

  documentTypeOptions: DocumentTypeLookupDto[] = [];
  giaiDoanOptions = [
    { value: 0, text: 'Tất cả' },
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];

  giaiDoan: number;
  hinhThuc: number;
  Actions = Actions;
  public keyword: string = '';

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  constructor(
    private documentTypeService: DocumentTypeService,
    private dialogService: DialogService,
    private utilService: UtilityService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private fileAttachmentService: FileAttachmentService,
    private fileService: FileService
  ) {}

  ngOnInit() {
    this.buildActionMenu();
    this.getOptions();
    this.loadData();
  }

  loadData() {
    this.toggleBlockUI(true);
    if (this.mode == 'update') {
      this.fileAttachmentService
        .getList({
          maxResultCount: this.maxResultCount,
          skipCount: this.skipCount,
          keyword: this.keyword,
          complainId: this.complainId,
          denounceId: this.denounceId,
          giaiDoan: this.giaiDoan,
          hinhThuc: this.hinhThuc,
        })
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: (response: PagedResultDto<FileAttachmentDto>) => {
            this.items = response.items;
            this.totalCount = response.totalCount;
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    }

    if (this.mode == 'create') {
      this.items = this.data.filter(
        x =>
          (!this.giaiDoan || x.giaiDoan == this.giaiDoan) &&
          (!this.hinhThuc || x.hinhThuc == this.hinhThuc)
      );
      this.toggleBlockUI(false);
    }
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
    this.visibleAddModal = true;
  }
  submitAdd(dto: any) {
    if (dto) {
      this.toggleBlockUI(true);
      if (this.mode == 'create') {
        let fileAttachment = {
          loaiVuViec: this.loaiVuViec,
          complainId: this.complainId,
          denounceId: this.denounceId,
          giaiDoan: dto.giaiDoan,
          tenTaiLieu: dto.tenTaiLieu,
          hinhThuc: dto.hinhThuc,
          thoiGianBanHanh: dto.thoiGianBanHanh,
          ngayNhan: dto.ngayNhan,
          thuTuButLuc: dto.thuTuButLuc,
          noiDungChinh: dto.noiDungChinh,
          fileName: dto.fileName,
          contentType: dto.contentType,
          contentLength: dto.contentLength,
        } as FileAttachmentDto;
        this.data.push(fileAttachment);
        this.files.push(dto.file);
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.loadData();
        this.visibleAddModal = false;
        this.toggleBlockUI(false);
      }
      if (this.mode == 'update') {
        dto.loaiVuViec = this.loaiVuViec;
        dto.complainId = this.complainId;
        dto.denounceId = this.denounceId;
        this.fileAttachmentService
          .create(dto)
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe({
            next: (res: FileAttachmentDto) => {
              if (res) {
                this.fileService
                  .uploadFilAttachment(res.id, dto.file)
                  .pipe(takeUntil(this.ngUnsubscribe))
                  .subscribe(
                    res => {
                      this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
                      this.loadData();
                      this.visibleAddModal = false;
                      this.toggleBlockUI(false);
                    },
                    () => {
                      this.toggleBlockUI(false);
                    }
                  );
              }
            },
            error: () => {
              this.toggleBlockUI(false);
            },
          });
      }
    }
  }
  closeAddModal() {
    this.visibleAddModal = false;
  }
  showEditModal(item: FileAttachmentDto) {
    this.selectedItem = item;
    this.visibleUpdateModal = true;
  }
  submitUpdate(dto: any) {
    if (dto) {
      if (this.mode == 'create') {
        let fileAttachment = {
          giaiDoan: dto.giaiDoan,
          tenTaiLieu: dto.tenTaiLieu,
          hinhThuc: dto.hinhThuc,
          thoiGianBanHanh: dto.thoiGianBanHanh,
          ngayNhan: dto.ngayNhan,
          thuTuButLuc: dto.thuTuButLuc,
          noiDungChinh: dto.noiDungChinh,
          fileName: dto.fileName,
          contentType: dto.contentType,
          contentLength: dto.contentLength,
        } as FileAttachmentDto;
        let index = this.data.indexOf(this.selectedItem);
        if (index > -1) {
          this.data[index] = fileAttachment;
          if (dto.file) {
            let file = this.files.find(x => x.name === this.selectedItem.fileName);
            let indexFile = this.files.indexOf(file);
            if (index > -1) this.files[indexFile] = dto.file;
          }
          this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
          this.loadData();
          this.visibleUpdateModal = false;
          this.selectedItem = null;
        }
      }
      if (this.mode == 'update') {
        this.fileAttachmentService
          .update(this.selectedItem.id, dto)
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe({
            next: (res: FileAttachmentDto) => {
              if (dto.file) {
                this.fileService
                  .uploadFilAttachment(res.id, dto.file)
                  .pipe(takeUntil(this.ngUnsubscribe))
                  .subscribe(
                    res => {
                      this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                      this.loadData();
                      this.visibleUpdateModal = false;
                      this.selectedItem = null;
                      this.toggleBlockUI(false);
                    },
                    () => {
                      this.toggleBlockUI(false);
                    }
                  );
              } else {
                this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.loadData();
                this.toggleBlockUI(false);
              }
            },
            error: () => {
              this.toggleBlockUI(false);
            },
          });
      }
    }
  }
  closeUpdateModal() {
    this.visibleUpdateModal = false;
    this.selectedItem = null;
  }
  dowload(item: FileAttachmentDto) {
    this.toggleBlockUI(true);
    this.fileService
      .downloadFileAttachment(item.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          const uint8Array = this.utilService.base64ToArrayBuffer(data);
          const blob = new Blob([uint8Array], { type: item.contentType });
          saveAs(blob, item.fileName);
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
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
    if (this.mode == 'create') {
      let index = this.data.indexOf(item);
      if (index !== -1) {
        this.data.splice(index, 1);
        let file = this.files.find(x => x.name === item.fileName);
        let indexFile = this.files.indexOf(file);
        if (indexFile !== -1) this.files.splice(indexFile, 1);
        this.loadData();
        this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
      }
    }
    if (this.mode == 'update') {
      this.fileAttachmentService
        .delete(item.id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
            this.loadData();
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    }
  }

  getHinhThuc(id): string {
    let documentType = this.documentTypeOptions.find(x => x.id === id);
    return documentType?.documentTypeName ?? '';
  }
  getGiaiDoan(id): string {
    let giaiDoan = this.giaiDoanOptions.find(x => x.value === id);
    return giaiDoan?.text ?? '';
  }
  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  buildActionMenu() {
    this.actionMenu = [
      {
        label: this.Actions.UPDATE,
        icon: 'pi pi-fw pi-pencil',
        command: event => {
          this.showEditModal(this.actionItem);
          this.actionItem = null;
        },
      },
      {
        label: this.Actions.DOWNLOAD,
        icon: 'pi pi-fw pi-download',
        command: event => {
          this.dowload(this.actionItem);
          this.actionItem = null;
        },
        visible: this.mode == 'update',
      },
      {
        label: this.Actions.DELETE,
        icon: 'pi pi-fw pi-trash',
        command: event => {
          this.deleteRow(this.actionItem);
          this.actionItem = null;
        },
      },
    ];
  }
  setActionItem(item) {
    this.actionItem = item;
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
