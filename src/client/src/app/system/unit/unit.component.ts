import { ListResultDto, PagedResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { UnitDto, UnitLookupDto, UnitService } from '@proxy/units';
import { ConfirmationService, MenuItem } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { DIALOG_MD } from 'src/app/_shared/constants/sizes.const';
import { Actions } from 'src/app/_shared/enums/actions.enum';
import { UnitDetailComponent } from './detail/unit-detail.component';
import { UnitTypeLookupDto, UnitTypeService } from '@proxy/category-unit-types';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
  selector: 'app-unit',
  templateUrl: './unit.component.html',
})
export class UnitComponent implements OnInit, OnDestroy {
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
  public items: UnitDto[];
  public selectedItems: UnitDto[] = [];
  actionItem: UnitDto;
  public keyword: string = '';
  unitTypeId: number = 1;
  parentId: number;

  unitTypeOptions: UnitTypeLookupDto[] = [];
  parentUnitOptions: UnitLookupDto[] = [];

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;
  actionMenu: MenuItem[];

  constructor(
    public layoutService: LayoutService,
    private unitService: UnitService,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService,
    private unitTypeService: UnitTypeService
  ) {}

  ngOnInit() {
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
    this.breadcrumb = [{ label: ' Quản trị hệ thống', icon: 'pi pi-cog', disabled: true }];
    this.breadcrumb.push({
      label: ' Danh mục địa danh',
      icon: 'pi pi-flag',
    });

    this.getOptions();
    this.getPermission();
    this.buildActionMenu();
    this.loadData();
  }

  getOptions() {
    this.layoutService.blockUI$.next(true);
    this.unitTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitTypeLookupDto>) => {
          this.unitTypeOptions = res.items;
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  unitTypeChange(id, isFirst: boolean = false) {
    if (!id) return;
    if (id > 1) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(id - 1)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.parentUnitOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.parentUnitOptions = [];
            this.layoutService.blockUI$.next(false);
          }
        );
    } else {
      this.parentUnitOptions = [];
      this.parentId = null;
    }
  }

  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('Units.Edit');
    this.hasPermissionDelete = this.permissionService.getGrantedPolicy('Units.Delete');
    this.visibleActionColumn = this.hasPermissionUpdate || this.hasPermissionDelete;
  }

  loadData() {
    this.layoutService.blockUI$.next(true);

    this.unitService
      .getList({
        maxResultCount: this.maxResultCount,
        skipCount: this.skipCount,
        keyword: this.keyword,
        unitTypeId: this.unitTypeId,
        parentId: this.parentId,
      })
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<UnitDto>) => {
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

  showUpdateModal(row: any) {
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

    this.unitService
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

    this.unitService
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
