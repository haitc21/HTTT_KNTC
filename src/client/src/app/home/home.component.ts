import { AuthService } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HoSo } from '../shared/mock/HoSo';
import { MockService } from '../shared/mock/mock.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  blockedPanel = false;
  data: HoSo[] = [];
  mockData: HoSo[] = [];
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

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private oAuthService: OAuthService,
    private authService: AuthService,
    private mockService: MockService
  ) {}

  ngOnInit(): void {
    this.mockData = this.mockService.mockData();
    this.loadData(true);
  }
  loadData(isFirst: boolean = false): void {
    this.data = this.mockData;
    // this.data = this.data.filter(x => x.typeHoSo != "Khiếu nại");
    // console.log(this.mockData);
    // console.log(this.data);
    

    // if (this.landComplaint)
    //   this.data = this.data.filter(x => x.type === 'Khiếu nại' && x.fieldType === 'Đất đai');
    // else this.data = this.data.filter(x => x.type !== 'Khiếu nại' && x.fieldType !== 'Đất đai');

    // if (this.enviromentalComplaint)
    //   this.data = this.data.filter(x => x.type == 'Khiếu nại' && x.fieldType == 'Môi trường');
    // else this.data = this.data.filter(x => x.type != 'Khiếu nại' && x.fieldType != 'Môi trường');

    // if (this.waterResourceComplaint)
    //   this.data = this.data.filter(x => x.type == 'Khiếu nại' && x.fieldType == 'Tài nguyên nước');
    // else
    //   this.data = this.data.filter(x => x.type != 'Khiếu nại' && x.fieldType != 'Tài nguyên nước');

    // if (this.mineralResourceComplaint)
    //   this.data = this.data.filter(x => x.type == 'Khiếu nại' && x.fieldType == 'Khoáng sản');
    // else this.data = this.data.filter(x => x.type != 'Khiếu nại' && x.fieldType != 'Khoáng sản');
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
