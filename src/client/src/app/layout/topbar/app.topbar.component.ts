import { Component, ElementRef, OnDestroy, OnInit, ViewChild, isDevMode } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { OAuthService } from 'angular-oauth2-oidc';
import { PermissionService } from '@abp/ng.core';
import { LayoutService } from '../service/app.layout.service';
import { LOGIN_URL } from 'src/app/shared/constants/urls.const';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from 'src/app/shared/services/file.service';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { ProfileComponent } from 'src/app/system/user/profile/profile.component';
import { DIALOG_MD, DIALOG_SM } from 'src/app/shared/constants/sizes.const';
import { UserInfoDto } from '@proxy/users';
import { DialogService } from 'primeng/dynamicdialog';
import { SetPasswordComponent } from 'src/app/system/user/set-password/set-password.component';
import { LinhVuc } from '@proxy';
import { GetSysConfigService } from 'src/app/shared/services/sysconfig.services';
import { Subject, forkJoin, takeUntil } from 'rxjs';
import { SysConfigConsts } from 'src/app/shared/constants/sys-config.consts';

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
    private sysConfigService: GetSysConfigService
  ) {}
  ngOnInit(): void {
    this.getSysConfigAmdInitMenu();
    if (this.isAutenticated) {
      const accessToken = this.oAuthService.getAccessToken();
      let decodedAccessToken = atob(accessToken.split('.')[1]);
      let accessTokenJson = JSON.parse(decodedAccessToken);
      this.userName = accessTokenJson.preferred_username ?? '';
      this.userId = accessTokenJson.sub ?? '';
      this.getAvatar();
      this.fileService.avatarUrl$.subscribe(url => {
        if (url) this.avatarUrl = url; // Cập nhật đường dẫn tới avatar của người dùng
      });
    }
  }

  getSysConfigAmdInitMenu() {
    this.layoutService.blockUI$.next(true);
    let getGeoServerDomain$ = this.sysConfigService.getSysConfig(SysConfigConsts.GEOSERVER_DOMAIN);
    let getTitle$ = this.sysConfigService.getSysConfig(SysConfigConsts.TITLE);

    forkJoin([getGeoServerDomain$, getTitle$])
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        ([geoServerDomain, title]) => {
          if (geoServerDomain) this.geoserverUrl = geoServerDomain.value;
          if (title) this.title = title.value;
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
        routerLink: ['/pages/map'],
      },
      {
        label: 'Khiếu nại',
        icon: 'pi pi-fw pi-envelope',
        items: [
          {
            label: 'Đất đai',
            routerLink: [`/pages/complain/${LinhVuc.DatDai}`],
          },
          {
            label: 'Môi trường',
            routerLink: [`/pages/complain/${LinhVuc.MoiTruong}`],
          },

          {
            label: 'Khoáng sản',
            routerLink: [`/pages/complain/${LinhVuc.KhoangSan}`],
          },
          {
            label: 'Tài nguyên nước',
            routerLink: [`/pages/complain/${LinhVuc.TaiNguyenNuoc}`],
          },
        ],
      },
      {
        label: 'Tố cáo',
        icon: 'fa fa-balance-scale',
        items: [
          {
            label: 'Đất đai',
            routerLink: [`/pages/denounce/${LinhVuc.DatDai}`],
          },
          {
            label: 'Môi trường',
            routerLink: [`/pages/denounce/${LinhVuc.MoiTruong}`],
          },

          {
            label: 'Khoáng sản',
            routerLink: [`/pages/denounce/${LinhVuc.KhoangSan}`],
          },
          {
            label: 'Tài nguyên nước',
            routerLink: [`/pages/denounce/${LinhVuc.TaiNguyenNuoc}`],
          },
        ],
      },
      {
        label: 'Thống kê',
        icon: 'fa fa-chart-bar',
        items: [
          {
            label: 'Thống kê',
            routerLink: ['/pages/dashboard'],
          },
          {
            label: 'Sổ theo dõi KN/TC',
            routerLink: ['/pages/reports/logbook'],
          },
          /*
          {
            label: 'Tổng hợp kết quả KN/TC',
            routerLink: ['/pages/reports/summary'],
          },
          */
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
