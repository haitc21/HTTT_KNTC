import { AuthService, ListResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Complain, fieldsHoSo, typesHoSo } from '../../shared/mock/Complain';
import { MockService } from '../../shared/mock/mock.service';
import { PagedResultDto, PermissionService } from '@abp/ng.core';
import { Actions } from 'src/app/shared/enums/actions.enum';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { DomSanitizer } from '@angular/platform-browser';
import { ComplainDto, ComplainService, GetComplainListDto } from '@proxy/complains';
import { UnitService } from '@proxy/units';
import { UnitLookupDto } from '@proxy/units/models';
import { LinhVuc, LoaiKetQua, LoaiVuViec } from '@proxy';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { LandComplainDetailComponent } from './detail/land-complain-detail.component';
import { DIALOG_BG } from 'src/app/shared/constants/sizes.const';

@Component({
  selector: 'app-land-complain',
  templateUrl: './land-complain.component.html',
  styleUrls: ['./land-complain.component.scss'],
})
export class LandComplainComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();

  blockedPanel = false;

  typesHoSo = typesHoSo;
  fieldsHoSo = fieldsHoSo;

  filter: GetComplainListDto;
  keyword: string = '';
  items: ComplainDto[];
  selectedItems: ComplainDto[] = [];
  actionItem: ComplainDto;

  skipCount: number = 0;
  maxResultCount: number = 10;
  totalCount: number;

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

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private oAuthService: OAuthService,
    private authService: AuthService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService,
    private complainService: ComplainService,
    private utilService: UtilityService,
    private unitService: UnitService
  ) {}

  ngOnInit(): void {
    this.getPermission();
    this.buildActionMenu();
    this.loadOptions();
    this.loadData(true);
    this.toggleBlockUI(false);
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
    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword,
      maTinhTP: this.maTinh,
      maQuanHuyen: this.maHuyen,
      maXaPhuongTT: this.maXa,
      fromDate: this.thoiGianTiepNhanRange ? this.thoiGianTiepNhanRange[0].toUTCString() : null,
      toDate:
        this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
          ? this.thoiGianTiepNhanRange[1].toUTCString()
          : null,
      linhVuc: LinhVuc.DataDai,
      ketQua: this.tinhTrang,
      giaiDoan: this.giaiDoan,
    } as GetComplainListDto;

    this.complainService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<ComplainDto>) => {
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

    this.complainService.delete(id).subscribe({
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
    const ref = this.dialogService.open(LandComplainDetailComponent, {
      header: 'Thêm khiếu nại/khiếu kiện',
      width: DIALOG_BG,
    });

    ref.onClose.subscribe((data: ComplainDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }
  showEditModal(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }

    const ref = this.dialogService.open(LandComplainDetailComponent, {
      data: {
        id: row.id,
      },
      header: `Cập nhật khiếu nại/khiếu kiện '${row.tieuDe}'`,
      width: DIALOG_BG,
    });

    ref.onClose.subscribe((data: ComplainDto) => {
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
    this.complainService.deleteMultiple(ids).subscribe({
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
