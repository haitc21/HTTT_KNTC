import { ListResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { PagedResultDto, PermissionService } from '@abp/ng.core';
import { Actions } from 'src/app/shared/enums/actions.enum';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { DenounceDto, DenounceService, GetDenounceListDto } from '@proxy/denounces';
import { UnitService } from '@proxy/units';
import { UnitLookupDto } from '@proxy/units/models';
import { LinhVuc, LoaiKetQua, LoaiVuViec } from '@proxy';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { DenounceDetailComponent } from './detail/denounce-detail.component';
import { DIALOG_BG } from 'src/app/shared/constants/sizes.const';
import { EileUploadDto as FileUploadDto } from 'src/app/shared/models/file-upload.class';
import { FileService } from 'src/app/shared/services/file.service';
import { DialogService } from 'primeng/dynamicdialog';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-denounce',
  templateUrl: './denounce.component.html',
  styleUrls: ['./denounce.component.scss'],
})
export class DenounceComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();

  linhVuc: LinhVuc;
  header: string = '';
  items: DenounceDto[];
  selectedItems: DenounceDto[] = [];
  actionItem: DenounceDto;

  // default
  blockedPanel = false;
  skipCount: number = 0;
  maxResultCount: number = 10;
  totalCount: number;

  // filter
  filter: GetDenounceListDto;
  keyword: string = '';
  maTinh: number;
  maHuyen: number;
  maXa: number;
  thoiGianTiepNhanRange: Date[];
  giaiDoan: number;
  tinhTrang: number;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  giaiDoanOptions = [
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];
  loaiKQOptions = [
    { value: LoaiKetQua.Dung, text: 'Đúng' },
    { value: LoaiKetQua.Sai, text: 'Sai' },
    { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  ];

  // Permissions
  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;

  // Thao tac
  Actions = Actions;
  actionMenu: MenuItem[];

  constructor(
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService,
    private denounceService: DenounceService,
    private utilService: UtilityService,
    private unitService: UnitService,
    private fileService: FileService,
    protected route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getPermission();
    this.buildActionMenu();
    this.loadOptions();
    this.route.paramMap.subscribe(params => {
      this.linhVuc = +params.get('linhVuc') as LinhVuc;
      this.setHeader();
      this.resetFilter();
      this.loadData(true);
    });
  }
  private resetFilter() {
    this.skipCount = 0;
    this.maxResultCount = 10;
    this.totalCount = 0;
    this.keyword = '';
    this.maTinh = null;
    this.maHuyen = null;
    this.maXa = null;
    this.thoiGianTiepNhanRange = [];
    this.giaiDoan = null;
    this.tinhTrang = null;
  }

  private setHeader() {
    switch (this.linhVuc) {
      case LinhVuc.DataDai:
        this.header = 'Tố cao đất đai';
        break;
      case LinhVuc.MoiTruong:
        this.header = 'Tố cao môi trường';
        break;
      case LinhVuc.TaiNguyenNuoc:
        this.header = 'Tố cao tài nguyên nước';
        break;
      case LinhVuc.KhoangSan:
        this.header = 'Tố cao khoáng sản';
        break;
      default:
        this.header = '';
    }
  }

  loadOptions() {
    this.toggleBlockUI(true);
    this.unitService
      .getLookup(1)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitLookupDto>) => {
          this.tinhOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }
  inhChange(event) {
    this.loadData();
    if (event.value) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(2, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenOptions = res.items;
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.huyenOptions = [];
  }
  huyenChange(event) {
    this.loadData();
    if (event.value) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(3, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaOptions = res.items;
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.xaOptions = [];
  }

  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);
    if (isFirst) {
      this.filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        linhVuc: this.linhVuc
      } as GetDenounceListDto;
    } else {
      this.filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        keyword: this.keyword,
        maTinhTP: this.maTinh,
        maQuanHuyen: this.maHuyen,
        maXaPhuongTT: this.maXa,
        fromDate: this.thoiGianTiepNhanRange[0]
          ? this.thoiGianTiepNhanRange[0].toUTCString()
          : null,
        toDate:
          this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
            ? this.thoiGianTiepNhanRange[1].toUTCString()
            : null,
        linhVuc: this.linhVuc,
        ketQua: this.tinhTrang,
        giaiDoan: this.giaiDoan,
      } as GetDenounceListDto;
    }

    this.denounceService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<DenounceDto>) => {
          this.items = response.items;

          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
    this.toggleBlockUI(false);
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('Denounces.Edit');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('Denounces.Delete');

    this.visibleActionColumn = this.hasPermissionUpdate || this.hasPermissionDelete;
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
        visible: this.hasPermissionUpdate,
      },
      {
        label: this.Actions.DELETE,
        icon: 'pi pi-fw pi-trash',
        command: event => {
          this.deleteRow(this.actionItem);
          this.actionItem = null;
        },
        visible: this.hasPermissionDelete,
      },
    ];
  }

  deleteRow(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteRowConfirm(row.id);
      },
    });
  }

  deleteRowConfirm(id) {
    this.toggleBlockUI(true);

    this.denounceService.delete(id).subscribe({
      next: () => {
        this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
        this.loadData();
        this.selectedItems = [];
        this.actionItem = null;
        this.toggleBlockUI(false);
      },
      error: () => {
        this.toggleBlockUI(false);
      },
    });
  }

  setActionItem(item) {
    this.actionItem = item;
  }
  showAddModal() {
    const ref = this.dialogService.open(DenounceDetailComponent, {
      header: 'Thêm khiếu nại/khiếu kiện',
      width: DIALOG_BG,
      data: {
        linhVuc: this.linhVuc,
      },
    });

    ref.onClose.subscribe((data: DenounceDto | FileUploadDto[]) => {
      if (data) {
        if (data instanceof Array) {
          this.toggleBlockUI(true);
          const uploadObservables = data.map(f => {
            return this.fileService
              .uploadFilAttachment(f.id, f.file)
              .pipe(takeUntil(this.ngUnsubscribe));
          });

          forkJoin(uploadObservables).subscribe(
            results => {
              // results.forEach(res => {
              //   this.notificationService.showSuccess(`${res}`);
              // });
              this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
              this.toggleBlockUI(false);
              this.selectedItems = [];
              this.loadData();
            },
            () => {
              this.toggleBlockUI(false);
            }
          );
        } else {
          this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
          this.selectedItems = [];
          this.loadData();
        }
      }
    });
  }
  showEditModal(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }

    const ref = this.dialogService.open(DenounceDetailComponent, {
      data: {
        id: row.id,
        linhVuc: this.linhVuc,
      },
      header: `Cập nhật khiếu nại/khiếu kiện '${row.tieuDe}'`,
      width: DIALOG_BG,
    });

    ref.onClose.subscribe((data: DenounceDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.actionItem = null;
        this.loadData();
      }
    });
  }

  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    var ids = [];
    this.selectedItems.forEach(element => {
      ids.push(element.id);
    });
    this.confirmationService.confirm({
      message: MessageConstants.CONFIRM_DELETE_MSG,
      accept: () => {
        this.deleteItemsConfirm(ids);
      },
    });
  }

  getLoaiKetQua(kq: any): string {
    if (!kq) return '';
    return this.loaiKQOptions.find(x => x.value == kq).text;
  }

  deleteItemsConfirm(ids: any[]) {
    this.toggleBlockUI(true);
    this.denounceService.deleteMultiple(ids).subscribe({
      next: () => {
        this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
        this.loadData();
        this.selectedItems = [];
        this.toggleBlockUI(false);
      },
      error: () => {
        this.toggleBlockUI(false);
      },
    });
  }

  thoiGiantiepNhanChange() {
    if (this.thoiGianTiepNhanRange == null || this.thoiGianTiepNhanRange[1]) {
      this.loadData();
    }
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
