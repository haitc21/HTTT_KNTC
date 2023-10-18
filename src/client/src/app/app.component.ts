import { AfterViewInit, Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { LayoutService } from './layout/service/app.layout.service';
import { GetSysConfigService } from './_shared/services/sysconfig.services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit, AfterViewInit {
  menuMode = 'static';
  blockedLayout = false;
  configs: any;

  constructor(
    private primengConfig: PrimeNGConfig,
    public layoutService: LayoutService,
    private sysConfigService: GetSysConfigService
  ) { }

  ngOnInit() {
    document.documentElement.style.fontSize = '14px';
    this.getConfigs();
    this.setBlockUi();
    this.configPrimeng();
  }
  ngAfterViewInit() {
    // Hide the spinner when the document is ready
    const spinnerElement = document.getElementById('nb-global-spinner');
    if (spinnerElement) {
      spinnerElement.style.display = 'none';
    }
  }

  private getConfigs() {
    this.sysConfigService.getAll().subscribe(
      data => {
        this.configs = data;
      },
      err => { }
    );
  }

  private configPrimeng() {
    this.primengConfig.ripple = true;
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
      weak: "mật khẩu yếu",
      medium: "mật khẩu trung bình",
      strong: "mật khẩu mạnh"
    });
  }

  private setBlockUi() {
    this.layoutService.blockUI$.subscribe(block => {
      if (block === true) {
        setTimeout(() => {
          this.blockedLayout = true;
        }, 1);
      }
      if (block === false) {
        setTimeout(() => {
          this.blockedLayout = false;
        }, 500);
      }
    });
  }
}
