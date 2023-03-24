import { ListResultDto } from '@abp/ng.core';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { LinhVuc, LoaiKetQua, LoaiKhieuNai, LoaiVuViec } from '@proxy';
import {
  DenounceDto,
  DenounceService,
  CreateDenounceDto,
  UpdateDenounceDto,
} from '@proxy/denounces';
import { CreateAndUpdateFileAttachmentDto } from '@proxy/file-attachments';
import { LandTypeLookupDto, LandTypeService } from '@proxy/land-types';
import { UnitLookupDto, UnitService } from '@proxy/units';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { EileUploadDto } from 'src/app/shared/models/file-upload.class';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { FileAttachmentComponent } from '../../file-attachment/file-attachment.component';

@Component({
  templateUrl: './denounce-detail.component.html',
  styleUrls: ['./denounce-detail.component.scss'],
})
export class DenounceDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  @ViewChild(FileAttachmentComponent)
  fileAttachmentComponent: FileAttachmentComponent;

  denounceId: string;
  mode: 'create' | 'update' = 'create';
  loaiVuViec = LoaiVuViec.KhieuNai;
  fileUploads: EileUploadDto[] = [];
  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  selectedEntity: DenounceDto;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];
  huyenThuaDateOptions: UnitLookupDto[] = [];
  xaThuaDatOptions: UnitLookupDto[] = [];
  landTypeOptions: LandTypeLookupDto[];
  loaiKQOPtions = [
    { value: LoaiKetQua.Dung, text: 'Đúng' },
    { value: LoaiKetQua.Sai, text: 'Sai' },
    { value: LoaiKetQua.CoDungCoSai, text: 'Có Đúng/Có Sai' },
  ];

  // Validate
  validationMessages = {
    maHoSo: [
      { type: 'required', message: 'Mã hồ sơ không được để trống' },
      {
        type: 'maxLength',
        message: `Mã hồ sơ không vượt quá ${KNTCValidatorConsts.MaxMaHoSoLength} kí tự`,
      },
    ],
    tieuDe: [
      { type: 'required', message: 'Tiêu đề không được để trống' },
      {
        type: 'maxLength',
        message: `Tiêu đề không vượt quá ${KNTCValidatorConsts.MaxTieuDeLength} kí tự`,
      },
    ],
    nguoiToCao: [
      { type: 'required', message: 'Người tố cáo không được để trống' },
      {
        type: 'maxLength',
        message: `Người tố cáo không vượt quá ${KNTCValidatorConsts.MaxTenNguoiLength} kí tự`,
      },
    ],
    cccdCmnd: [
      { type: 'required', message: 'CCCD/CMND không được để trống' },
      {
        type: 'maxLength',
        message: `CCCD/CMND không vượt quá ${KNTCValidatorConsts.MaxCccdCmndLength} kí tự`,
      },
    ],
    // ngayCapCccdCmnd: [{ type: 'required', message: 'Ngày cấp CCCD/CMND không được để trống' }],
    // noiCapCccdCmnd: [
    //   { type: 'required', message: 'Nơi cấp CCCD/CMND không được để trống' },
    //   {
    //     type: 'maxLength',
    //     message: `Nơi cấp CCCD/CMND không vượt quá ${KNTCValidatorConsts.MaxNoiCapCccdCmnd} kí tự`,
    //   },
    // ],
    ngaySinh: [{ type: 'required', message: 'Ngày sinh không được để trống' }],
    dienThoai: [
      { type: 'required', message: 'Số điện thoại không được để trống' },
      { type: 'pattern', message: 'Số điện thoại không chính xác' },
    ],
    email: [{ type: 'email', message: 'Địa chỉ email không chính xác' }],
    diaChiThuongTru: [
      { type: 'required', message: 'Địa chỉ thường trú không được để trống' },
      {
        type: 'maxLength',
        message: `Địa chỉ thường trú không vượt quá ${KNTCValidatorConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    diaChiLienHe: [
      { type: 'required', message: 'Địa chỉ liên hệ không được để trống' },
      {
        type: 'maxLength',
        message: `Địa chỉ liên hệ không vượt quá ${KNTCValidatorConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    maTinhTP: [{ type: 'required', message: 'Tỉnh/TP không được để trống' }],
    maQuanHuyen: [{ type: 'required', message: 'Quận/huyện không được để trống' }],
    maXaPhuongTT: [{ type: 'required', message: 'Xã/phường/TT không được để trống' }],
    thoiGianTiepNhan: [{ type: 'required', message: 'Mgày tiếp nhận không được để trống' }],
    thoiGianHenTraKQ: [{ type: 'required', message: 'Ngày hẹn trả kết quả không được để trống' }],
    noiDungVuViec: [{ type: 'required', message: 'Nội dung vụ việc không được để trống' }],
    nguoiBiToCao: [
      { type: 'required', message: 'Người bị tố cáo không được để trống' },
      {
        type: 'maxLength',
        message: `Người bị tố cáo không vượt quá ${KNTCValidatorConsts.MaxTenNguoiLength} kí tự`,
      },
    ],
    boPhanDangXL: [
      { type: 'required', message: 'Bộ phận đang XL không được để trống' },
      {
        type: 'maxLength',
        message: `Bộ phận đang XL không vượt quá ${KNTCValidatorConsts.MaxBoPhanXLLength} kí tự`,
      },
    ],
    soThua: [
      { type: 'required', message: 'Số thửa không được để trống' },
      {
        type: 'maxLength',
        message: `Số thửa không vượt quá ${KNTCValidatorConsts.MaxSoThuaLength} kí tự`,
      },
    ],
    toBanDo: [
      { type: 'required', message: 'Tờ bản đồ không được để trống' },
      {
        type: 'maxLength',
        message: `Tờ bản đồ không vượt quá ${KNTCValidatorConsts.MaxToBanDoLength} kí tự`,
      },
    ],
    dienTich: [{ type: 'required', message: 'Diện tích không được để trống' }],
    loaiDat: [{ type: 'required', message: 'Loại đất không được để trống' }],
    diaChiThuaDat: [
      { type: 'required', message: 'Địa chỉ thửa đất không được để trống' },
      {
        type: 'maxLength',
        message: `Địa chỉ thửa đất không vượt quá ${KNTCValidatorConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    tinhThuaDat: [{ type: 'required', message: 'Tỉnh/TP thửa đất không được để trống' }],
    huyenThuaDat: [{ type: 'required', message: 'Quận/Huyện thửa đất không được để trống' }],
    xaThuaDat: [{ type: 'required', message: 'Xã/Phường/TT  thửa đất không được để trống' }],
    duLieuToaDo: [
      {
        type: 'maxLength',
        message: `Dữ liệu tọa độ không vượt quá ${KNTCValidatorConsts.MaxToaDoLength} kí tự`,
      },
    ],
    duLieuHinhHoc: [
      {
        type: 'maxLength',
        message: `Dữ liệu hình học không vượt quá ${KNTCValidatorConsts.MaxHinhHocLength} kí tự`,
      },
    ],
    ghiChu: [
      {
        type: 'maxlength',
        message: `Ghi chú không vượt quá ${KNTCValidatorConsts.MaxGhiChuLength} kí tự`,
      },
    ],
    ngayGQTC: [{ type: 'required', message: 'Ngày giải quyết tố cáo không được để trống' }],
    nguoiGQTC: [
      { type: 'required', message: 'Người bị tố cáo không được để trống' },
      {
        type: 'maxLength',
        message: `Người bị tố cáo không vượt quá ${KNTCValidatorConsts.MaxTenNguoiLength} kí tự`,
      },
    ],
    quyerDinhThuLyGQTC: [
      { type: 'required', message: 'QĐ thụ lý không được để trống' },
      {
        type: 'maxlength',
        message: `QĐ không vượt quá ${KNTCValidatorConsts.MaxSoQDLength} kí tự`,
      },
    ],
    ngayQDGQTC: [{ type: 'required', message: 'Ngày QĐ GQTC không được để trống' }],
    quyetDinhDinhChiGQTC: [
      {
        type: 'maxlength',
        message: `QĐ không vượt quá ${KNTCValidatorConsts.MaxSoQDLength} kí tự`,
      },
    ],
    soVBKLNDTC: [
      { type: 'required', message: 'Số VB KL NDTC không được để trống' },
      {
        type: 'maxlength',
        message: `Số VB KL NDTC không vượt quá ${KNTCValidatorConsts.MaxSoQDLength} kí tự`,
      },
    ],
    ngayNhanTBKQXLKLTC: [
      { type: 'required', message: 'Ngày nhận TB KQXLKLTC không được để trống' },
    ],
    ketQua: [{ type: 'required', message: 'Xã/Phường/TT  thửa đất không được để trống' }],
  };

  get formControls() {
    return this.form.controls;
  }

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private denounceService: DenounceService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private unitService: UnitService,
    private landTypeService: LandTypeService,
    private notificationService: NotificationService
  ) {}

  ngOnInit() {
    this.loadOptions();
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.denounceId = this.config.data?.id;
      this.mode = 'update';
      this.loadDetail(this.denounceId);
    }
  }

  //#region load options
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
    this.toggleBlockUI(true);
    this.landTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<LandTypeLookupDto>) => {
          this.landTypeOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }

  tinhChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(2, id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenOptions = res.items;
            if (!isFirst) {
              this.form.get('maQuanHuyen').reset();
              this.form.get('maXaPhuongTT').reset();
            }
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.huyenOptions = [];
  }
  huyenChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(3, id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaOptions = res.items;
            if (!isFirst) {
              this.form.get('maXaPhuongTT').reset();
            }
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.xaOptions = [];
  }

  tinhThuaDatChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(2, id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.huyenThuaDateOptions = res.items;
            if (!isFirst) {
              this.form.get('huyenThuaDat').reset();
              this.form.get('xaThuaDat').reset();
            }
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.huyenThuaDateOptions = [];
  }
  huyenThuaDatChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(3, id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaThuaDatOptions = res.items;
            if (!isFirst) {
              this.form.get('xaThuaDat').reset();
            }
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else this.xaThuaDatOptions = [];
  }
  //#endregion

  loadDetail(id: any) {
    this.toggleBlockUI(true);
    this.denounceService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: DenounceDto) => {
          this.selectedEntity = response;
          this.tinhChange(this.selectedEntity.maTinhTP, true);
          this.huyenChange(this.selectedEntity.maQuanHuyen, true);
          this.tinhThuaDatChange(this.selectedEntity.tinhThuaDat, true);
          this.huyenThuaDatChange(this.selectedEntity.huyenThuaDat, true);

          this.form.patchValue(this.selectedEntity);
          this.form
            .get('ngaySinh')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity.ngaySinh));
          this.form
            .get('thoiGianTiepNhan')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity.thoiGianTiepNhan));
          this.form
            .get('thoiGianHenTraKQ')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity.thoiGianHenTraKQ));
          this.form
            .get('ngayGQTC')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayGQTC));
          this.form
            .get('ngayQDGQTC')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayQDGQTC));
          this.form
            .get('giaHanGQTC1')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.giaHanGQTC1));
          this.form
            .get('giaHanGQTC2')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.giaHanGQTC2));
          this.form
            .get('ngayNhanTBKQXLKLTC')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayNhanTBKQXLKLTC));

          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) return;
    this.toggleBlockUI(true);
    if (this.utilService.isEmpty(this.denounceId)) {
      let value = this.form.value as CreateDenounceDto;
      let fileAttachmentDtos = this.fileAttachmentComponent.data;
      let files = this.fileAttachmentComponent.files;

      value.fileAttachments = fileAttachmentDtos.map(x => {
        return {
          loaiVuViec: LoaiVuViec.KhieuNai,
          denounceId: x.denounceId,
          tenTaiLieu: x.tenTaiLieu,
          giaiDoan: x.giaiDoan,
          hinhThuc: x.hinhThuc,
          thoiGianBanHanh: x.thoiGianBanHanh,
          ngayNhan: x.ngayNhan,
          thuTuButLuc: x.thuTuButLuc,
          noiDungChinh: x.noiDungChinh,
          fileName: x.fileName,
          contentType: x.contentType,
          contentLength: x.contentLength,
        } as CreateAndUpdateFileAttachmentDto;
      });
      this.denounceService
        .create(value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: DenounceDto) => {
            res.fileAttachments.forEach(fmDto => {
              let file = files.find(f => f.name === fmDto.fileName);
              if (file) {
                this.fileUploads.push({
                  id: fmDto.id,
                  name: fmDto.tenTaiLieu,
                  file: file,
                });
              }
            });
            this.toggleBlockUI(false);
            if (this.fileUploads.length > 0) this.ref.close(this.fileUploads);
            else this.ref.close(res);
          },
          () => {
            this.toggleBlockUI(false);
          }
        );
    } else {
      let value = this.form.value as UpdateDenounceDto;
      this.denounceService
        .update(this.config.data.id, value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          data => {
            this.toggleBlockUI(false);
            this.ref.close(data);
          },
          err => {
            this.toggleBlockUI(false);
          }
        );
    }
  }

  buildForm() {
    this.form = this.fb.group({
      maHoSo: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxMaHoSoLength)],
      ],
      tieuDe: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTieuDeLength)],
      ],
      nguoiToCao: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTenNguoiLength)],
      ],
      cccdCmnd: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCccdCmndLength)],
      ],
      // ngayCapCccdCmnd: [null, [Validators.required]],
      // noiCapCccdCmnd: [
      //   null,
      //   [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxNoiCapCccdCmnd)],
      // ],
      ngaySinh: [null, [Validators.required]],
      dienThoai: [null, [Validators.required, Validators.pattern('^(0[0-9]{9}|\\+84[0-9]{9})$')]],
      email: [null, [Validators.email]],
      diaChiThuongTru: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxDiaChiLength)],
      ],
      diaChiLienHe: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxDiaChiLength)],
      ],
      maTinhTP: [null, [Validators.required]],
      maQuanHuyen: [null, [Validators.required]],
      maXaPhuongTT: [null, [Validators.required]],
      linhVuc: [this.config.data?.linhVuc],
      thoiGianTiepNhan: [null, [Validators.required]],
      thoiGianHenTraKQ: [null, [Validators.required]],
      noiDungVuViec: [null, [Validators.required]],
      nguoiBiToCao: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTenNguoiLength)],
      ],
      boPhanDangXL: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxBoPhanXLLength)],
      ],
      soThua: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxSoThuaLength)],
      ],
      toBanDo: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxToBanDoLength)],
      ],
      dienTich: [null, [Validators.required]],
      loaiDat: [null, [Validators.required]],
      diaChiThuaDat: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxDiaChiLength)],
      ],
      tinhThuaDat: [null, [Validators.required]],
      huyenThuaDat: [null, [Validators.required]],
      xaThuaDat: [null, [Validators.required]],

      duLieuToaDo: [null, [Validators.maxLength(KNTCValidatorConsts.MaxToaDoLength)]],
      duLieuHinhHoc: [null, [Validators.maxLength(KNTCValidatorConsts.MaxHinhHocLength)]],
      ghiChu: [null, Validators.maxLength(KNTCValidatorConsts.MaxGhiChuLength)],

      ngayGQTC: [null, [Validators.required]],
      nguoiGQTC: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTenNguoiLength)],
      ],
      quyerDinhThuLyGQTC: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxSoQDLength)],
      ],
      ngayQDGQTC: [null, [Validators.required]],
      quyetDinhDinhChiGQTC: [null, [Validators.maxLength(KNTCValidatorConsts.MaxSoQDLength)]],
      giaHanGQTC1: [null],
      giaHanGQTC2: [null],

      soVBKLNDTC: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxSoQDLength)],
      ],
      ngayNhanTBKQXLKLTC: [null, [Validators.required]],
      ketQua: [null, [Validators.required]],
      congKhaiKLGQTC: [false],

      concurrencyStamp: [],
    });
  }

  private toggleBlockUI(enabled: boolean) {
    if (enabled == true) {
      this.btnDisabled = true;
      this.blockedPanelDetail = true;
    } else {
      setTimeout(() => {
        this.btnDisabled = false;
        this.blockedPanelDetail = false;
      }, 300);
    }
  }
  close() {
    if (this.ref) {
      this.ref.close();
    }
  }
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
