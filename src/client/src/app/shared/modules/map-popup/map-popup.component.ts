import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoaiKetQua, LoaiVuViec } from '@proxy';
import { SummaryDto, SummaryMapDto } from '@proxy/summaries';
import { UnitService } from '@proxy/units';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from '../../services/notification.service';
import { MessageConstants } from '../../constants/messages.const';
import { ComplainDetailComponent } from 'src/app/pages/complain/detail/complain-detail.component';
import { DIALOG_BG } from '../../constants/sizes.const';
import { DenounceDetailComponent } from 'src/app/pages/denounce/detail/denounce-detail.component';
import { UtilityService } from '../../services/utility.service';
import { Subject, takeUntil } from 'rxjs';
import { ComplainDto, ComplainService } from '@proxy/complains';
import { DenounceDto, DenounceService } from '@proxy/denounces';

@Component({
  selector: 'app-map-popup',
  templateUrl: './map-popup.component.html',
  styleUrls: ['./map-popup.component.scss'],
})
export class MapPopupComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  public blockedPanelDetail: boolean = false;

  @Input() dataMap!: SummaryMapDto;
  public form: FormGroup;
  hoSo!: SummaryDto;

  loaiKQOPtions = [
    { value: LoaiKetQua.Dung, text: 'Đúng' },
    { value: LoaiKetQua.Sai, text: 'Sai' },
    { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  ];
  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private utilService: UtilityService,
    private complainService: ComplainService,
    private denounceService: DenounceService
  ) {}

  ngOnInit() {
    if (this.dataMap.loaiVuViec == LoaiVuViec.KhieuNai) this.loadComplain();
    else this.loadDenounce();
  }

  loadComplain() {
    this.toggleBlockUI(true);
    this.complainService
      .get(this.dataMap.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        res => {
          this.mapData(res, LoaiVuViec.KhieuNai);
          this.buildForm();
          this.toggleBlockUI(false);
        },
        err => {
          this.toggleBlockUI(false);
        }
      );
  }

  loadDenounce() {
    this.toggleBlockUI(true);
    this.denounceService
      .get(this.dataMap.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        res => {
          this.mapData(res, LoaiVuViec.ToCao);
          this.buildForm();
          this.toggleBlockUI(false);
        },
        err => {
          this.toggleBlockUI(false);
        }
      );
  }

  mapData(res: ComplainDto | DenounceDto, loaiVuViec: LoaiVuViec) {
    let item = {
      id: this.dataMap.id,
      maHoSo: res.maHoSo,
      nguoiNopDon: res.nguoiNopDon,
      dienThoai: res.dienThoai,
      diaChiLienHe: res.diaChiLienHe,
      loaiVuViec: loaiVuViec,
      tieuDe: res.tieuDe,
      thoiGianTiepNhan: res.thoiGianTiepNhan,
      thoiGianHenTraKQ: res.thoiGianHenTraKQ,
      boPhanDangXL: res.boPhanDangXL,
      ketQua: res.ketQua,
      duLieuToaDo: res.duLieuToaDo,
      duLieuHinhHoc: res.duLieuHinhHoc,
    } as SummaryDto;
    this.hoSo = item;
  }

  buildForm() {
    this.form = this.fb.group({
      maHoSo: [this.hoSo.maHoSo],
      tieuDe: [this.hoSo.tieuDe],
      nguoiNopDon: [this.hoSo.nguoiNopDon],
      dienThoai: [this.hoSo.dienThoai],
      thoiGianTiepNhan: [this.utilService.convertDateToLocal(this.hoSo.thoiGianTiepNhan)],
      thoiGianHenTraKQ: [this.utilService.convertDateToLocal(this.hoSo.thoiGianHenTraKQ)],
      boPhanDangXL: [this.hoSo.boPhanDangXL],
      ketQua: [this.hoSo.ketQua],
    });
    this.form.disable();
  }
  viewDetail() {
    if (!this.hoSo) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    if (this.dataMap.loaiVuViec == LoaiVuViec.KhieuNai) {
      const ref = this.dialogService.open(ComplainDetailComponent, {
        height: '92vh',
        data: {
          id: this.hoSo.id,
          loaiVuViec: LoaiVuViec.KhieuNai,
          linhVuc: this.hoSo.linhVuc,
          mode: 'view',
        },
        header: `Chi tiết khiếu nại/khiếu kiện "${this.hoSo.tieuDe}"`,
        width: DIALOG_BG,
      });
    }
    if (this.dataMap.loaiVuViec == LoaiVuViec.ToCao) {
      const ref = this.dialogService.open(DenounceDetailComponent, {
        height: '92vh',
        data: {
          id: this.hoSo.id,
          loaiVuViec: LoaiVuViec.ToCao,
          linhVuc: this.hoSo.linhVuc,
          mode: 'view',
        },
        header: `Chi tiết đơn tố cáo "${this.hoSo.tieuDe}"`,
        width: DIALOG_BG,
      });
    }
  }
  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.blockedPanelDetail = false;
      }, 300);
    }
  }
  ngOnDestroy(): void {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
