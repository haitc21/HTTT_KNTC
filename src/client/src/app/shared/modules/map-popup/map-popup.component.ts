import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { LoaiKetQua, LoaiVuViec } from '@proxy';
import { SummaryDto } from '@proxy/summaries';
import { UnitService } from '@proxy/units';
import { DialogService } from 'primeng/dynamicdialog';
import { NotificationService } from '../../services/notification.service';
import { MessageConstants } from '../../constants/messages.const';
import { ComplainDetailComponent } from 'src/app/pages/complain/detail/complain-detail.component';
import { DIALOG_BG } from '../../constants/sizes.const';
import { DenounceDetailComponent } from 'src/app/pages/denounce/detail/denounce-detail.component';
import { UtilityService } from '../../services/utility.service';

@Component({
  selector: 'app-map-popup',
  templateUrl: './map-popup.component.html',
  styleUrls: ['./map-popup.component.scss'],
})
export class MapPopupComponent implements OnInit {
  @Input() hoSo: SummaryDto;
  public form: FormGroup;

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
  ) {}

  ngOnInit() {
    this.buildForm();
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
    if (this.hoSo.loaiVuViec == LoaiVuViec.KhieuNai) {
      const ref = this.dialogService.open(ComplainDetailComponent, {
        height: '80vh',
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
    if (this.hoSo.loaiVuViec == LoaiVuViec.ToCao) {
      const ref = this.dialogService.open(DenounceDetailComponent, {
        height: '80vh',
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
}
