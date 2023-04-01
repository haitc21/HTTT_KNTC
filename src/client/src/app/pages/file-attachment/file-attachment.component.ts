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
import { TYPE_EXCEL } from 'src/app/shared/constants/file-type.consts';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'app-file-attachment',
  templateUrl: './file-attachment.component.html',
  styleUrls: ['./file-attachment.component.scss'],
})
export class FileAttachmentComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() loaiVuViec!: LoaiVuViec;
  @Input() modeHoSo: 'create' | 'update' | 'view';
  @Input() complainId: string | null = null;
  @Input() denounceId: string | null = null;

  LoaiVuViec = LoaiVuViec;
  blockedPanel = false;
  items: FileAttachmentDto[] = [];
  data: FileAttachmentDto[] = [];
  files: File[] = [];
  actionItem: FileAttachmentDto;
  actionMenu: MenuItem[];
  congKhai: boolean | null;

  // modal
  visibleAddModal = false;
  visibleUpdateModal = false;
  visibleViewModal = false;
  headerModal = '';
  selectedItem: FileAttachmentDto;

  documentTypeOptions: DocumentTypeLookupDto[] = [];
  giaiDoanOptions = [
    { value: 0, text: 'Tất cả' },
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];
  congKhaiOptions = [
    { value: true, text: 'Công khai' },
    { value: false, text: 'Không công khai' },
  ];

  giaiDoan: number;
  hinhThuc: number;
  Actions = Actions;
  public keyword: string = '';

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private documentTypeService: DocumentTypeService,
    private dialogService: DialogService,
    private utilService: UtilityService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private fileAttachmentService: FileAttachmentService,
    private fileService: FileService,
    private oAuthService: OAuthService
  ) {}

  ngOnInit() {
    this.buildActionMenu();
    this.getOptions();
    this.loadData();
    console.log('modeHoSo', this.modeHoSo);
  }

  loadData() {
    this.toggleBlockUI(true);
    if (this.modeHoSo == 'update' || this.modeHoSo == 'view') {
      this.fileAttachmentService
        .getList({
          maxResultCount: this.maxResultCount,
          skipCount: this.skipCount,
          keyword: this.keyword,
          complainId: this.complainId,
          denounceId: this.denounceId,
          giaiDoan: this.giaiDoan,
          hinhThuc: this.hinhThuc,
          congKhai: this.hasLoggedIn ? this.congKhai : true,
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

    if (this.modeHoSo == 'create') {
      this.items = this.data.filter(
        x =>
          (!this.giaiDoan || x.giaiDoan == this.giaiDoan) &&
          (!this.hinhThuc || x.hinhThuc == this.hinhThuc) &&
          (!this.congKhai || x.congKhai == this.congKhai)
      );
      this.toggleBlockUI(false);
    }
  }

  exportExcel() {
    this.toggleBlockUI(true);
    this.fileAttachmentService
      .getExcel({
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
        keyword: this.keyword,
        complainId: this.complainId,
        denounceId: this.denounceId,
        giaiDoan: this.giaiDoan,
        hinhThuc: this.hinhThuc,
        congKhai: this.modeHoSo == 'view' ? true : this.congKhai,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            const uint8Array = this.utilService.base64ToArrayBuffer(data);
            const blob = new Blob([uint8Array], { type: TYPE_EXCEL });
            let fileName =
              this.utilService.formatDate(new Date(), 'dd/MM/yyyy HH:mm') + '_Tệp gắn kèm.xlsx';
            saveAs(blob, fileName);
          }
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
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
    this.headerModal = 'Thêm mới tệp gắn kèm';
  }
  submitAdd(dto: any) {
    if (dto) {
      this.toggleBlockUI(true);
      if (this.modeHoSo == 'create') {
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
          congKhai: dto.congKhai,
        } as FileAttachmentDto;
        this.data.push(fileAttachment);
        this.files.push(dto.file);
        // this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.loadData();
        this.visibleAddModal = false;
        this.headerModal = '';
        this.toggleBlockUI(false);
      }
      if (this.modeHoSo == 'update') {
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
                      this.headerModal = '';
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
    this.headerModal = '';
  }
  showUpdateModal(item: FileAttachmentDto) {
    this.selectedItem = item;
    this.headerModal = `Cập nhật tệp gắn kèm "${item.tenTaiLieu}"`;
    this.visibleUpdateModal = true;
  }
  submitUpdate(dto: any) {
    if (dto) {
      if (this.modeHoSo == 'create') {
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
          congKhai: dto.congKhai,
        } as FileAttachmentDto;
        let index = this.data.indexOf(this.selectedItem);
        if (index > -1) {
          this.data[index] = fileAttachment;
          if (dto.file) {
            let file = this.files.find(x => x.name === this.selectedItem.fileName);
            let indexFile = this.files.indexOf(file);
            if (index > -1) this.files[indexFile] = dto.file;
          }
          // this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
          this.loadData();
          this.headerModal = '';
          this.visibleUpdateModal = false;
          this.selectedItem = null;
        }
      }
      if (this.modeHoSo == 'update') {
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
                      this.headerModal = '';
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
                this.headerModal = '';
                this.visibleUpdateModal = false;
                this.selectedItem = null;
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
    this.headerModal = '';
    this.selectedItem = null;
  }
  viewDetail(item: FileAttachmentDto) {
    this.selectedItem = item;
    this.headerModal = `Chi tiết tệp gắn kèm "${item.tenTaiLieu}"`;
    this.visibleViewModal = true;
  }
  closeViewModal() {
    this.visibleViewModal = false;
    this.headerModal = '';
    this.selectedItem = null;
  }

  download(item: FileAttachmentDto) {
    this.toggleBlockUI(true);
    this.fileService
      .downloadFileAttachment(item.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            const uint8Array = this.utilService.base64ToArrayBuffer(data);
            const blob = new Blob([uint8Array], { type: item.contentType });
            saveAs(blob, item.fileName);
          }
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
    if (this.modeHoSo == 'create') {
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
    if (this.modeHoSo == 'update') {
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
          this.showUpdateModal(this.actionItem);
          this.actionItem = null;
        },
        visible: this.modeHoSo != 'view',
      },
      {
        label: this.Actions.DOWNLOAD,
        icon: 'pi pi-fw pi-download',
        command: event => {
          this.download(this.actionItem);
          this.actionItem = null;
        },
        visible: this.modeHoSo != 'create',
      },
      {
        label: this.Actions.DELETE,
        icon: 'pi pi-fw pi-trash',
        command: event => {
          this.deleteRow(this.actionItem);
          this.actionItem = null;
        },
        visible: this.modeHoSo != 'view',
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
