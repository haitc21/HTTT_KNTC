import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { LinhVuc, LoaiVuViec } from '@proxy';
import { ComplainDto, ComplainService } from '@proxy/complains';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: './land-complain-detail.component.html',
})
export class LandComplainDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  selectedEntity: ComplainDto;

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
    nguoiDeNghi: [
      { type: 'required', message: 'Người đề nghị không được để trống' },
      {
        type: 'maxLength',
        message: `Người đề nghị không vượt quá ${KNTCValidatorConsts.MaxNguoiDeNghiLength} kí tự`,
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
    dienThaoi: [
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
      { type: 'required', message: 'Dữ liệu tọa độ không được để trống' },
      {
        type: 'maxLength',
        message: `Dữ liệu tọa độ không vượt quá ${KNTCValidatorConsts.MaxToaDoLength} kí tự`,
      },
    ],
    duLieuHinhHoc: [
      { type: 'required', message: 'Dữ liệu hình học không được để trống' },
      {
        type: 'maxLength',
        message: `Dữ liệu hình học không vượt quá ${KNTCValidatorConsts.MaxHinhHocLength} kí tự`,
      },
    ],

    ngayTraKQ: [{ type: 'required', message: 'Ngày trả kết quả không được để trống' }],
    thamQuyen: [
      { type: 'required', message: 'Thẩm quyền không được để trống' },
      {
        type: 'maxlength',
        message: `Thẩm quyền không vượt quá ${KNTCValidatorConsts.MaxThamQuyenLength} kí tự`,
      },
    ],
    soQD: [
      { type: 'required', message: 'Số QĐ không được để trống' },
      {
        type: 'maxlength',
        message: `Số QĐ không vượt quá ${KNTCValidatorConsts.MaxSoQDLength} kí tự`,
      },
    ],
    ghiChu: [
      {
        type: 'maxlength',
        message: `Ghi chú không vượt quá ${KNTCValidatorConsts.MaxGhiChuLength} kí tự`,
      },
    ],
    ketQua: [{ type: 'required', message: 'Kết quả không được để trống' }],

    tenTaiLieu: [
      { type: 'required', message: 'Tên tài liệu không được để trống' },
      {
        type: 'maxlength',
        message: `Tên tài liệu tối đa ${KNTCValidatorConsts.MaxTenTaiLieuLength} ký tự`,
      },
    ],
    hinhThuc: [
      { type: 'required', message: 'Hình thức không được để trống' },
      {
        type: 'maxlength',
        message: `Hình thức tối đa ${KNTCValidatorConsts.MaxHinhThucLength} ký tự`,
      },
    ],
    thuTuButLuc: [
      { type: 'required', message: 'Thứ tự bút lục không được để trống' },
      {
        type: 'maxlength',
        message: `Thứ tự bút lục tối đa ${KNTCValidatorConsts.MaxThuTuButLucLength} ký tự`,
      },
    ],
  };

  get formControls() {
    return this.form.controls;
  }
  get kqgqHoSos(): FormArray {
    return this.form.get('kqgqHoSos') as FormArray;
  }

  get tepDinhKemHoSos(): FormArray {
    return this.form.get('tepDinhKemHoSos') as FormArray;
  }

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private complainService: ComplainService,
    private utilService: UtilityService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
    }
  }

  loadDetail(id: any) {
    this.toggleBlockUI(true);
    this.complainService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ComplainDto) => {
          this.selectedEntity = response;
          this.form.patchValue(this.selectedEntity);
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.complainService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          data => {
            this.ref.close(data);
            this.toggleBlockUI(false);
          },
          err => {
            this.toggleBlockUI(false);
          }
        );
    } else {
      this.complainService
        .update(this.config.data.id, this.form.value)
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
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxMaHoSoLength)],
      ],
      tieuDe: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTieuDeLength)],
      ],
      nguoiDeNghi: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxNguoiDeNghiLength)],
      ],
      cccdCmnd: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCccdCmndLength)],
      ],
      // ngayCapCccdCmnd: ['', [Validators.required]],
      // noiCapCccdCmnd: [
      //   '',
      //   [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxNoiCapCccdCmnd)],
      // ],
      ngaySinh: ['', [Validators.required]],
      dienThaoi: [null, [Validators.required, Validators.pattern('^(0[0-9]{9}|\\+84[0-9]{9})$')]],
      email: [null, [Validators.email]],
      diaChiThuongTru: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxDiaChiLength)],
      ],
      diaChiLienHe: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxDiaChiLength)],
      ],
      maTinhTP: ['', [Validators.required]],
      maQuanHuyen: ['', [Validators.required]],
      maXaPhuongTT: ['', [Validators.required]],
      linhVuc: [LinhVuc.DataDai],
      thoiGianTiepNhan: [null, [Validators.required]],
      thoiGianHenTraKQ: [null, [Validators.required]],
      noiDungVuViec: ['', [Validators.required]],
      boPhanDangXL: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxBoPhanXLLength)],
      ],
      soThua: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxSoThuaLength)],
      ],
      toBanDo: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxToBanDoLength)],
      ],
      dienTich: [null, [Validators.required]],
      loaiDat: [null, [Validators.required]],
      diaChiThuaDat: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxDiaChiLength)],
      ],
      tinhThuaDat: ['', [Validators.required]],
      huyenThuaDat: ['', [Validators.required]],
      xaThuaDat: ['', [Validators.required]],

      duLieuToaDo: ['', [Validators.maxLength(KNTCValidatorConsts.MaxToaDoLength)]],
      duLieuHinhHoc: ['', [Validators.maxLength(KNTCValidatorConsts.MaxHinhHocLength)]],

      listKQGQHoSoDeleted: [null],
      listTepDinhKemHoSosDeleted: [null],
      concurrencyStamp: [null],
      kqgqHoSos: this.fb.array([]),
      tepDinhKemHoSos: this.fb.array([]),
    });
  }

  addKQGQHoSo(): void {
    const kqgqHoSoFormGroup = this.fb.group({
      lanGQ: [''],
      ngayTraKQ: ['', Validators.required],
      thamQuyen: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxThamQuyenLength)],
      ],
      soQD: ['', [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxSoQDLength)]],
      ghiChu: ['', Validators.maxLength(KNTCValidatorConsts.MaxGhiChuLength)],
      ketQua: ['', Validators.required],
    });

    this.kqgqHoSos.push(kqgqHoSoFormGroup);
  }

  removeKQGQHoSo(index: number): void {
    this.kqgqHoSos.removeAt(index);
  }

  addTepDinhKemHoSo(): void {
    const tepDinhKemHoSoFormGroup = this.fb.group({
      tenTaiLieu: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTenTaiLieuLength)],
      ],
      hinhThuc: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxHinhThucLength)],
      ],
      thoiGianBanHanh: [null],
      ngayNhan: [null],
      thuTuButLuc: [
        '',
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxThuTuButLucLength)],
      ],
      noiDungChinh: [''],
    });

    this.tepDinhKemHoSos.push(tepDinhKemHoSoFormGroup);
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
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
