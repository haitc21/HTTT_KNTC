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
    this.items = [
      {
        label: 'Trang chủ',
        icon: 'pi pi-fw pi-home',
        routerLink: ['/'],
      },
      {
        label: 'Thống kê',
        icon: 'pi pi-fw pi-calendar',
        routerLink: ['/dashboard'],
      },
      {
        label: 'Bản đồ',
        icon: 'pi pi-fw pi-map',
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
    ];
  }
}
