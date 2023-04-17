import { PagedResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RoleDto, RolesService } from '@proxy/roles';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { PermissionGrantComponent } from '../permission-grant/permission-grant.component';
import { RoleDetailComponent } from './detail/role-detail.component';
import { DIALOG_MD, DIALOG_SM } from 'src/app/shared/constants/sizes.const';
import { ROLE_PROVIDER } from 'src/app/shared/constants/provider-namex.const';
import { Actions } from 'src/app/shared/enums/actions.enum';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
})
export class RoleComponent implements OnInit, OnDestroy {
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
  public items: RoleDto[];
  public selectedItems: RoleDto[] = [];
  actionItem: RoleDto;
  public keyword: string = '';

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  hasPermissionManagementPermionsion = false;
  visibleActionColumn = false;
  actionMenu: MenuItem[];

  constructor(
    public layoutService: LayoutService,
    private roleService: RolesService,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService
  ) {}

  ngOnInit() {
    this.breadcrumb = [{ label: 'Quản lý vai trò' }];
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
    this.getPermission();
    this.buildActionMenu();
    this.loadData();
  }
  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('AbpIdentity.Users.Update');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('AbpIdentity.Users.Delete');
    this.hasPermissionManagementPermionsion = this.permissionService.getGrantedPolicy(
      'AbpIdentity.Users.ManagePermissions'
    );
    this.visibleActionColumn =
      this.hasPermissionUpdate ||
      this.hasPermissionDelete ||
      this.hasPermissionManagementPermionsion;
  }

  loadData() {
    this.layoutService.blockUI$.next(true);

    this.roleService
      .getListFilter({
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
        keyword: this.keyword,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<RoleDto>) => {
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
    const ref = this.dialogService.open(RoleDetailComponent, {
      header: 'Thêm vai trò',
      width: DIALOG_SM,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: RoleDto) => {
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
    const ref = this.dialogService.open(RoleDetailComponent, {
      data: {
        id: row.id,
      },
      header: `Cập nhật vai trò '${row.name}'`,
      width: DIALOG_SM,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: RoleDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
        this.selectedItems = [];
        this.loadData();
      }
    });
  }
  showPermissionModal(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const ref = this.dialogService.open(PermissionGrantComponent, {
      data: {
        providerKey: row.name,
        providerName: ROLE_PROVIDER,
      },
      header: `Phân quyền cho vai trò '${row.name}'`,
      width: DIALOG_SM,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: any) => {
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

    this.roleService
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

    this.roleService
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
        label: this.Actions.MANAGE_PERMISSIONS,
        icon: 'pi pi-fw pi-wrench',
        command: event => {
          this.showPermissionModal(this.actionItem);
          this.actionItem = null;
        },
        visible: this.hasPermissionManagementPermionsion,
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
