import { AuthService } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HoSo, fieldsHoSo, typesHoSo } from '../../shared/mock/HoSo';
import { MockService } from '../../shared/mock/mock.service';

@Component({
  selector: 'app-search-map',
  templateUrl: './search-map.component.html',
  styleUrls: ['./search-map.component.scss'],
  animations: [
    trigger('toggleMenu', [
      state(
        'visible',
        style({
          transform: 'translateX(0)',
        })
      ),
      state(
        'hidden',
        style({
          transform: 'translateX(-100%)',
          display: 'none',
        })
      ),
      transition('visible => hidden', animate('.3s ease-out')),
      transition('hidden => visible', animate('.3s ease-in')),
    ]),
  ],
})
export class SearchMapComponent implements OnInit {
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
  landComplaint = false;
  enviromentalComplaint = false;
  waterResourceComplaint = false;
  mineralResourceComplaint = false;

  landAccusation = false;
  emviromentalAccusation = false;
  waterResourceAccusation = false;
  mineralResourceAccusation = false;

  // ẩn hiện menu trái
  visibleFilterLeff = true;

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
  }
  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
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
