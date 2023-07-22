import { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { LoaiVuViec } from '@proxy';
import { DocumentTypeLookupDto, DocumentTypeService } from '@proxy/document-types';
import {
  FileAttachmentDto,
  FileAttachmentService,
} from '@proxy/file-attachments';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { Actions } from 'src/app/_shared/enums/actions.enum';
import { FileService } from 'src/app/_shared/services/file.service';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { UtilityService } from 'src/app/_shared/services/utility.service';
import { TYPE_EXCEL } from 'src/app/_shared/constants/file-type.consts';
import { OAuthService } from 'angular-oauth2-oidc';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { congKhaiOptions, giaiDoanOptions } from 'src/app/_shared/constants/consts';

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
  giaiDoanOptions = giaiDoanOptions;
  congKhaiOptions = congKhaiOptions;

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
    private utilService: UtilityService,
    private confirmationService: ConfirmationService,
    private notificationService: NotificationService,
    private fileAttachmentService: FileAttachmentService,
    private fileService: FileService,
    private oAuthService: OAuthService,
    public layoutService: LayoutService
  ) {}

  ngOnInit() {
    this.buildActionMenu();
    this.getOptions();
    this.loadData();
  }

  loadData() {
    this.layoutService.blockUI$.next(true);
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
            this.layoutService.blockUI$.next(false);
          },
          error: () => {
            this.layoutService.blockUI$.next(false);
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
      this.layoutService.blockUI$.next(false);
    }
  }

  exportExcel() {
    this.layoutService.blockUI$.next(true);
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
            let fileName =
              this.utilService.formatDate(new Date(), 'dd/MM/yyyy HH:mm') + '_Tệp gắn kèm.xlsx';
            const uint8Array = this.utilService.saveFile(data, TYPE_EXCEL, fileName);
          }
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  getOptions() {
    this.layoutService.blockUI$.next(true);
    this.documentTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<DocumentTypeLookupDto>) => {
          this.documentTypeOptions = res.items;
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  showAddModal() {
    this.visibleAddModal = true;
    this.headerModal = 'Thêm mới Hồ sơ gắn kèm';
  }
  
  submitAdd(dto: any) {
    if (dto) {
      this.layoutService.blockUI$.next(true);
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
        this.layoutService.blockUI$.next(false);
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
                      this.layoutService.blockUI$.next(false);
                    },
                    () => {
                      this.layoutService.blockUI$.next(false);
                    }
                  );
              }
            },
            error: () => {
              this.layoutService.blockUI$.next(false);
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
    this.headerModal = `Cập nhật Hồ sơ gắn kèm "${item.tenTaiLieu}"`;
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
                      this.layoutService.blockUI$.next(false);
                    },
                    () => {
                      this.layoutService.blockUI$.next(false);
                    }
                  );
              } else {
                this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
                this.loadData();
                this.headerModal = '';
                this.visibleUpdateModal = false;
                this.selectedItem = null;
                this.layoutService.blockUI$.next(false);
              }
            },
            error: () => {
              this.layoutService.blockUI$.next(false);
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
    this.headerModal = `Chi tiết Hồ sơ gắn kèm "${item.tenTaiLieu}"`;
    this.visibleViewModal = true;
  }
  closeViewModal() {
    this.visibleViewModal = false;
    this.headerModal = '';
    this.selectedItem = null;
  }

  download(item: FileAttachmentDto) {
    this.layoutService.blockUI$.next(true);
    this.fileService
      .downloadFileAttachment(item.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            const uint8Array = this.utilService.saveFile(data, item.contentType, item.fileName);
          }
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
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
            this.layoutService.blockUI$.next(false);
          },
          error: () => {
            this.layoutService.blockUI$.next(false);
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
        visible: this.modeHoSo != 'view' && this.hasLoggedIn,
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
