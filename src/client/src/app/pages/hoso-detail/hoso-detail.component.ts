import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { LinhVuc, LoaiVuViec } from '@proxy';
import { ComplainDto, ComplainService } from '@proxy/complains';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { HoSoConsts } from 'src/app/shared/constants/ho-so.const';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: './hoso-detail.component.html',
})
export class HoSoDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity: ComplainDto;

  loaiVuViec: LoaiVuViec;
  linhVuc: LinhVuc;
  

  // Validate
  validationMessages = {
    maHoSo: [
      { type: 'required', message: 'Mã hồ sơ không được để trống' },
      { type: 'maxLength', message: `Mã hồ sơ không vượt quá ${HoSoConsts.MaxMaHoSoLength} kí tự` },
    ],
    tieuDe: [
      { type: 'required', message: 'Tiêu đề không được để trống' },
      { type: 'maxLength', message: `Tiêu đề không vượt quá ${HoSoConsts.MaxTieuDeLength} kí tự` },
    ],
    nguoiDeNghi: [
      { type: 'required', message: 'Người đề nghị không được để trống' },
      {
        type: 'maxLength',
        message: `Người đề nghị không vượt quá ${HoSoConsts.MaxNguoiDeNghiLength} kí tự`,
      },
    ],
    cccdCmnd: [
      { type: 'required', message: 'CCCD/CMND không được để trống' },
      {
        type: 'maxLength',
        message: `CCCD/CMND không vượt quá ${HoSoConsts.MaxCccdCmndLength} kí tự`,
      },
    ],
    ngayCapCccdCmnd: [{ type: 'required', message: 'Ngày cấp CCCD/CMND không được để trống' }],
    noiCapCccdCmnd: [
      { type: 'required', message: 'Nơi cấp CCCD/CMND không được để trống' },
      {
        type: 'maxLength',
        message: `Nơi cấp CCCD/CMND không vượt quá ${HoSoConsts.MaxNoiCapCccdCmnd} kí tự`,
      },
    ],
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
        message: `Địa chỉ thường trú không vượt quá ${HoSoConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    diaChiLienHe: [
      { type: 'required', message: 'Địa chỉ liên hệ không được để trống' },
      {
        type: 'maxLength',
        message: `Địa chỉ liên hệ không vượt quá ${HoSoConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    maTinhTP: [{ type: 'required', message: 'Tỉnh/TP không được để trống' }],
    maQuanHuyen: [{ type: 'required', message: 'Quận/huyện không được để trống' }],
    maXaPhuongTT: [{ type: 'required', message: 'Xã/phường/TT không được để trống' }],
    loaiVuViec: [{ type: 'required', message: 'Loại vụ việc không được để trống' }],
    linhVuc: [{ type: 'required', message: 'Lĩnh vực không được để trống' }],
    ngayTiepNhan: [{ type: 'required', message: 'Mgày tiếp nhận không được để trống' }],
    ngayHenTraKQ: [{ type: 'required', message: 'Ngày hẹn trả kết quả không được để trống' }],
    noiDungVuViec: [{ type: 'required', message: 'Nội dung vụ việc không được để trống' }],
    soThua: [
      { type: 'required', message: 'Số thửa không được để trống' },
      { type: 'maxLength', message: `Số thửa không vượt quá ${HoSoConsts.MaxSoThuaLength} kí tự` },
    ],
    toBanDo: [
      { type: 'required', message: 'Tờ bản đồ không được để trống' },
      {
        type: 'maxLength',
        message: `Tờ bản đồ không vượt quá ${HoSoConsts.MaxToBanDoLength} kí tự`,
      },
    ],
    dienTich: [
      { type: 'required', message: 'Diện tích không được để trống' },
      {
        type: 'maxLength',
        message: `Diện tích không vượt quá ${HoSoConsts.MaxDienTichLength} kí tự`,
      },
    ],
    loaiDat: [
      { type: 'required', message: 'Loại đất không được để trống' },
      {
        type: 'maxLength',
        message: `Loại đất không vượt quá ${HoSoConsts.MaxLoaiDatLength} kí tự`,
      },
    ],
    diaChiThuaDat: [
      { type: 'required', message: 'Địa chỉ thửa đất không được để trống' },
      {
        type: 'maxLength',
        message: `Địa chỉ thửa đất không vượt quá ${HoSoConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    tinhThuaDat: [{ type: 'required', message: 'Tỉnh/TP thửa đất không được để trống' }],
    huyenThuaDat: [{ type: 'required', message: 'Quận/Huyện thửa đất không được để trống' }],
    xaThuaDat: [{ type: 'required', message: 'Xã/Phường/TT  thửa đất không được để trống' }],
    duLieuToaDo: [
      { type: 'required', message: 'Dữ liệu tọa độ không được để trống' },
      {
        type: 'maxLength',
        message: `Dữ liệu tọa độ không vượt quá ${HoSoConsts.MaxToaDoLength} kí tự`,
      },
    ],
    duLieuHinhHoc: [
      { type: 'required', message: 'Dữ liệu hình học không được để trống' },
      {
        type: 'maxLength',
        message: `Dữ liệu hình học không vượt quá ${HoSoConsts.MaxHinhHocLength} kí tự`,
      },
    ],

    ngayTraKQ: [{ type: 'required', message: 'Ngày trả kết quả không được để trống' }],
    thamQuyen: [
      { type: 'required', message: 'Thẩm quyền không được để trống' },
      {
        type: 'maxlength',
        message: `Thẩm quyền không vượt quá ${HoSoConsts.MaxThamQuyenLength} kí tự`,
      },
    ],
    soQD: [
      { type: 'required', message: 'Số QĐ không được để trống' },
      { type: 'maxlength', message: `Số QĐ không vượt quá ${HoSoConsts.MaxSoQDLength} kí tự` },
    ],
    ghiChu: [
      { type: 'maxlength', message: `Ghi chú không vượt quá ${HoSoConsts.MaxGhiChuLength} kí tự` },
    ],
    ketQua: [{ type: 'required', message: 'Kết quả không được để trống' }],

    tenTaiLieu: [
      { type: 'required', message: 'Tên tài liệu không được để trống' },
      { type: 'maxlength', message: `Tên tài liệu tối đa ${HoSoConsts.MaxTenTaiLieuLength} ký tự` },
    ],
    hinhThuc: [
      { type: 'required', message: 'Hình thức không được để trống' },
      { type: 'maxlength', message: `Hình thức tối đa ${HoSoConsts.MaxHinhThucLength} ký tự` },
    ],
    thuTuButLuc: [
      { type: 'required', message: 'Thứ tự bút lục không được để trống' },
      {
        type: 'maxlength',
        message: `Thứ tự bút lục tối đa ${HoSoConsts.MaxThuTuButLucLength} ký tự`,
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
    this.loaiVuViec = this.config.data?.loaiVuViec;
    this.linhVuc  = this.config.data?.linhVuc;
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
      maHoSo: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxMaHoSoLength)]],
      tieuDe: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxTieuDeLength)]],
      nguoiDeNghi: [
        '',
        [Validators.required, Validators.maxLength(HoSoConsts.MaxNguoiDeNghiLength)],
      ],
      cccdCmnd: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxCccdCmndLength)]],
      ngayCapCccdCmnd: ['', [Validators.required]],
      noiCapCccdCmnd: [
        '',
        [Validators.required, Validators.maxLength(HoSoConsts.MaxNoiCapCccdCmnd)],
      ],
      ngaySinh: ['', [Validators.required]],
      dienThaoi: [null, [Validators.required, Validators.pattern('^(0[0-9]{9}|\\+84[0-9]{9})$')]],
      email: [null, [Validators.email]],
      diaChiThuongTru: [
        '',
        [Validators.required, Validators.maxLength(HoSoConsts.MaxDiaChiLength)],
      ],
      diaChiLienHe: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxDiaChiLength)]],
      maTinhTP: ['', [Validators.required]],
      maQuanHuyen: ['', [Validators.required]],
      maXaPhuongTT: ['', [Validators.required]],
      loaiVuViec: [this.loaiVuViec, [Validators.required]],
      linhVuc: [this.linhVuc, [Validators.required]],
      ngayTiepNhan: [null, [Validators.required]],
      ngayHenTraKQ: [null, [Validators.required]],
      noiDungVuViec: ['', [Validators.required]],
      soThua: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxSoThuaLength)]],
      toBanDo: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxToBanDoLength)]],
      dienTich: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxDienTichLength)]],
      loaiDat: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxLoaiDatLength)]],
      diaChiThuaDat: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxDiaChiLength)]],
      tinhThuaDat: ['', [Validators.required]],
      huyenThuaDat: ['', [Validators.required]],
      xaThuaDat: ['', [Validators.required]],

      duLieuToaDo: ['', [Validators.maxLength(HoSoConsts.MaxToaDoLength)]],
      duLieuHinhHoc: ['', [Validators.maxLength(HoSoConsts.MaxHinhHocLength)]],

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
      thamQuyen: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxThamQuyenLength)]],
      soQD: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxSoQDLength)]],
      ghiChu: ['', Validators.maxLength(HoSoConsts.MaxGhiChuLength)],
      ketQua: ['', Validators.required],
    });

    this.kqgqHoSos.push(kqgqHoSoFormGroup);
  }

  removeKQGQHoSo(index: number): void {
    this.kqgqHoSos.removeAt(index);
  }

  addTepDinhKemHoSo(): void {
    const tepDinhKemHoSoFormGroup = this.fb.group({
      tenTaiLieu: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxTenTaiLieuLength)]],
      hinhThuc: ['', [Validators.required, Validators.maxLength(HoSoConsts.MaxHinhThucLength)]],
      thoiGianBanHanh: [null],
      ngayNhan: [null],
      thuTuButLuc: [
        '',
        [Validators.required, Validators.maxLength(HoSoConsts.MaxThuTuButLucLength)],
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
