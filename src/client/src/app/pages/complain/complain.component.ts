import { ListResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { PagedResultDto, PermissionService } from '@abp/ng.core';
import { Actions } from 'src/app/_shared/enums/actions.enum';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { ComplainDto, ComplainService, GetComplainListDto } from '@proxy/complains';
import { UnitService } from '@proxy/units';
import { UnitLookupDto } from '@proxy/units/models';
import { LinhVuc, LoaiKetQua, LoaiVuViec } from '@proxy';
import { UtilityService } from 'src/app/_shared/services/utility.service';
import { ComplainDetailComponent } from './detail/complain-detail.component';
import { DIALOG_BG } from 'src/app/_shared/constants/sizes.const';
import { FileUploadDto as FileUploadDto } from 'src/app/_shared/models/file-upload.class';
import { FileService } from 'src/app/_shared/services/file.service';
import { DialogService } from 'primeng/dynamicdialog';
import { ActivatedRoute } from '@angular/router';
import { TYPE_EXCEL } from 'src/app/_shared/constants/file-type.consts';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import {
  KetquaOptions,
  congKhaiOptions,
  giaiDoanOptions,
  loaiKQOptions,
} from 'src/app/_shared/constants/consts';

@Component({
  selector: 'app-complain',
  templateUrl: './complain.component.html',
  styleUrls: ['./complain.component.scss'],
})
export class ComplainComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  home: MenuItem;
  breadcrumb: MenuItem[];

  linhVuc: LinhVuc;
  header: string = '';
  items: ComplainDto[];
  selectedItems: ComplainDto[] = [];
  actionItem: ComplainDto;

  // default
  blockedPanel = false;
  skipCount: number = 0;
  maxResultCount: number = 20;
  totalCount: number;

  // filter
  filter: GetComplainListDto;
  keyword: string = '';
  nguoiNopDon = '';
  maTinh: number;
  maHuyen: number;
  maXa: number;
  thoiGianTiepNhanRange: Date[];
  giaiDoan: number;
  tinhTrang: number;
  congKhai: boolean | null;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  giaiDoanOptions = giaiDoanOptions;
  loaiKQOptions = loaiKQOptions;
  congKhaiOptions = congKhaiOptions;
  KetquaOptions = KetquaOptions;

  // Permissions
  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;

  // Thao tac
  Actions = Actions;
  actionMenu: MenuItem[];

  constructor(
    public layoutService: LayoutService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService,
    private complainService: ComplainService,
    private utilService: UtilityService,
    private unitService: UnitService,
    private fileService: FileService,
    protected route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
    this.getPermission();
    this.buildActionMenu();
    this.loadOptions();
    this.route.paramMap.subscribe(params => {
      this.linhVuc = +params.get('linhVuc') as LinhVuc;
      this.buildBreadcrumb();
      //this.setHeader();
      this.resetFilter();
      this.loadData(true);
    });
  }
  private resetFilter() {
    this.skipCount = 0;
    this.maxResultCount = 20;
    this.totalCount = 0;
    this.keyword = '';
    this.nguoiNopDon = '';
    this.maTinh = null;
    this.maHuyen = null;
    this.maXa = null;
    this.thoiGianTiepNhanRange = null;
    this.giaiDoan = null;
    this.tinhTrang = null;
  }

  buildBreadcrumb() {
    this.breadcrumb = [{ label: ' Khiếu nại', icon: 'pi pi-inbox', disabled: true }];

    switch (this.linhVuc) {
      case LinhVuc.DatDai:
        this.breadcrumb.push({
          label: ' Đất đai',
          icon: 'pi pi-image',
          routerLink: [`/complain/${LinhVuc.DatDai}`],
        });
        break;
      case LinhVuc.MoiTruong:
        this.breadcrumb.push({
          label: ' Môi trường',
          icon: 'pi pi-sun',
          routerLink: [`/complain/${LinhVuc.MoiTruong}`],
        });
        break;
      case LinhVuc.TaiNguyenNuoc:
        this.breadcrumb.push({
          label: ' Tài nguyên nước',
          icon: 'pi pi-flag-fill',
          routerLink: [`/complain/${LinhVuc.TaiNguyenNuoc}`],
        });
        break;
      case LinhVuc.KhoangSan:
        this.breadcrumb.push({
          label: ' Khoáng sản',
          icon: 'pi pi-bitcoin',
          routerLink: [`/complain/${LinhVuc.KhoangSan}`],
        });
        break;
      default:
      //this.header = '';
    }
  }

  loadOptions() {
    this.layoutService.blockUI$.next(true);
    this.unitService
      .getLookup(1)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitLookupDto>) => {
          this.tinhOptions = res.items;
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  tinhChange(event) {
    this.loadData();
    if (event.value) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(2, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.huyenOptions = [];
  }

  huyenChange(event) {
    this.loadData();
    if (event.value) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(3, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.xaOptions = [];
  }

  loadData(isFirst: boolean = false) {
    this.layoutService.blockUI$.next(true);
    if (isFirst) {
      this.filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        linhVuc: this.linhVuc,
      } as GetComplainListDto;
    } else {
      this.filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        keyword: this.keyword,
        nguoiNopDon: this.nguoiNopDon,
        maTinhTP: this.maTinh,
        maQuanHuyen: this.maHuyen,
        maXaPhuongTT: this.maXa,
        fromDate:
          this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[0]
            ? this.thoiGianTiepNhanRange[0].toUTCString()
            : null,
        toDate:
          this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
            ? this.thoiGianTiepNhanRange[1].toUTCString()
            : null,
        linhVuc: this.linhVuc,
        ketQua: this.tinhTrang,
        giaiDoan: this.giaiDoan,
        congKhai: this.congKhai,
      } as GetComplainListDto;
    }

    this.complainService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ComplainDto>) => {
          this.items = response.items;

          this.totalCount = response.totalCount;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
    this.layoutService.blockUI$.next(false);
  }
  exportExcel() {
    this.layoutService.blockUI$.next(true);
    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword,
      nguoiNopDon: this.nguoiNopDon,
      maTinhTP: this.maTinh,
      maQuanHuyen: this.maHuyen,
      maXaPhuongTT: this.maXa,
      fromDate:
        this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[0]
          ? this.thoiGianTiepNhanRange[0].toUTCString()
          : null,
      toDate:
        this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
          ? this.thoiGianTiepNhanRange[1].toUTCString()
          : null,
      linhVuc: this.linhVuc,
      ketQua: this.tinhTrang,
      giaiDoan: this.giaiDoan,
    } as GetComplainListDto;
    this.complainService
      .getExcel(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            let fileName =
              this.utilService.formatDate(new Date(), 'dd/MM/yyyy HH:mm') +
              '_Khiếu nại/Khiếu kiện.xlsx';
            const uint8Array = this.utilService.saveFile(data, TYPE_EXCEL, fileName);
          }
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('Complains.Edit');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('Complains.Delete');

    this.visibleActionColumn = this.hasPermissionUpdate || this.hasPermissionDelete;
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
    this.layoutService.blockUI$.next(true);

    this.complainService
      .delete(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.resetFilter();
          this.loadData();
          this.selectedItems = [];
          this.actionItem = null;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  setActionItem(item) {
    this.actionItem = item;
  }

  showAddModal() {
    const ref = this.dialogService.open(ComplainDetailComponent, {
      modal: true,
      height: '92vh',
      header: 'Thêm khiếu nại/khiếu kiện',
      width: DIALOG_BG,
      data: {
        loaiVuViec: LoaiVuViec.KhieuNai,
        linhVuc: this.linhVuc,
        mode: 'create',
      },
    });

    ref.onClose.subscribe((data: ComplainDto | FileUploadDto[]) => {
      if (data) {
        if (data instanceof Array) {
          this.layoutService.blockUI$.next(true);
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
              this.layoutService.blockUI$.next(false);
              this.selectedItems = [];
              this.resetFilter();
              this.loadData();
            },
            () => {
              this.layoutService.blockUI$.next(false);
            }
          );
        } else {
          this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
          this.selectedItems = [];
          this.resetFilter();
          this.loadData();
        }
      }
    });
  }

  showUpdateModal(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }

    const ref = this.dialogService.open(ComplainDetailComponent, {
      height: '92vh',
      data: {
        id: row.id,
        loaiVuViec: LoaiVuViec.KhieuNai,
        linhVuc: this.linhVuc,
        mode: 'update',
      },
      header: `Cập nhật khiếu nại/khiếu kiện "${row.tieuDe}"`,
      width: DIALOG_BG,
    });

    ref.onClose.subscribe((data: ComplainDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.actionItem = null;
        this.resetFilter();
        this.loadData();
      }
    });
  }
  viewDetail(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }

    const ref = this.dialogService.open(ComplainDetailComponent, {
      height: '92vh',
      data: {
        id: row.id,
        loaiVuViec: LoaiVuViec.KhieuNai,
        linhVuc: this.linhVuc,
        mode: 'view',
      },
      header: `Chi tiết khiếu nại/khiếu kiện "${row.tieuDe}"`,
      width: DIALOG_BG,
    });
  }

  deleteItems() {
    if (this.selectedItems.length == 0) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    let ids = [];
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
    if (!kq) return 'Chưa có KQ';
    return this.loaiKQOptions.find(x => x.value == kq).text;
  }

  deleteItemsConfirm(ids: any[]) {
    this.layoutService.blockUI$.next(true);
    this.complainService
      .deleteMultiple(ids)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.resetFilter();
          this.loadData();
          this.selectedItems = [];
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  thoiGiantiepNhanChange() {
    if (this.thoiGianTiepNhanRange == null || this.thoiGianTiepNhanRange[1]) {
      this.loadData();
    }
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
