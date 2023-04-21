import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PrimeNGConfig } from 'primeng/api';
import { LOGIN_URL } from './shared/constants/urls.const';
import { OAuthService } from 'angular-oauth2-oidc';
import { LayoutService } from './layout/service/app.layout.service';

@Component({
  selector: 'app-root',
  template: `
    <p-panel #layoutPnl>
      <router-outlet></router-outlet>
      <p-toast position="top-right"></p-toast>
      <p-confirmDialog
        header="Xác nhận"
        acceptLabel="Có"
        rejectLabel="Không"
        icon="pi pi-exclamation-triangle"
      ></p-confirmDialog>
      <p-blockUI [target]="layoutPnl" [blocked]="blockedLayout">
        <p-progressSpinner
          [style]="{
            width: '150px',
            height: '150px',
            position: 'absolute',
            top: '50%',
            left: '50%',
            transform: 'translate(-50%, -50%)'
          }"
          strokeWidth="2"
          animationDuration=".5s"
        ></p-progressSpinner>
      </p-blockUI>
    </p-panel>
  `,
})
export class AppComponent {
  menuMode = 'static';
  blockedLayout = false;

  constructor(
    private primengConfig: PrimeNGConfig,
    public layoutService: LayoutService,
    private oAuthService: OAuthService,
    private router: Router
  ) {
    this.layoutService.blockUI$.subscribe(block => {
      if (block == true) {
        setTimeout(() => {
          this.blockedLayout = true;
        }, 300);
      } else {
        setTimeout(() => {
          this.blockedLayout = false;
        }, 300);
      }
    });
  }

  ngOnInit() {
    this.primengConfig.ripple = true;
    document.documentElement.style.fontSize = '14px';
    this.primengConfig.setTranslation({
      firstDayOfWeek: 0,
      dayNames: ['Chủ nhật', 'Thứ hai', 'Thứ ba', 'Thứ tư', 'Thứ năm', 'Thứ sáu', 'Thứ bảy'],
      dayNamesShort: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
      dayNamesMin: ['CN', 'T2', 'T3', 'T4', 'T5', 'T6', 'T7'],
      monthNames: [
        'Tháng 01',
        'Tháng 02',
        'Tháng 03',
        'Tháng 04',
        'Tháng 05',
        'Tháng 06',
        'Tháng 07',
        'Tháng 08',
        'Tháng 09',
        'Tháng 10',
        'Tháng 11',
        'Tháng 12',
      ],
      monthNamesShort: [
        'Th 1',
        'Th 2',
        'Th 3',
        'Th 4',
        'Th 5',
        'Th 6',
        'Th 7',
        'Th 8',
        'Th 9',
        'Th 10',
        'Th 11',
        'Th 12',
      ],
      today: 'Hôm nay',
      clear: 'Xóa',
      dateFormat: 'dd/mm/yy',
      weekHeader: 'Tuần',
    });
    // if (!this.oAuthService.hasValidAccessToken()) {
    //   this.router.navigate([LOGIN_URL]);
    // }
  }
}
