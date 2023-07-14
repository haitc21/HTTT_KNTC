import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoaiKetQua, LoaiVuViec } from '@proxy';
import { SummaryDto } from '@proxy/summaries';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from '../../../services/notification.service';
import { MessageConstants } from '../../../constants/messages.const';
import { ComplainDetailComponent } from 'src/app/pages/complain/detail/complain-detail.component';
import { DIALOG_BG } from '../../../constants/sizes.const';
import { DenounceDetailComponent } from 'src/app/pages/denounce/detail/denounce-detail.component';
import { UtilityService } from '../../../services/utility.service';
import { Subject, takeUntil } from 'rxjs';
import { ComplainDto, ComplainService } from '@proxy/complains';
import { DenounceDto, DenounceService } from '@proxy/denounces';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { loaiKQOptions } from '../../../constants/consts';

@Component({
  selector: 'app-map-popup',
  templateUrl: './map-popup.component.html',
  styleUrls: ['./map-popup.component.scss'],
})
export class MapPopupComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  public blockedPanelDetail: boolean = false;

  @Input() dataMap!: SummaryDto;
  public form: FormGroup;
  loaiKQOPtions = loaiKQOptions;

  constructor(
    private fb: FormBuilder,
    private dialogService: DialogService,
    private notificationService: NotificationService,
    private utilService: UtilityService,
    private complainService: ComplainService,
    private denounceService: DenounceService,
    private layoutService: LayoutService
  ) {}

  ngOnInit() {
    this.buildForm();
    if (this.dataMap.loaiVuViec == LoaiVuViec.KhieuNai) this.loadComplain();
    else this.loadDenounce();
  }

  loadComplain() {
    this.toggleBlockUI(true);
    this.complainService
      .get(this.dataMap.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ComplainDto) => {
          this.setValueForm(res);
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
        (res: DenounceDto) => {
          this.setValueForm(res);
          this.toggleBlockUI(false);
        },
        err => {
          this.toggleBlockUI(false);
        }
      );
  }
  setValueForm(hoSo: ComplainDto | DenounceDto) {
    this.form.patchValue(hoSo);
    this.form
      .get('thoiGianTiepNhan')
      .setValue(this.utilService.convertDateToLocal(hoSo.thoiGianTiepNhan));
    this.form
      .get('thoiGianHenTraKQ')
      .setValue(this.utilService.convertDateToLocal(hoSo.thoiGianHenTraKQ));
    this.form.disable();
  }
  buildForm() {
    this.form = this.fb.group({
      id: [null],
      maHoSo: [null],
      tieuDe: [null],
      nguoiNopDon: [null],
      dienThoai: [null],
      thoiGianTiepNhan: [null],
      thoiGianHenTraKQ: [null],
      soThua: [null],
      toBanDo: [null],
      boPhanDangXL: [null],
      ketQua: [null],
    });
  }

  viewDetail() {
    if (!this.form.value?.id) {
      this.notificationService.showError(MessageConstants.NOT_CHOOSE_ANY_RECORD);
      return;
    }
    if (this.dataMap.loaiVuViec == LoaiVuViec.KhieuNai) {
      const ref = this.dialogService.open(ComplainDetailComponent, {
        height: '92vh',
        data: {
          id: this.form.value?.id,
          loaiVuViec: LoaiVuViec.KhieuNai,
          linhVuc: this.form.value?.linhVuc,
          mode: 'view',
        },
        header: `Chi tiết khiếu nại/khiếu kiện "${this.form.value?.tieuDe}"`,
        width: DIALOG_BG,
      });
    }
    if (this.dataMap.loaiVuViec == LoaiVuViec.ToCao) {
      const ref = this.dialogService.open(DenounceDetailComponent, {
        height: '92vh',
        data: {
          id: this.form.value?.id,
          loaiVuViec: LoaiVuViec.ToCao,
          linhVuc: this.form.value?.linhVuc,
          mode: 'view',
        },
        header: `Chi tiết đơn tố cáo "${this.form.value?.tieuDe}"`,
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
