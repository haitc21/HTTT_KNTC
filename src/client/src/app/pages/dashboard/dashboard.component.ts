import { AuthService } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HoSo, fieldsHoSo, typesHoSo } from '../../shared/mock/HoSo';
import { MockService } from '../../shared/mock/mock.service';

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
export class DashboardComponent implements OnInit {
  blockedPanel = false;
  @ViewChild('map', { static: true }) mapContainer: ElementRef;
  isMapInitialised = false;
  data: HoSo[] = [];

  mockData: HoSo[] = [];
  loaiHS = ['khiếu nại', 'Tố cáo'];
  linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];
  typesHoSo = typesHoSo;
  fieldsHoSo = fieldsHoSo;

  public selectedItems: HoSo[] = [];
  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  // fileter
  landComplaint = true;
  enviromentalComplaint = true;
  waterResourceComplaint = true;
  mineralResourceComplaint = true;

  landAccusation = true;
  emviromentalAccusation = true;
  waterResourceAccusation = true;
  mineralResourceAccusation = true;

  // Chart
  dataPieChart: any;
  pieChartOptions: any;
  dataBarChart: any;
  barChartOptions: any;

  // ẩn hiện menu trái
  visibleFilterLeff = true;
  hideColumnState = 'visible';
  expandColumnState = 'normal';

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private oAuthService: OAuthService,
    private authService: AuthService,
    private mockService: MockService
  ) {}

  ngOnInit(): void {
    this.toggleBlockUI(true);
    this.mockData = this.mockService.mockData();
    this.toggleBlockUI(false);
    this.isMapInitialised = !!this.mapContainer;
    this.loadData(true);
  }
  loadData(isFirst: boolean = false) {
    this.data = [];

    if (this.landComplaint)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Land
        )
      );

    if (this.enviromentalComplaint)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo == typesHoSo.Complaint && x.fieldType == fieldsHoSo.Emviroment
        )
      );

    if (this.waterResourceComplaint)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo == typesHoSo.Complaint && x.fieldType == fieldsHoSo.Water
        )
      );

    if (this.mineralResourceComplaint)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo == typesHoSo.Complaint && x.fieldType == fieldsHoSo.Mineral
        )
      );

    if (this.landAccusation)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo === typesHoSo.Accusation && x.fieldType === fieldsHoSo.Land
        )
      );

    if (this.emviromentalAccusation)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo == typesHoSo.Accusation && x.fieldType == fieldsHoSo.Emviroment
        )
      );

    if (this.waterResourceAccusation)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo == typesHoSo.Accusation && x.fieldType == fieldsHoSo.Water
        )
      );

    if (this.mineralResourceAccusation)
      this.data.push(
        ...this.mockData.filter(
          x => x.typeHoSo == typesHoSo.Accusation && x.fieldType == fieldsHoSo.Mineral
        )
      );

    this.buildPieChart();

    this.buildBarChart();
  }
  private buildBarChart() {
    this.dataBarChart = {
      labels: [
        'KN Đất đai',
        'KN Môi trường',
        'KN tài nguyên ngước',
        'KN Khoáng sản',
        'TC Đất đai',
        'TC Môi trường',
        'TC tài nguyên ngước',
        'TC Khoáng sản',
      ],
      datasets: [
        {
          label: 'Hồ sơ',
          backgroundColor: '#2196f3',
          data: [
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Land
            ).length,
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Emviroment
            ).length,
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Water
            ).length,
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Mineral
            ).length,

            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Accusation && x.fieldType === fieldsHoSo.Land
            ).length,
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Accusation && x.fieldType === fieldsHoSo.Emviroment
            ).length,
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Accusation && x.fieldType === fieldsHoSo.Water
            ).length,
            this.mockData.filter(
              x => x.typeHoSo === typesHoSo.Accusation && x.fieldType === fieldsHoSo.Mineral
            ).length,
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
      },
      scales: {
        x: {
          ticks: {
            color: '#495057',
          },
          grid: {
            color: '#ebedef',
          },
        },
        y: {
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

  private buildPieChart() {
    this.dataPieChart = {
      labels: ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'],
      datasets: [
        {
          data: [
            this.mockData.filter(x => x.fieldType === fieldsHoSo.Land).length,
            this.mockData.filter(x => x.fieldType === fieldsHoSo.Emviroment).length,
            this.mockData.filter(x => x.fieldType === fieldsHoSo.Water).length,
            this.mockData.filter(x => x.fieldType === fieldsHoSo.Mineral).length,
          ],
          backgroundColor: ['#2196f3', '#fccc55', '#6ebe71', '#f9ae61'],
          hoverBackgroundColor: ['#1c80cf', '#d5a326', '#419544', '#f79530'],
        },
      ],
    };
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
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
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanel = true;
    } else {
      setTimeout(() => {
        this.blockedPanel = false;
      }, 300);
    }
  }
}
