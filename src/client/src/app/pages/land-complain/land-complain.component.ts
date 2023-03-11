import { AuthService, ListResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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

@Component({
  selector: 'app-land-complain',
  templateUrl: './land-complain.component.html',
  styleUrls: ['./land-complain.component.scss'],
})
export class LandComplainComponent implements OnInit {
  //System variables
  private ngUnsubscribe = new Subject<void>();

  blockedPanel = false;
  data: Complain[] = [];
  mockData: Complain[] = [];

  loaiHS = ['khiếu nại', 'Tố cáo'];
  linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];
  typesHoSo = typesHoSo;
  fieldsHoSo = fieldsHoSo;

  filter: GetComplainListDto;
  keyword: string = '';
  items: any[];
  selectedItems: ComplainDto[] = [];
  actionItem: ComplainDto;

  skipCount: number = 0;
  maxResultCount: number = 10;
  totalCount: number;

  maTinh: number;
  maHuyen: number;
  maXa: number;
  NgayTiepNhanRange: Date[];
  giaiDoan: number;
  tinhTrang: number;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;

  Actions = Actions;
  actionMenu: MenuItem[];

  giaiDoanOptions = [
    { value: 0, text: 'Khiếu nại lần I' },
    { value: 1, text: 'Khiếu nại lần II' },
  ];
  stageOptions = [
    { value: LoaiKetQua.Dung, text: 'Đúng' },
    { value: LoaiKetQua.Sai, text: 'Sai' },
    { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  ];

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private oAuthService: OAuthService,
    private authService: AuthService,
    dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService,
    private complainService: ComplainService,
    private unitService: UnitService
  ) {}

  ngOnInit(): void {
    this.toggleBlockUI(true);
    //this.mockData = this.mockService.mockData();
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
    }
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
    }
  }

  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);
    console.log(this.NgayTiepNhanRange);
    
    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword,
      maTinhTP: this.maTinh,
      maQuanHuyen: this.maHuyen,
      maXaPhuongTT: this.maXa,
      fromDate: this.NgayTiepNhanRange ? this.NgayTiepNhanRange[0].toUTCString() : null,
      toDate: this.NgayTiepNhanRange && this.NgayTiepNhanRange[1]  ? this.NgayTiepNhanRange[1].toUTCString() : null,
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

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 300);
    }
  }

  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('AbpIdentity.Users.Update');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('AbpIdentity.Users.Update');

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

  showEditModal(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    /*
    const ref = this.dialogService.open(ComplainComponent, {
      data: {
        id: row.id,
      },
      header: `Cập nhật người dùng '${row.userName}'`,
      width: DIALOG_MD,
    });

    ref.onClose.subscribe((data: IdentityUserUpdateDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.actionItem = null;
        this.loadData();
      }
    });
    */
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
}
