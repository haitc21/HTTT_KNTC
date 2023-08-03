import { PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject, takeUntil } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { GetSummaryListDto, SummaryChartDto, SummaryDto } from '../../proxy/summaries/models';
import { SummaryService } from '@proxy/summaries';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { congKhaiOptions, loaiKQOptions } from 'src/app/_shared/constants/consts';
import { Chart } from 'chart.js';
import ChartDataLabels from 'chartjs-plugin-datalabels';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  animations: [
    trigger('hideColumn', [
      state(
        'visible',
        style({
          width: '16.6666666667%',
          opacity: 1,
        })
      ),
      state(
        'hidden',
        style({
          width: 0,
          opacity: 0,
        })
      ),
      transition('visible => hidden', animate('0.3s')),
      transition('hidden => visible', animate('0.3s')),
    ]),
    trigger('expandColumn', [
      state(
        'normal',
        style({
          width: '83.3333333333%',
        })
      ),
      state(
        'expanded',
        style({
          width: '100%',
        })
      ),
      transition('normal => expanded', animate('0.3s')),
      transition('expanded => normal', animate('0.3s')),
    ]),
  ],
})
export class DashboardComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  home: MenuItem;
  breadcrumb: MenuItem[];
  dataChart: SummaryChartDto;

  blockedPanel = false;
  // items: SummaryDto[] = [];
  dataMap: SummaryDto[] = [];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 20;
  public totalCount: number;

  // filter
  landComplain = true;
  enviromentComplain = true;
  waterComplain = true;
  mineralComplain = true;

  landDenounce = true;
  enviromentDenounce = true;
  waterDenounce = true;
  mineralDenounce = true;
  filter: GetSummaryListDto;
  keyword: string = '';
  congKhai: boolean | null;
  maTinh: number = 24;
  maHuyen: number;
  maXa: number;
  thoiGianTiepNhanRange: Date[];
  tinhTrang: number;

  loaiKQOptions = loaiKQOptions;
  congKhaiOptions = congKhaiOptions;
  // ẩn hiện menu trái
  visibleFilterLeff = true;
  hideColumnState = 'visible';
  expandColumnState = 'normal';

  // Chart
  dataPieChart_KN: any;
  dataPieChart_TC: any;
  pieChartOptions_KN: any;
  pieChartOptions_TC: any;

  dataBarChart: any;
  barChartOptions: any;
  plugin = ChartDataLabels;

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    public layoutService: LayoutService,
    private oAuthService: OAuthService,
    private summaryService: SummaryService
  ) { }

  ngOnInit(): void {
    this.buildBreadcumb();
    //this.mockData = this.mockService.mockData();
    //this.loadGeo();
    this.loadData(true);
  }

  private buildBreadcumb() {
    this.breadcrumb = [{ label: 'Thống kê' }];
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
  }

  loadData(isFirst: boolean = false) {
    this.getDataChart();
    // this.getDataTable();
    this.getDataMap();
  }

  private getDataChart() {
    Chart.register(ChartDataLabels);
    this.layoutService.blockUI$.next(true);

    this.summaryService
      .getChart()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: SummaryChartDto) => {
          this.dataChart = res;
          this.buildPieChart();

          this.buildBarChart();

          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  private getDataMap() {
    this.layoutService.blockUI$.next(true);
    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword,

      landComplain: this.landComplain,
      enviromentComplain: this.enviromentComplain,
      waterComplain: this.waterComplain,
      mineralComplain: this.mineralComplain,
      landDenounce: this.landDenounce,
      enviromentDenounce: this.enviromentDenounce,
      waterDenounce: this.waterDenounce,
      mineralDenounce: this.mineralDenounce,

      maTinhTP: this.maTinh,
      maQuanHuyen: this.maHuyen,
      maXaPhuongTT: this.maXa,
      fromDate:
        this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[0]
          ? this.thoiGianTiepNhanRange[0].toUTCString()
          : null,
      toDate:
        this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
          ? this.thoiGianTiepNhanRange[1].toUTCString()
          : null,
      ketQua: this.tinhTrang,
      congKhai: this.hasLoggedIn ? this.congKhai : true,
    } as GetSummaryListDto;

    this.summaryService
      .getList(this.filter)
      //.getMap(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<SummaryDto>) => {
          this.dataMap = response.items;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }


  buildPieChart() {
    const labels = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];
    const totalKN = this.dataChart.landComplain +
      this.dataChart.enviromentComplain +
      this.dataChart.waterComplain +
      this.dataChart.mineralComplain;
    this.dataPieChart_KN = {
      labels: labels,
      datasets: [
        {
          data: [
            this.dataChart.landComplain,
            this.dataChart.enviromentComplain,
            this.dataChart.waterComplain,
            this.dataChart.mineralComplain,
          ],
          backgroundColor: ['#c00000', '#00af50', '#01b0f1', '#f4b083'],
          hoverBackgroundColor: ['#d24d4d', '#4dc785', '#4ec8f5', '#f7c8a8'],
        },
      ],
    };

    this.pieChartOptions_KN = {
      plugins: {
        title: {
          display: true,
          text: 'Khiếu nại',
          font: {
            size: 16,
            weight: 'bold'
          }
        },
        maintainAspectRatio: false,
        responsive: true,
        legend: {
          display: false,
          labels: {
            color: '#495057',
          },
        },
        datalabels: {
          // color: '#000000',
          color: '#fff',
          font: {
            size: 14,
          },
          formatter: (value, ctx) => {
            const percentage = (value * 100 / totalKN);
            return percentage !== 0 ? `${labels[ctx.dataIndex]} ${percentage.toFixed(2)}%` : '';
          }
        }
      }
    };

    this.dataPieChart_TC = {
      labels: labels,
      datasets: [
        {
          data: [
            this.dataChart.landDenounce,
            this.dataChart.enviromentDenounce,
            this.dataChart.waterDenounce,
            this.dataChart.mineralDenounce,
          ],
          backgroundColor: ['#c00000', '#00af50', '#01b0f1', '#f4b083'],
          hoverBackgroundColor: ['#d24d4d', '#4dc785', '#4ec8f5', '#f7c8a8'],
        },
      ],
    };
    this.pieChartOptions_TC = {
      plugins: {
        title: {
          display: true,
          text: 'Tố cáo',
          font: {
            size: 16,
            weight: 'bold'
          }
        },
        maintainAspectRatio: false,
        responsive: true,
        legend: {
          display: false,
          labels: {
            color: '#495057',
          },
        },
        datalabels: {
          // color: '#000000',
          color: '#fff',
          font: {
            size: 14,
          },
          formatter: (value, ctx) => {
            const percentage = (value * 100 / totalKN);
            return percentage !== 0 ? `${labels[ctx.dataIndex]} ${percentage.toFixed(2)}%` : '';
          },
        }
      }
    };
  }


  private buildBarChart() {
    this.dataBarChart = {
      labels: [
        'KN Đất đai',
        'KN Môi trường',
        'KN tài nguyên nước',
        'KN Khoáng sản',
        'TC Đất đai',
        'TC Môi trường',
        'TC tài nguyên nước',
        'TC Khoáng sản',
      ],
      datasets: [
        {
          type: 'bar',
          label: 'Chưa có KQ',
          backgroundColor: '#a7d08c',
          data: [
            this.dataChart.landComplain_ChuaCoKQ,
            this.dataChart.enviromentComplain_ChuaCoKQ,
            this.dataChart.waterComplain_ChuaCoKQ,
            this.dataChart.mineralComplain_ChuaCoKQ,

            this.dataChart.landDenounce_ChuaCoKQ,
            this.dataChart.enviromentDenounce_ChuaCoKQ,
            this.dataChart.waterDenounce_ChuaCoKQ,
            this.dataChart.mineralDenounce_ChuaCoKQ,
          ],
        },
        {
          type: 'bar',
          label: 'Đúng',
          backgroundColor: '#fe0000',
          data: [
            this.dataChart.landComplain_Dung,
            this.dataChart.enviromentComplain_Dung,
            this.dataChart.waterComplain_Dung,
            this.dataChart.mineralComplain_Dung,

            this.dataChart.landDenounce_Dung,
            this.dataChart.enviromentDenounce_Dung,
            this.dataChart.waterDenounce_Dung,
            this.dataChart.mineralDenounce_Dung,
          ],
        },
        {
          type: 'bar',
          label: 'Có Đúng/Có Sai',
          backgroundColor: '#fed966',
          data: [
            this.dataChart.landComplain_CoDungCoSai,
            this.dataChart.enviromentComplain_CoDungCoSai,
            this.dataChart.waterComplain_CoDungCoSai,
            this.dataChart.mineralComplain_CoDungCoSai,

            this.dataChart.landDenounce_CoDungCoSai,
            this.dataChart.enviromentDenounce_CoDungCoSai,
            this.dataChart.waterDenounce_CoDungCoSai,
            this.dataChart.mineralDenounce_CoDungCoSai,
          ],
        },
        {
          type: 'bar',
          label: 'Sai',
          backgroundColor: '#aeaaa9',
          data: [
            this.dataChart.landComplain_Sai,
            this.dataChart.enviromentComplain_Sai,
            this.dataChart.waterComplain_Sai,
            this.dataChart.mineralComplain_Sai,

            this.dataChart.landDenounce_Sai,
            this.dataChart.enviromentDenounce_Sai,
            this.dataChart.waterDenounce_Sai,
            this.dataChart.mineralDenounce_Sai,
          ],
        },
      ],
    };
    this.barChartOptions = {
      plugins: {
        maintainAspectRatio: false,
        responsive: true,
        legend: {
          labels: {
            color: '#495057',
          },
        },
        // Change options for ALL labels of THIS CHART
        datalabels: {
          color: '#000000',
          font: {
            size: 14,
          }
        }
      },
      scales: {
        x: {
          stacked: true,
          ticks: {
            color: '#495057',
          },
          grid: {
            color: '#ebedef',
          },
        },
        y: {
          stacked: true,
          ticks: {
            color: '#495057',
          },
          grid: {
            color: '#ebedef',
          },
        },
      },
    };
  }

  toggleMenuLeft() {
    this.visibleFilterLeff = !this.visibleFilterLeff;
    if (!this.visibleFilterLeff) {
      this.hideColumnState = 'hidden';
      this.expandColumnState = 'expanded';
    } else {
      this.hideColumnState = 'visible';
      this.expandColumnState = 'normal';
    }
  }

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
