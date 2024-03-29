import { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/_shared/services/utility.service';
import { UnitService } from '@proxy/units';
import { UnitLookupDto } from '@proxy/units/models';
import { LoaiVuViec } from '@proxy';
import { MenuItem } from 'primeng/api';
import { GetSummaryListDto, SummaryDto } from '../../proxy/summaries/models';
import { SummaryService } from '@proxy/summaries';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { ComplainDetailComponent } from '../complain/detail/complain-detail.component';
import { DIALOG_BG } from 'src/app/_shared/constants/sizes.const';
import { DenounceDetailComponent } from '../denounce/detail/denounce-detail.component';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { TYPE_EXCEL } from 'src/app/_shared/constants/file-type.consts';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { TrangThaiOptions, KetquaOptions, LinhVucOptions, LoaiVuViecOptions, congKhaiOptions, loaiKQOptions } from 'src/app/_shared/constants/consts';
import { BaseMapLookupDto } from '../../proxy/base-maps/models';
import { BaseMapService } from '@proxy/base-maps';
import { trangthaiOptions} from 'src/app/_shared/constants/consts';
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
export class SearchMapComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  home: MenuItem;
  breadcrumb: MenuItem[];

  blockedPanel = false;
  dataMap: SummaryDto[] = [];
  baseMapList: BaseMapLookupDto[] = [];
  //dataMap: SummaryDto[] = [];

  //spatialData: SpatialDataDto[];

  //Paging variables
  public skipCount: number = 0;
  public maxResultCount: number = 20;
  public maxGeoCount: number = 1000;
  public totalCount: number;

  // filter
  geo = false;
  baseMap: string[] = [];
  toado: string;

  filter: GetSummaryListDto;
  //filter: GetSpatialDataListDto;
  //filter: GetComplainListDto;

  landComplain = true;
  enviromentComplain = true;
  waterComplain = true;
  mineralComplain = true;

  landDenounce = true;
  enviromentDenounce = true;
  waterDenounce = true;
  mineralDenounce = true;

  keyword: string = '';
  nguoiNopDon: string = '';
  congKhai: boolean | null;
  maTinh: number = 24;
  maHuyen: number;
  maXa: number;
  thoiGianTiepNhanRange: Date[];
  ketQua: number;
  trangThai: number;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  loaiKQOptions = loaiKQOptions;
  LoaiVuViecOptions = LoaiVuViecOptions;
  LinhVucOptions = LinhVucOptions;
  KetquaOptions = KetquaOptions;
  trangThaiOPtions = trangthaiOptions;
  TrangthaiOptions = TrangThaiOptions;
  congKhaiOptions = congKhaiOptions;
  // ẩn hiện menu trái
  visibleFilterLeff = true;
  hideColumnState = 'visible';
  expandColumnState = 'normal';

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    public layoutService: LayoutService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private oAuthService: OAuthService,
    //private spatialDataService: SpatialDataService,
    private unitService: UnitService,
    private utilService: UtilityService,
    private baseMapService: BaseMapService, 
    private summaryService: SummaryService
  ) {}

  ngOnInit(): void {
    this.buildBreadcumb();
    //this.mockData = this.mockService.mockData();
    this.loadBaseMapMenu();
    this.loadOptions();
    //this.loadGeo();
    this.loadData(true);
  }

  private buildBreadcumb() {
    this.breadcrumb = [{ label: 'Bản đồ' }];
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
  }

  loadBaseMapMenu() {//load base-map options
    this.layoutService.blockUI$.next(true);
    this.baseMapService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<BaseMapLookupDto>) => {
          this.baseMapList = res.items;
          
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
    this.layoutService.blockUI$.next(true);
  }

  loadData(isFirst: boolean = false) {
    this.getDataTable();
  }

  private getDataTable() {
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
      ketQua: this.ketQua,
      congKhai: this.hasLoggedIn ? this.congKhai : true,
      trangThai: this.trangThai,
      nguoiNopDon: this.nguoiNopDon
    } as GetSummaryListDto;
    this.summaryService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: PagedResultDto<SummaryDto>) => {
          this.dataMap = res.items;
          this.totalCount = res.totalCount;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  loadGeo() {
    if (this.geo) {
      //this.layoutService.blockUI$.next(true);

      /*Commented by Duongdx - 20/05/2023 - Load từ GeoServer thay cho DB->Không cần đoạn này nữa
      let filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxGeoCount,
        keyword: this.keyword,
      } as GetSpatialDataListDto;
      //this.spatialData
      this.spatialDataService
        .getList(filter)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<SpatialDataDto>) => {
            this.spatialData = res.items; //.map(item => item.geoJson);

            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
        */      
    }
  }

  exportExcel() {
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
      ketQua: this.ketQua,
      congKhai: this.hasLoggedIn ? this.congKhai : true,
      trangThai: this.trangThai,
      nguoiNopDon: this.nguoiNopDon,
    } as GetSummaryListDto;

    this.summaryService
      .getExcel(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            let fileName =
              this.utilService.formatDate(new Date(), 'dd/MM/yyyy HH:mm') +
              '_Khiếu nại Tố cáo.xlsx';
            const uint8Array = this.utilService.saveFile(data, TYPE_EXCEL, fileName);
          }
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  loadOptions() {
    this.layoutService.blockUI$.next(true);
    this.unitService
      .getLookup(1)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitLookupDto>) => {
          this.tinhOptions = res.items;
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
    if (this.maTinh) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(2, this.maTinh)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    }
  }

  tinhChange(event) {
    this.loadData();
    if (event.value) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(2, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.huyenOptions = [];
  }

  viewDetail(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    if (row.loaiVuViec == LoaiVuViec.KhieuNai) {
      const ref = this.dialogService.open(ComplainDetailComponent, {
        height: '92vh',
        data: {
          id: row.id,
          loaiVuViec: LoaiVuViec.KhieuNai,
          linhVuc: row.linhVuc,
          mode: 'view',
        },
        header: `Chi tiết khiếu nại/khiếu kiện "${row.tieuDe}"`,
        width: DIALOG_BG,
      });
    }
    if (row.loaiVuViec == LoaiVuViec.ToCao) {
      const ref = this.dialogService.open(DenounceDetailComponent, {
        height: '92vh',
        data: {
          id: row.id,
          loaiVuViec: LoaiVuViec.ToCao,
          linhVuc: row.linhVuc,
          mode: 'view',
        },
        header: `Chi tiết đơn tố cáo "${row.tieuDe}"`,
        width: DIALOG_BG,
      });
    }
  }

  huyenChange(event) {
    this.loadData();
    if (event.value) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(3, event.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.xaOptions = [];
  }

  thoiGiantiepNhanChange() {
    if (this.thoiGianTiepNhanRange == null || this.thoiGianTiepNhanRange[1]) {
      this.loadData();
    }
  }

  setPosition(duLieuToaDo){
    if (duLieuToaDo!=null)
      this.toado = duLieuToaDo;
  }

  changeBaseMap(i: number, data: string, e: any){
    if (e.checked){
      if (this.baseMap.indexOf(data)==-1)
        this.baseMap.push(data);
    }
    else{
      if (this.baseMap.indexOf(data)!=-1)
        this.baseMap.splice(this.baseMap.indexOf(data), 1);
    }
    this.baseMap = [...this.baseMap];
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.getDataTable();
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
