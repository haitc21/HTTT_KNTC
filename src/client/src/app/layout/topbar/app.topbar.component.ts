import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { OAuthService } from 'angular-oauth2-oidc';
import { PermissionService } from '@abp/ng.core';
import { LayoutService } from '../service/app.layout.service';
import { LOGIN_URL } from 'src/app/shared/constants/urls.const';
import { DomSanitizer } from '@angular/platform-browser';
import { FileService } from 'src/app/shared/services/file.service.spec';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { ProfileComponent } from 'src/app/system/user/profile/profile.component';
import { DIALOG_MD, DIALOG_SM } from 'src/app/shared/constants/sizes.const';
import { UserInfoDto } from '@proxy/users';
import { DialogService } from 'primeng/dynamicdialog';
import { SetPasswordComponent } from 'src/app/system/user/set-password/set-password.component';

@Component({
  selector: 'app-topbar',
  templateUrl: './app.topbar.component.html',
  styleUrls: ['./app.topbar.component.scss'],
})
export class AppTopBarComponent implements OnInit {
  items!: MenuItem[];
  userMenuItems: MenuItem[];
  systemMenuItems: MenuItem[];

  @ViewChild('menubutton') menuButton!: ElementRef;

  @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

  @ViewChild('topbarmenu') menu!: ElementRef;

  isAutenticated: boolean = false;
  userName = '';
  userId = '';
  avatarUrl: any;

  constructor(
    public layoutService: LayoutService,
    private router: Router,
    private oAuthService: OAuthService,
    private permissionService: PermissionService,
    private fileService: FileService,
    private notificationService: NotificationService,
    public dialogService: DialogService,
    private sanitizer: DomSanitizer
  ) {}
  ngOnInit(): void {
    this.isAutenticated = this.oAuthService.hasValidAccessToken();
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
    this.initMenu();
    this.initMenuUser();
    this.initMenuSystem();
  }
  initMenuUser() {
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
          this.router.navigate(['/']);
        },
      },
    ];
  }

  initMenu() {
    this.items = [
      {
        icon: 'pi pi-fw pi-home',
        routerLink: ['/'],
      },
      {
        label: 'Dash Board',
        icon: 'pi pi-fw pi-calendar',
        routerLink: ['/pages/dashboard'],
      },
      {
        label: 'Bản đồ',
        icon: 'pi pi-fw pi-map',
        routerLink: ['/pages/map'],
      },
      {
        label: 'Khiếu nại',
        icon: 'pi pi-fw pi-envelope',
        items: [
          {
            label: 'Đất đai',
            routerLink: ['/'],
          },
          {
            label: 'Môi trường',
            routerLink: ['/'],
          },

          {
            label: 'Khoáng sản',
            routerLink: ['/'],
          },
          {
            label: 'Tài nguyên nước',
            routerLink: ['/'],
          },
        ],
      },
      {
        label: 'Tố cáo',
        icon: 'fa fa-balance-scale',
        items: [
          {
            label: 'Đất đai',
            routerLink: ['/'],
          },

          {
            label: 'Môi trường',
            routerLink: ['/'],
          },

          {
            label: 'Khoáng sản',
            routerLink: ['/'],
          },
          {
            label: 'Tài nguyên nước',
            routerLink: ['/'],
          },
        ],
      },
    ];
  }

  initMenuSystem() {
    this.systemMenuItems = [
      {
        label: 'Cấu hình hệ thống',
        routerLink: ['/'],
      },
      {
        label: 'Quản lý ngời dùng',
        // icon: 'pi pi-fw pi-users',
        routerLink: ['/system/user'],
        visible: this.permissionService.getGrantedPolicy('AbpIdentity.Users'),
      },
      {
        label: 'Quản lý vai trò',
        // icon: 'pi pi-fw pi-user-edit',
        routerLink: ['/system/role'],
        visible: this.permissionService.getGrantedPolicy('AbpIdentity.Roles'),
      },
      {
        label: 'Danh mục',
        routerLink: ['/'],
      },
    ];
  }

  login() {
    this.router.navigate([LOGIN_URL, this.router.url]);
  }
  getAvatar() {
    this.fileService.getAvatar(this.userId).subscribe(data => {
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
}
