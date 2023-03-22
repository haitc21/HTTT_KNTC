import { PermissionService } from '@abp/ng.core';
import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LinhVuc } from '@proxy';
import { MenuItem } from 'primeng/api';
import { LayoutService } from '../service/app.layout.service';

@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html',
})
export class AppMenuComponent implements OnInit {
  // model: any[] = [];
  items: MenuItem[];

  constructor(public layoutService: LayoutService, private permissionService: PermissionService) {}

  ngOnInit() {
    // this.model = [
    //   {
    //     label: 'Trang chủ',
    //     items: [{ label: 'Dashboard', icon: 'pi pi-fw pi-home', routerLink: ['/'] }],
    //   },
    //   {
    //     label: 'Hệ thống',
    //     items: [
    //       {
    //         label: 'Danh sách quyền',
    //         icon: 'pi pi-fw pi-circle',
    //         routerLink: ['/system/role'],
    //         permission: 'AbpIdentity.Roles',
    //       },
    //       {
    //         label: 'Danh sách người dùng',
    //         icon: 'pi pi-fw pi-circle',
    //         routerLink: ['/system/user'],
    //         permission: 'AbpIdentity.Users',
    //       },
    //     ],
    //   },
    // ];
    this.items = [
      {
        label: 'Trang chủ',
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
            routerLink: [`/pages/complain/${LinhVuc.DataDai}`],
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
            routerLink: [`/pages/denounce/${LinhVuc.DataDai}`],
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
    ];
  }
}
