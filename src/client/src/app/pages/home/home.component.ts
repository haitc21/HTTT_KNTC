import { AuthService } from '@abp/ng.core';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HoSo, fieldsHoSo, typesHoSo } from '../../shared/mock/HoSo';
import { MockService } from '../../shared/mock/mock.service';
import { animate, state, style, transition, trigger } from '@angular/animations';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
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
export class HomeComponent implements OnInit {
  blockedPanel = false;
  @ViewChild('map', { static: true }) mapContainer: ElementRef;

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
