import { ListResultDto } from '@abp/ng.core';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { PagedResultDto } from '@abp/ng.core';
import { MenuItem } from 'primeng/api';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { Subject, takeUntil } from 'rxjs';
import { GetSummaryListDto, SummaryDto } from '../../proxy/summaries/models';
import { UnitService } from '@proxy/units';
import { UnitLookupDto } from '@proxy/units/models';
import { LinhVuc, LoaiVuViec } from '@proxy';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { DIALOG_BG } from 'src/app/shared/constants/sizes.const';
import { DialogService } from 'primeng/dynamicdialog';
import { ActivatedRoute } from '@angular/router';
import { TYPE_EXCEL } from 'src/app/shared/constants/file-type.consts';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { KetquaOptions, LinhVucOptions, LoaiVuViecOptions, congKhaiOptions, giaiDoanOptions, linhVucNameOptions, loaiKQOptions, loaiVuViecNameOptions } from 'src/app/shared/constants/consts';
import { ComplainDetailComponent } from '../complain/detail/complain-detail.component';
import { OAuthService } from 'angular-oauth2-oidc';
import { SummaryService } from '@proxy/summaries';
import { DenounceDetailComponent } from '../denounce/detail/denounce-detail.component';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss'],
})
export class ReportComponent implements OnInit, OnDestroy {
  //System variables
  private ngUnsubscribe = new Subject<void>();
  home: MenuItem;
  breadcrumb: MenuItem[];
  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  header: string = '';
  items: SummaryDto[] = [];
  actionItem: SummaryDto;

  // default
  blockedPanel = false;
  skipCount: number = 0;
  maxResultCount: number = 20;
  totalCount: number;

  // filter
  filter: GetSummaryListDto;
  keyword: string = '';
  nguoiNopDon = '';

  maTinh: number;
  maHuyen: number;
  maXa: number;
  thoiGianTiepNhanRange: Date[];
  giaiDoan: number;
  tinhTrang: number;
  congKhai: boolean | null;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];

  loaiVuviecOptions = loaiVuViecNameOptions;
  linhvucOptions = linhVucNameOptions;
  LoaiVuViecOptions = LoaiVuViecOptions;
  LinhVucOptions = LinhVucOptions;  
  giaiDoanOptions = giaiDoanOptions;
  loaiKQOptions = loaiKQOptions;
  congKhaiOptions = congKhaiOptions;
  KetquaOptions = KetquaOptions;

  get hasLoggedIn(): boolean {
    return this.oAuthService.hasValidAccessToken();
  }

  constructor(
    public layoutService: LayoutService,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private oAuthService: OAuthService,
    private summaryService: SummaryService,
    private utilService: UtilityService,
    private unitService: UnitService,
    protected route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.home = { label: ' Trang chủ', icon: 'pi pi-home', routerLink: '/' };
    this.loadOptions();
    this.initData(null);
  }

  initData(loaivuviec:any){
    if (loaivuviec==null) this.loaiVuViec = LoaiVuViec.TatCa;
    else this.loaiVuViec = loaivuviec.value;
    this.resetFilter();
    this.buildBreadcrumb();
    this.loadData(true);
  }

  private resetFilter() {
    this.skipCount = 0;
    this.maxResultCount = 20;
    this.totalCount = 0;
    this.keyword = '';
    //this.loaiVuViec = LoaiVuViec.TatCa;
    this.linhVuc = LinhVuc.TatCa;
    this.nguoiNopDon = '';
    this.maTinh = null;
    this.maHuyen = null;
    this.maXa = null;
    this.thoiGianTiepNhanRange = null;
    this.giaiDoan = null;
    this.tinhTrang = null;
  }

  buildBreadcrumb() {
    this.breadcrumb = [{ label: ' Thống kê', icon: 'pi pi-chart-bar', disabled: true }];
    switch (this.loaiVuViec) {
      case LoaiVuViec.KhieuNai:{
        this.breadcrumb.push({
          label: ' Báo cáo Khiếu nại',
          icon: 'pi pi-envelope',
          disabled: true,
        });
        break;
      }
      case LoaiVuViec.ToCao:{
        this.breadcrumb.push({
          label: ' Báo cáo Tố cáo',
          icon: 'fa fa-balance-scale',
          disabled: true,
        });
        break;
      }   
      default:{
        this.breadcrumb.push({
          label: ' Sổ theo dõi Khiếu nại/Tố cáo',
          icon: 'pi pi-send',
          disabled: true,
        });
        break;
      }
    }
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

  loadData(isFirst: boolean = false) {
    this.layoutService.blockUI$.next(true);

    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword,
      loaiVuViec: this.loaiVuViec,
      linhVuc: this.linhVuc,
      // landComplain: this.linhVuc == LinhVuc.DatDai,
      // enviromentComplain: this.linhVuc == LinhVuc.MoiTruong,
      // mineralComplain: this.linhVuc == LinhVuc.KhoangSan,
      // waterComplain: this.linhVuc == LinhVuc.TaiNguyenNuoc,
      
      // landDenounce: this.linhVuc == LinhVuc.DatDai,
      // enviromentDenounce: this.linhVuc == LinhVuc.MoiTruong,
      // mineralDenounce: this.linhVuc == LinhVuc.KhoangSan,
      // waterDenounce: this.linhVuc == LinhVuc.TaiNguyenNuoc,

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
      nguoiNopDon: this.nguoiNopDon
    } as GetSummaryListDto;

    this.summaryService
      .getList(this.filter)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: PagedResultDto<SummaryDto>) => {
          this.items = res.items;
          this.totalCount = res.totalCount;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  exportExcel() {
    this.layoutService.blockUI$.next(true);
    this.filter = {
      skipCount: this.skipCount,
      maxResultCount: this.maxResultCount,
      keyword: this.keyword,
      loaiVuViec: this.loaiVuViec,
      linhVuc: this.linhVuc,

      // landComplain: this.linhVuc == LinhVuc.DatDai,
      // enviromentComplain: this.linhVuc == LinhVuc.MoiTruong,
      // mineralComplain: this.linhVuc == LinhVuc.KhoangSan,
      // waterComplain: this.linhVuc == LinhVuc.TaiNguyenNuoc,
      
      // landDenounce: this.linhVuc == LinhVuc.DatDai,
      // enviromentDenounce: this.linhVuc == LinhVuc.MoiTruong,
      // mineralDenounce: this.linhVuc == LinhVuc.KhoangSan,
      // waterDenounce: this.linhVuc == LinhVuc.TaiNguyenNuoc,

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
      nguoiNopDon: this.nguoiNopDon,
    } as GetSummaryListDto;

    this.summaryService
      .getLogBookExcel(this.filter)//GetLogBookExcel
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (data: any) => {
          if (data) {
            let fileName =
              this.utilService.formatDate(new Date(), 'dd/MM/yyyy HH:mm') +
              '_Theo dõi Đơn thư Khiếu nại - Tố cáo.xlsx';
            const uint8Array = this.utilService.saveFile(data, TYPE_EXCEL, fileName);
          }
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  pageChanged(event: any): void {
    this.skipCount = event.page * this.maxResultCount;
    this.maxResultCount = event.rows;
    this.loadData();
  }

  setActionItem(item) {
    this.actionItem = item;
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

  getLoaiKetQua(kq: any): string {
    if (!kq) return 'Chưa có KQ';
    return this.loaiKQOptions.find(x => x.value == kq).text;
  }

  thoiGiantiepNhanChange() {
    if (this.thoiGianTiepNhanRange == null || this.thoiGianTiepNhanRange[1]) {
      this.loadData();
    }
  }
  
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
