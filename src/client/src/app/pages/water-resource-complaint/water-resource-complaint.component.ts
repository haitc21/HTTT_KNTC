import { AuthService } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Complain, fieldsHoSo, typesHoSo } from '../../shared/mock/Complain';
import { MockService } from '../../shared/mock/mock.service';

@Component({
  selector: 'app-water-resource-complaint',
  templateUrl: './water-resource-complaint.component.html',
  styleUrls: ['./water-resource-complaint.component.scss'],
})
export class WaterResourceComplaintComponent implements OnInit {
  blockedPanel = false;
  data: Complain[] = [];

  mockData: Complain[] = [];
  loaiHS = ['khiếu nại', 'Tố cáo'];
  linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];
  typesHoSo = typesHoSo;
  fieldsHoSo = fieldsHoSo;

  public selectedItems: Complain[] = [];
  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  keyword = '';
  lanKNSearch: Number;
  stageSearch: Number;
  giaiDoanOptions = [
    { value: 0, text: 'Khiếu nại lần I' },
    { value: 1, text: 'Khiếu nại lần II' },
  ];
  stageOptions = [
    { value: 0, text: 'Đúng' },
    { value: 1, text: 'Sai' },
  ];

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
    this.toggleBlockUI(true);
    this.data = [];

    this.data.push(
      ...this.mockData.filter(
        x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Water
      )
    );

    if (this.keyword) {
      this.data = this.data.filter(
        x => x.code.includes(this.keyword) || x.title.includes(this.keyword)
      );
    }
    switch (this.lanKNSearch) {
      case 0:
        this.data = this.data.filter(x => !x.returnDate2);
        break;
      case 1:
        this.data = this.data.filter(x => x.returnDate2);
        break;
      default:
        this.data = this.data;
    }
    switch (this.stageSearch) {
      case 0:
        this.data = this.data.filter(x => x.result1 || x.result2);
        break;
      case 1:
        this.data = this.data.filter(x => x.result2 === false);
        break;
      default:
        this.data = this.data;
    }

    this.toggleBlockUI(false);
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
