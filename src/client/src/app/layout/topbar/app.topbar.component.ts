import { Component, ElementRef, OnDestroy, OnInit, ViewChild, isDevMode } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { OAuthService } from 'angular-oauth2-oidc';
import { ConfigStateService, PermissionService } from '@abp/ng.core';
import { LayoutService } from '../service/app.layout.service';
import { LOGIN_URL } from 'src/app/_shared/constants/urls.const';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from 'src/app/_shared/services/file.service';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { ProfileComponent } from 'src/app/system/user/profile/profile.component';
import { DIALOG_MD, DIALOG_SM } from 'src/app/_shared/constants/sizes.const';
import { UserInfoDto } from '@proxy/users';
import { DialogService } from 'primeng/dynamicdialog';
import { SetPasswordComponent } from 'src/app/system/user/set-password/set-password.component';
import { LinhVuc } from '@proxy';
import { GetSysConfigService } from 'src/app/_shared/services/sysconfig.services';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { SysConfigConsts } from 'src/app/_shared/constants/sys-config.consts';

@Component({
  selector: 'app-topbar',
  templateUrl: './app.topbar.component.html',
  styleUrls: ['./app.topbar.component.scss'],
})
export class AppTopBarComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  items!: MenuItem[];
  userMenuItems: MenuItem[];
  systemMenuItems: MenuItem[];

  @ViewChild('menubutton') menuButton!: ElementRef;

  @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

  @ViewChild('topbarmenu') menu!: ElementRef;

  userName = '';
  userId = '';
  avatarUrl: any;
  geoserverUrl: string;
  title: string;

  get isAutenticated() {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    public layoutService: LayoutService,
    private router: Router,
    private oAuthService: OAuthService,
    private permissionService: PermissionService,
    private fileService: FileService,
    private notificationService: NotificationService,
    public dialogService: DialogService,
    private sanitizer: DomSanitizer,
    private sysConfigService: GetSysConfigService,
    private config: ConfigStateService
  ) {}
  ngOnInit(): void {
    this.getSysConfigAmdInitMenu();
    this.getCurrentUser();
  }
  getCurrentUser() {
    if (this.isAutenticated) {
      this.config
        .getOne$('currentUser')
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(currentUser => {
          if (currentUser) {
            this.userName = currentUser.name;
            this.userId = currentUser.id;
          }
        });

      this.getAvatar();
      this.fileService.avatarUrl$.subscribe(url => {
        if (url) this.avatarUrl = url;
      });
    }
  }

  getSysConfigAmdInitMenu() {
    this.layoutService.blockUI$.next(true);
    const cacheKey = SysConfigConsts.Prefix;
    const storedConfig = localStorage.getItem(cacheKey);
    if (storedConfig) {
      const { value, expiry } = JSON.parse(storedConfig);
      const currentTime = new Date().getTime();
      if (currentTime < expiry) {
        let configs = value.items;
        this.geoserverUrl = configs.find(x => x.name === SysConfigConsts.GEOSERVER_DOMAIN)?.value;
        this.title = configs.find(x => x.name == SysConfigConsts.TITLE)?.value;
        this.layoutService.blockUI$.next(false);
        this.initMenu();
        return;
      }
    }
    this.sysConfigService
      .getSysAll()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        data => {
          let configs = data.items;
          if (configs) {
            this.geoserverUrl = configs.find(
              x => x.name == SysConfigConsts.GEOSERVER_DOMAIN
            )?.value;
            this.title = configs.find(x => x.name == SysConfigConsts.TITLE)?.value;
          }
          this.initMenu();
          this.layoutService.blockUI$.next(false);
        },
        err => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }
  initMenu() {
    this.userMenuItems = [
      {
        label: 'Thông tin cá nhân',
        // icon: 'pi pi-id-card',
        command: event => {
          this.profileModal();
        },
      },
      {
        label: 'Đổi mật khẩu',
        // icon: 'pi pi-key',
        command: event => {
          this.setPassword();
        },
      },
      {
        label: 'Đăng xuất',
        // icon: 'pi pi-sign-out',
        command: event => {
          this.oAuthService.logOut();
          window.location.reload();
          // this.router.navigate(['/']);
        },
      },
    ];
    this.items = [
      {
        icon: 'pi pi-fw pi-home',
        routerLink: ['/'],
      },
      {
        label: 'Bản đồ',
        icon: 'pi pi-fw pi-map-marker',
        routerLink: ['/map'],
      },
      {
        label: 'Khiếu nại',
        icon: 'pi pi-fw pi-envelope',
        items: [
          {
            label: 'Đất đai',
            routerLink: [`/complain/${LinhVuc.DatDai}`],
          },
          {
            label: 'Môi trường',
            routerLink: [`/complain/${LinhVuc.MoiTruong}`],
          },

          {
            label: 'Khoáng sản',
            routerLink: [`/complain/${LinhVuc.KhoangSan}`],
          },
          {
            label: 'Tài nguyên nước',
            routerLink: [`/complain/${LinhVuc.TaiNguyenNuoc}`],
          },
        ],
      },
      {
        label: 'Tố cáo',
        icon: 'fa fa-balance-scale',
        items: [
          {
            label: 'Đất đai',
            routerLink: [`/denounce/${LinhVuc.DatDai}`],
          },
          {
            label: 'Môi trường',
            routerLink: [`/denounce/${LinhVuc.MoiTruong}`],
          },

          {
            label: 'Khoáng sản',
            routerLink: [`/denounce/${LinhVuc.KhoangSan}`],
          },
          {
            label: 'Tài nguyên nước',
            routerLink: [`/denounce/${LinhVuc.TaiNguyenNuoc}`],
          },
        ],
      },
      {
        label: 'Báo cáo/Thống kê',
        icon: 'fa fa-chart-bar',
        items: [
          {
            label: 'Thống kê',
            routerLink: ['/dashboard'],
          },
          {
            label: 'Báo cáo KN/TC',
            routerLink: ['/reports/report'],
          },
          {
            label: 'Sổ theo dõi KN/TC',
            routerLink: ['/reports/logbook'],
          },
        ],
      },
      {
        label: 'Hệ thống',
        icon: 'fa fa-cogs',
        visible: this.isAutenticated,
        items: [
          {
            label: 'Cấu hình hệ thống',
            routerLink: ['/system/sys-config'],
            visible: this.permissionService.getGrantedPolicy('SysConfigs'),
          },
          {
            label: 'Quản lý bản đồ quy hoạch',
            url: `${this.geoserverUrl}/geoserver/web/`,
            visible: this.permissionService.getGrantedPolicy('GeoServesrs'),
          },
          {
            label: 'Quản lý người dùng',
            // icon: 'pi pi-fw pi-users',
            routerLink: ['/system/user'],
            visible: this.permissionService.getGrantedPolicy('AbpIdentity.Users'),
          },
          {
            label: 'Quản trị phân quyền',
            // icon: 'pi pi-fw pi-user-edit',
            routerLink: ['/system/role'],
            visible: this.permissionService.getGrantedPolicy('AbpIdentity.Roles'),
          },
          {
            label: 'Danh mục',
            visible: this.isAutenticated,
            items: [
              {
                label: 'Loại địa danh',
                routerLink: [`/system/unit-type`],
                visible: this.permissionService.getGrantedPolicy('UnitTypes'),
              },
              {
                label: 'Địa danh',
                routerLink: [`/system/unit`],
                visible: this.permissionService.getGrantedPolicy('Units'),
              },
              {
                label: 'Phân loại đất',
                routerLink: [`/system/land-type`],
                visible: this.permissionService.getGrantedPolicy('LandTypes'),
              },
              {
                label: 'Hình thức tệp',
                routerLink: [`/system/document-type`],
                visible: this.permissionService.getGrantedPolicy('DocumentTypes'),
              },
            ],
          },
        ],
      },
    ];
  }
  login() {
    this.router.navigate([LOGIN_URL, this.router.url]);
  }
  getAvatar() {
    this.fileService
      .getAvatar(this.userId)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(data => {
        if (data) {
          let objectURL = 'data:image/png;base64,' + data;
          this.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
        }
      });
  }
  profileModal() {
    if (!this.userId) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const ref = this.dialogService.open(ProfileComponent, {
      data: {
        id: this.userId,
      },
      header: `Thông tin cá nhân'`,
      width: DIALOG_MD,
    });

    ref.onClose.subscribe((data: UserInfoDto) => {
      if (data) {
        this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
      }
    });
  }
  setPassword() {
    if (!this.userId) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    const ref = this.dialogService.open(SetPasswordComponent, {
      data: {
        id: this.userId,
      },
      header: `Đặt lại mật khẩu`,
      width: DIALOG_SM,
    });
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
