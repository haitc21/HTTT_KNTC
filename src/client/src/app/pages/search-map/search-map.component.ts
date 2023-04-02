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
import { MenuItem } from 'primeng/api';
import { GetSummaryListDto, SummaryDto } from '../../proxy/summaries/models';
import { SummaryService } from '@proxy/summaries';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { ComplainDetailComponent } from '../complain/detail/complain-detail.component';
import { DIALOG_BG } from 'src/app/shared/constants/sizes.const';
import { DenounceDetailComponent } from '../denounce/detail/denounce-detail.component';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { TYPE_EXCEL } from 'src/app/shared/constants/file-type.consts';

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
  items: SummaryDto[] = [];
  dataMap: SummaryDto[] = [];

  spatialData: SpatialDataDto[];
  complains: ComplainDto[] = [];
  denounces: DenounceDto[] = [];

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

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  loaiKQOptions = [
    { value: LoaiKetQua.Dung, text: 'Đúng' },
    { value: LoaiKetQua.Sai, text: 'Sai' },
    { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  ];
  congKhaiOptions = [
    { value: true, text: 'Công khai' },
    { value: false, text: 'Không công khai' },
  ];
  // ẩn hiện menu trái
  visibleFilterLeff = true;
  hideColumnState = 'visible';
  expandColumnState = 'normal';

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private oAuthService: OAuthService,
    private authService: AuthService,
    protected route: ActivatedRoute,
    private spatialDataService: SpatialDataService,
    private unitService: UnitService,
    private utilService: UtilityService,
    private summaryService: SummaryService
  ) {}

  ngOnInit(): void {
    this.buildBreadcumb();
    //this.mockData = this.mockService.mockData();
    this.loadOptions();
    this.loadGeo();
    this.loadData(true);
  }
  private buildBreadcumb() {
    this.breadcrumb = [{ label: 'Bản đồ' }];
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
  }

  loadData(isFirst: boolean = false) {
    this.toggleBlockUI(true);
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
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: PagedResultDto<SummaryDto>) => {
          this.items = response.items;
          this.totalCount = response.totalCount;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });

    this.toggleBlockUI(true);
    this.summaryService
      .getMap(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SummaryDto[]) => {
          this.dataMap = response;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  loadGeo() {
    if (this.geo) {
      this.toggleBlockUI(true);
      let filter = {
        skipCount: this.skipCount,
        maxResultCount: this.maxResultCount,
        keyword: this.keyword,
      } as GetSpatialDataListDto;
      //this.spatialData
      this.spatialDataService
        .getList(filter)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<SpatialDataDto>) => {
            this.spatialData = res.items; //.map(item => item.geoJson);

            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    }
  }

  exportExcel() {
    this.toggleBlockUI(true);
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
      .getExcel(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            const uint8Array = this.utilService.base64ToArrayBuffer(data);
            const blob = new Blob([uint8Array], { type: TYPE_EXCEL });

            const url = window.URL.createObjectURL(blob); // Tạo URL tạm thời

            const link = document.createElement('a');
            link.href = url;
            link.download =
              this.utilService.formatDate(new Date(), 'dd/MM/yyyy HH:mm') +
              '_Khiếu nại Tố cáo.xlsx';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);

            window.URL.revokeObjectURL(url); // Xóa URL tạm thời
          }
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }

  loadOptions() {
    this.toggleBlockUI(true);
    this.unitService
      .getLookup(1)
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

  tinhChange(event) {
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

  viewDetail(row) {
    if (!row) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    if (row.loaiVuViec == LoaiVuViec.KhieuNai) {
      const ref = this.dialogService.open(ComplainDetailComponent, {
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
  getLoaiKetQua(kq: any): string {
    if (!kq) return '';
    return this.loaiKQOptions.find(x => x.value == kq).text;
  }

  getLoaiVuViecName(loaiVuViec: LoaiVuViec) {
    switch (loaiVuViec) {
      case LoaiVuViec.KhieuNai:
        return 'Khiếu nại/Khiếu kiện';
        break;
      case LoaiVuViec.ToCao:
        return 'Tố cáo';
        break;
      default:
        return '';
    }
  }
  getLinhVucName(linhVuc: LinhVuc) {
    switch (linhVuc) {
      case LinhVuc.DatDai:
        return 'Đất đai';
        break;
      case LinhVuc.MoiTruong:
        return 'Môi trường';
        break;
      case LinhVuc.TaiNguyenNuoc:
        return 'Tài nguyên nước';
        break;
      case LinhVuc.KhoangSan:
        return 'Khoáng sản';
        break;
      default:
        return '';
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
