import { AuthService, ListResultDto, PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
//import { Complain, fieldsHoSo, typesHoSo } from '../../shared/mock/Complain';
//import { MockService } from '../../shared/mock/mock.service';
import { SpatialDataDto, SpatialDataService, GetSpatialDataListDto } from '@proxy/spatial-datas';
import { ComplainDto, ComplainService, GetComplainListDto } from '@proxy/complains';
import { DenounceDto, DenounceService, GetDenounceListDto } from '@proxy/denounces';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
//import { each } from 'chart.js/dist/helpers/helpers.core';
import { UnitService } from '@proxy/units';
import { UnitLookupDto } from '@proxy/units/models';
import { ActivatedRoute } from '@angular/router';
import { LinhVuc, LoaiKetQua, LoaiVuViec, SpatialDatas } from '@proxy';
import {MenuItem} from 'primeng/api';

@Component({
  selector: 'app-search-map',
  templateUrl: './search-map.component.html',
  styleUrls: ['./search-map.component.scss'],
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

export class SearchMapComponent implements OnInit {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  home: MenuItem;
  breadcrumb: MenuItem[];

  blockedPanel = false;
  data: any[] = [];

  spatialData: SpatialDataDto[];
  complains: ComplainDto[] = []
  denounces: DenounceDto[] = []
  //mockData: Complain[] = [];

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  //loaiHS = ['khiếu nại', 'Tố cáo'];
  //linhVuc = ['Đất đai', 'Môi trường', 'Tài nguyên nước', 'Khoáng sản'];
  linhVuc: LinhVuc;
  //header: string = '';
  
  //typesHoSo = typesHoSo;
  //fieldsHoSo = fieldsHoSo;

  //public selectedItems: Complain[] = [];
  //items: ComplainDto[];
  selectedItems: ComplainDto[] = [];
  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 10;
  public totalCount: number;

  // filter
  geo = false;
  //filter: GetSpatialDataListDto;
  //filter: GetComplainListDto;

  landComplain = true;
  enviromentalComplain = true;
  waterComplain = true;
  mineralComplain = true;

  landDenounce = true;
  enviromentDenounce = true;
  waterDenounce = true;
  mineralDenounce = true;

  keyword = '';
  loaiVuviec: number;
  lanKNSearch: Number;
  stageSearch: Number;
  maTinh: number;
  maHuyen: number;
  maXa: number;
  thoiGianTiepNhanRange: Date[];
  giaiDoan: number;
  tinhTrang: number;

  loaiVuviecOptions = [
    { value: 0, text: 'Tất cả' },
    { value: LoaiVuViec.KhieuNai, text: 'Khiếu nại' },
    { value: LoaiVuViec.ToCao, text: 'Tố cáo' },
  ];

  linhVucOptions = [
    { value: 0, text: '' },
    { value: LinhVuc.DatDai, text: 'Đất đai' },
    { value: LinhVuc.KhoangSan, text: 'Khoáng sản' },
    { value: LinhVuc.MoiTruong, text: 'Môi trường' },
    { value: LinhVuc.TaiNguyenNuoc, text: 'Tài nguyên nước' },
  ];

  giaiDoanOptions = [
    { value: 0, text: '' },
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];

  loaiKQOptions = [
    { value: 0, text: '' },
    { value: LoaiKetQua.Dung, text: 'Đúng' },
    { value: LoaiKetQua.Sai, text: 'Sai' },
    { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  ];
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
    
    protected route: ActivatedRoute,
    private spatialDataService: SpatialDataService,
    private complainService: ComplainService,
    private denounceService: DenounceService,
    private utilService: UtilityService,
    private unitService: UnitService
  ) {}

  ngOnInit(): void {
    this.toggleBlockUI(true);
    this.breadcrumb = [
      {label:'Bản đồ'}
    ];
    this.home = {label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/'};
    //this.mockData = this.mockService.mockData();
    this.loadOptions();
    this.loadData(true);
    
    this.route.paramMap.subscribe(params => {
      //this.linhVuc = +params.get('linhVuc') as LinhVuc;
      //this.setHeader();
      this.resetFilter();
      this.loadData(true);
    });
    this.toggleBlockUI(false);  
  }

  /*
  private setHeader() {
    switch (this.linhVuc) {
      case LinhVuc.DataDai:
        this.header = 'Khiếu nại đất đai';
        break;
      case LinhVuc.MoiTruong:
        this.header = 'Khiếu nại môi trường';
        break;
      case LinhVuc.TaiNguyenNuoc:
        this.header = 'Khiếu nại tài nguyên nước';
        break;
      case LinhVuc.KhoangSan:
        this.header = 'Khiếu nại khoáng sản';
        break;
      default:
        this.header = '';
    }
  }
  */

  private resetFilter() {
    this.skipCount = 0;
    this.maxResultCount = 10;
    this.totalCount = 0;
    this.keyword = '';
    this.maTinh = null;
    this.maHuyen = null;
    this.maXa = null;
    this.thoiGianTiepNhanRange = [];
    this.giaiDoan = null;
    this.tinhTrang = null;
    this.loaiVuviec = null;
  }

  
  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);

    //spatialData
    debugger
    if (this.geo){
      
      let filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        keyword: this.keyword,
      } as GetSpatialDataListDto;
       //this.spatialData
      this.spatialDataService.getList(filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<SpatialDataDto>) => {

          this.spatialData = res.items;//.map(item => item.geoJson);
          
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
    }

    /*
    if (isFirst) {      
      this.filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        linhVuc: this.linhVuc
      } as GetComplainListDto;
    } else {
    */
   //Khiếu nại
    if (this.loaiVuviec==LoaiVuViec.KhieuNai){
      let filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        keyword: this.keyword,
        
        maTinhTP: this.maTinh,
        maQuanHuyen: this.maHuyen,
        maXaPhuongTT: this.maXa,
        fromDate: this.thoiGianTiepNhanRange[0]
          ? this.thoiGianTiepNhanRange[0].toUTCString()
          : null,
        toDate:
          this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
            ? this.thoiGianTiepNhanRange[1].toUTCString()
            : null,
        linhVuc: this.linhVuc,
        ketQua: this.tinhTrang,
        giaiDoan: this.giaiDoan,
      } as GetComplainListDto;
    //}

      this.complainService
        .getList(filter)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: (response: PagedResultDto<ComplainDto>) => {
            this.data = response.items;
            this.data.forEach(x => x.type = 1);
            this.totalCount = response.totalCount;
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
      }
    else if (this.loaiVuviec==LoaiVuViec.ToCao){
      //Tố cáo
      let filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        keyword: this.keyword,
        
        maTinhTP: this.maTinh,
        maQuanHuyen: this.maHuyen,
        maXaPhuongTT: this.maXa,
        fromDate: this.thoiGianTiepNhanRange[0]
          ? this.thoiGianTiepNhanRange[0].toUTCString()
          : null,
        toDate:
          this.thoiGianTiepNhanRange && this.thoiGianTiepNhanRange[1]
            ? this.thoiGianTiepNhanRange[1].toUTCString()
            : null,
        linhVuc: this.linhVuc,
        ketQua: this.tinhTrang,
        giaiDoan: this.giaiDoan,
      } as GetDenounceListDto;
    //}

      this.denounceService
        .getList(filter)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: (response: PagedResultDto<DenounceDto>) => {
            this.data = response.items;
            this.data.forEach(x => x.type = 2);
            this.totalCount = response.totalCount;
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    }
    else{//Ca 2 loai luon :D

    }
    this.toggleBlockUI(false);
  }

  loadOptions() {
    this.toggleBlockUI(true);

    this.unitService
      .getLookup(1, 12) //Mã tỉnh Thái Nguyên
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitLookupDto>) => {
          this.tinhOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }

  TinhChange(event) {
    this.loadData();
    if (event.value) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(2, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenOptions = res.items;
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.huyenOptions = [];
  }

  huyenChange(event) {
    this.loadData();
    if (event.value) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(3, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaOptions = res.items;
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.xaOptions = [];
  }

  thoiGiantiepNhanChange() {
    if (this.thoiGianTiepNhanRange == null || this.thoiGianTiepNhanRange[1]) {
      this.loadData();
    }
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  /*
  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);
    this.data = [];
    
    this.filter = {
      filter: this.keyword,
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      email: this.emailSearch,
      phoneNumber: this.phoneNumberSearch,
      roleId: this.roleIdSearch,
    };

    /*
    if(this.keyword || this.lanKNSearch || this.stageSearch || 
      this.landComplain || this.enviromentalComplain || this.waterComplain || this.mineralComplain ||
      this.landDenounce || this.enviromentDenounce || this.waterDenounce || this.mineralDenounce){
      //search 
      if (this.landComplain)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo === typesHoSo.Complaint && x.fieldType === fieldsHoSo.Land
          )
        );

      if (this.enviromentalComplain)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo == typesHoSo.Complaint && x.fieldType == fieldsHoSo.Emviroment
          )
        );

      if (this.waterComplain)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo == typesHoSo.Complaint && x.fieldType == fieldsHoSo.Water
          )
        );

      if (this.mineralComplain)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo == typesHoSo.Complaint && x.fieldType == fieldsHoSo.Mineral
          )
        );

      if (this.landDenounce)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo === typesHoSo.Accusation && x.fieldType === fieldsHoSo.Land
          )
        );

      if (this.enviromentDenounce)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo == typesHoSo.Accusation && x.fieldType == fieldsHoSo.Emviroment
          )
        );

      if (this.waterDenounce)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo == typesHoSo.Accusation && x.fieldType == fieldsHoSo.Water
          )
        );

      if (this.mineralDenounce)
        this.data.push(
          ...this.mockData.filter(
            x => x.typeHoSo == typesHoSo.Accusation && x.fieldType == fieldsHoSo.Mineral
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
    }
    /

    this.toggleBlockUI(false);
  }
  */
 
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

  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
