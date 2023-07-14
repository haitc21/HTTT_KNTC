import { PagedResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { DIALOG_MD } from 'src/app/_shared/constants/sizes.const';
import { Actions } from 'src/app/_shared/enums/actions.enum';
import { UnitTypeDetailComponent } from './detail/unit-type-detail.component';
import { UnitTypeDto, UnitTypeService } from '@proxy/category-unit-types';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
  selector: 'app-Unit-type',
  templateUrl: './Unit-type.component.html',
})
export class UnitTypeComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;
  home: MenuItem;
  breadcrumb: MenuItem[];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  Actions = Actions;

  //Business variables
  public items: UnitTypeDto[];
  public selectedItems: UnitTypeDto[] = [];
  actionItem: UnitTypeDto;
  public keyword: string = '';

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;
  actionMenu: MenuItem[];

  constructor(
    public layoutService: LayoutService,
    private unitTypeService: UnitTypeService,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService
  ) {}

  ngOnInit() {
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
    this.breadcrumb = [{ label: ' Quản trị hệ thống', icon: 'pi pi-cog', disabled: true }];
    this.breadcrumb.push({
      label: ' Danh mục loại địa danh',
      icon: 'pi pi-star',
    });

    this.getPermission();
    this.buildActionMenu();
    this.loadData();
  }
  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('UnitTypes.Edit');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('UnitTypes.Delete');
    this.visibleActionColumn = this.hasPermissionUpdate || this.hasPermissionDelete;
  }

  loadData() {
    this.layoutService.blockUI$.next(true);

    this.unitTypeService
      .getList({
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
        keyword: this.keyword,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<UnitTypeDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  showAddModal() {
    const ref = this.dialogService.open(UnitTypeDetailComponent, {
      header: 'Thêm loại địa danh',
      width: DIALOG_MD,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: UnitTypeDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.CREATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  showUpdateModal(row: any) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const ref = this.dialogService.open(UnitTypeDetailComponent, {
      data: {
        id: row.id,
      },
      header: `Cập nhật loại địa danh`,
      width: DIALOG_MD,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: UnitTypeDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
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
  deleteItemsConfirm(ids: any[]) {
    this.layoutService.blockUI$.next(true);

    this.unitTypeService
      .deleteMultiple(ids)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.loadData();
          this.selectedItems = [];
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
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
        this.deleteRowConfirm(item.id);
      },
    });
  }
  deleteRowConfirm(id) {
    this.layoutService.blockUI$.next(true);

    this.unitTypeService
      .delete(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.notificationService.showSuccess(MessageConstants.DELETED_OK_MSG);
          this.loadData();
          this.selectedItems = [];
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
  } ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
