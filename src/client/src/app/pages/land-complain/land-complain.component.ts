import { AuthService } from '@abp/ng.core';
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

  //public selectedItems: Complain[] = [];
  filter: GetComplainListDto;
  public keyword: string = '';
  public items: any[];
  public selectedItems: ComplainDto[] = [];
  actionItem: ComplainDto;
  //emailSearch: string = '';
  //phoneNumberSearch: string = '';
  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  //lanKNSearch: Number;
  //stageSearch: Number;
  public MaTinh: number;
  public MaHuyen: number;
  public MaPhuong: number;
  public ThoiGianNop: Date;
  public GiaiDoan: number;
  public TinhTrang: number;

  MaTinhOptions = [];
  MaHuyenOptions = [];
  MaPhuongOptions = [];

  hasPermissionUpdate = false;
  hasPermissionDelete = false;
  visibleActionColumn = false;

  Actions = Actions;
  actionMenu: MenuItem[];

  lanKNOptions = [
    { value: 0, text: 'Khiếu nại lần I' },
    { value: 1, text: 'Khiếu nại lần II' },
  ];
  stageOptions = [
    { value: 1, text: 'Đúng' },
    { value: 2, text: 'Sai' },
    { value: 3, text: 'Có Đúng/Có Sai' },
  ];

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private oAuthService: OAuthService,
    private authService: AuthService,
    //private mockService: MockService,
    public dialogService: DialogService,
    private notificationService: NotificationService,
    private confirmationService: ConfirmationService,
    private permissionService: PermissionService,
    private complainService: ComplainService,
    private sanitizer: DomSanitizer,
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
    //load all options here
  }

  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);
    this.data = [];    

    /*
    this.data.push(
      ...this.mockData.filter(
        x => x.typeHoSo === typesHoSo.Complain && x.fieldType === fieldsHoSo.Land
      )
    );
    */
    debugger;
    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword
      // MaTinh: this.MaTinh,
      // MaHuyen: this.MaHuyen,
      // MaPhuong: this.MaPhuong,
      // ThoiGianNop: this.ThoiGianNop,
      // GiaiDoan: this.GiaiDoan,
      // TinhTrang: this.TinhTrang
    } as GetComplainListDto;
    debugger;
    //this.data 
    this.complainService.getList(this.filter)
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe({
      next: (response: PagedResultDto<ComplainDto>) => {
        this.items = response.items;
        /*
        this.items.forEach(x => {
          if (x.avatarContent) {
            let objectURL = 'data:image/png;base64,' + x.avatarContent;
            x.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
          }
        });
        */
        this.totalCount = response.totalCount;
        this.toggleBlockUI(false);
      },
      error: () => {
        this.toggleBlockUI(false);
      },
    });
    /*
    this.userService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<UserListDto>) => {
          this.items = response.items;
          this.items.forEach(x => {
            if (x.avatarContent) {
              let objectURL = 'data:image/png;base64,' + x.avatarContent;
              x.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
            }
          });
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
    let t = this.complainService.getList();
    */
/*
    this.data.push(
      ...this.ComplainData.filter(
        x => x.typeHoSo === typesHoSo.Complain && x.fieldType === fieldsHoSo.Land
      )
    );
*/
/*
    if (this.keyword) {
      this.data = this.data.filter(
        x => x.code.includes(this.keyword) || x.title.includes(this.keyword)
      );
    }
    switch (this.GiaiDoan) {
      case 0:
        this.data = this.data.filter(x => !x.returnDate2);
        break;
      case 1:
        this.data = this.data.filter(x => x.returnDate2);
        break;
      default:
        this.data = this.data;
    }
    switch (this.TinhTrang) {
      case 0:
        this.data = this.data.filter(x => x.result1 || x.result2);
        break;
      case 1:
        this.data = this.data.filter(x => x.result2 === false);
        break;
      default:
        this.data = this.data;
    }
*/
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

    this.visibleActionColumn =
      this.hasPermissionUpdate ||
      this.hasPermissionDelete;
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
