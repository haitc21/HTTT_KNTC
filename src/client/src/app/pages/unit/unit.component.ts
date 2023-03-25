import { PagedResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { UnitDto, UnitService } from '@proxy/units';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { DIALOG_MD } from 'src/app/shared/constants/sizes.const';
import { Actions } from 'src/app/shared/enums/actions.enum';
import { UnitDetailComponent } from './detail/unit-detail.component';

@Component({
  selector: 'app-unit',
  templateUrl: './unit.component.html',
})
export class UnitComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  public blockedPanel: boolean = false;

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;
  Actions = Actions;

  //Business variables
  public items: UnitDto[];
  public selectedItems: UnitDto[] = [];
  actionItem: UnitDto;
  public keyword: string = '';

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;
  actionMenu: MenuItem[];

  constructor(
    private unitService: UnitService,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService
  ) {}

  ngOnInit() {
    this.getPermission();
    this.buildActionMenu();
    this.loadData();
  }
  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('Unit.Edit');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('Unit.Delete');
    this.visibleActionColumn = this.hasPermissionUpdate || this.hasPermissionDelete;
  }

  loadData() {
    this.toggleBlockUI(true);

    this.unitService
      .getList({
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
        keyword: this.keyword,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<UnitDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  showAddModal() {
    const ref = this.dialogService.open(UnitDetailComponent, {
      header: 'Thêm địa danh',
      width: DIALOG_MD,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: UnitDto) => {
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

  showEditModal(row: any) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const ref = this.dialogService.open(UnitDetailComponent, {
      data: {
        id: row.id,
      },
      header: `Cập nhật địa danh`,
      width: DIALOG_MD,
    });

    ref.onClose.pipe(takeUntil(this.ngUnsubscribe)).subscribe((data: UnitDto) => {
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

    this.unitService
      .deleteMultiple(ids)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
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
    this.toggleBlockUI(true);

    this.unitService
      .delete(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
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
  setActionItem(item) {
    this.actionItem = item;
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
