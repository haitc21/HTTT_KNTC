import { ListResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Validators, FormGroup, FormBuilder, FormArray } from '@angular/forms';
import { LoaiKetQua, LoaiKhieuNai, LoaiVuViec } from '@proxy';
import {
  ComplainDto,
  ComplainService,
  CreateComplainDto,
  UpdateComplainDto,
} from '@proxy/complains';
import { CreateAndUpdateFileAttachmentDto } from '@proxy/file-attachments';
import { LandTypeLookupDto, LandTypeService } from '@proxy/land-types';
import { UnitLookupDto, UnitService } from '@proxy/units';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { FileUploadDto } from 'src/app/shared/models/file-upload.class';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { FileAttachmentComponent } from '../../file-attachment/file-attachment.component';
import { MapComponent } from 'src/app/shared/modules/map/map.component';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
  templateUrl: './complain-detail.component.html',
  styleUrls: ['./complain-detail.component.scss'],
})
export class ComplainDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  @ViewChild(FileAttachmentComponent) fileAttachmentComponent: FileAttachmentComponent;
  @ViewChild(MapComponent) mapComponent: MapComponent;

  complainId: string;
  mode: 'create' | 'update' | 'view' = 'view';
    
  // Permissions
  hasPermissionUpdate = false;

  loaiVuViec = LoaiVuViec.KhieuNai;
  fileUploads: FileUploadDto[] = [];
  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  selectedEntity: ComplainDto;
  dataMap: any[] = [];
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
  loaiKhieuNaiOptions = [
    { value: LoaiKhieuNai.KhieuNai, text: 'Khiếu nại' },
    { value: LoaiKhieuNai.KhieuKien, text: 'Khiếu kiện' },
  ];
  LoaiVuViec = LoaiVuViec;

  //
  coordinateLabel = "Lấy tọa độ";
  drawLabel = "Vẽ trên bản đồ";

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
    nguoiNopDon: [
      { type: 'required', message: 'Người đề nghị không được để trống' },
      {
        type: 'maxLength',
        message: `Người đề nghị không vượt quá ${KNTCValidatorConsts.MaxTenNguoiLength} kí tự`,
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
    thamQuyen: [
      {
        type: 'maxlength',
        message: `Thẩm quyền không vượt quá ${KNTCValidatorConsts.MaxThamQuyenLength} kí tự`,
      },
    ],
    soQD: [
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
    congKhai: [{ type: 'required', message: 'Không được để trống' }],
  };

  get formControls() {
    return this.form.controls;
  }

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private complainService: ComplainService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private unitService: UnitService,
    private landTypeService: LandTypeService,
    private permissionService: PermissionService,
    private notificationService: NotificationService,
    private layoutService: LayoutService,
  ) {}

  ngOnInit() {
    this.getPermission();
    this.loadOptions();
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.mode) == false) {
      this.mode = this.config.data?.mode;
    }
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.complainId = this.config.data?.id;
      this.loadDetail(this.complainId);
    }
  }

  //#region load options
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
    this.layoutService.blockUI$.next(true);
    this.landTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<LandTypeLookupDto>) => {
          this.landTypeOptions = res.items;
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  tinhChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);
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
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.huyenOptions = [];
  }

  huyenChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(3, id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaOptions = res.items;
            if (!isFirst) {
              this.form.get('maXaPhuongTT').reset();
            }
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.xaOptions = [];
  }

  tinhThuaDatChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);
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
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.huyenThuaDateOptions = [];
  }

  huyenThuaDatChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(3, id)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.xaThuaDatOptions = res.items;
            if (!isFirst) {
              this.form.get('xaThuaDat').reset();
            }
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else this.xaThuaDatOptions = [];
  }
  //#endregion

  loadDetail(id: any) {
    this.layoutService.blockUI$.next(true);
    this.dataMap.splice(0);//clear the array
    this.complainService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: ComplainDto) => {
          this.selectedEntity = response;

          this.dataMap.push(response);
          this.dataMap[0].loaiVuViec = LoaiVuViec.KhieuNai;

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
            .get('ngayKhieuNai1')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayKhieuNai1));
          this.form
            .get('ngayTraKQ1')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayTraKQ1));
          this.form
            .get('ngayKhieuNai2')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayKhieuNai2));
          this.form
            .get('ngayTraKQ2')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity?.ngayTraKQ2));

          if (this.mode == 'view') this.form.disable();
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  saveChange() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) return;
    this.layoutService.blockUI$.next(true);
    if (this.utilService.isEmpty(this.complainId)) {
      let value = this.form.value as CreateComplainDto;
      if (this.fileAttachmentComponent?.data) {
        let fileAttachmentDtos = this.fileAttachmentComponent.data;
        value.fileAttachments = fileAttachmentDtos.map(x => {
          return {
            loaiVuViec: LoaiVuViec.KhieuNai,
            complainId: x.complainId,
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
            congKhai: x.congKhai,
          } as CreateAndUpdateFileAttachmentDto;
        });
      }

      this.complainService
        .create(value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ComplainDto) => {
            res.fileAttachments.forEach(fmDto => {
              if (this.fileAttachmentComponent?.files) {
                let file = this.fileAttachmentComponent?.files.find(f => f.name === fmDto.fileName);
                if (file)
                  this.fileUploads.push({
                    id: fmDto.id,
                    name: fmDto.tenTaiLieu,
                    file: file,
                  });
              }
            });
            this.layoutService.blockUI$.next(false);
            if (this.fileUploads.length > 0) this.ref.close(this.fileUploads);
            else this.ref.close(res);
          },
          () => {
            this.layoutService.blockUI$.next(false);
          }
        );
    } else {
      let value = this.form.value as UpdateComplainDto;
      //console.log('cong khai', value.congKhai);

      this.complainService
        .update(this.config.data.id, value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          data => {
            this.layoutService.blockUI$.next(false);
            this.ref.close(data);
          },
          err => {
            this.layoutService.blockUI$.next(false);
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
      nguoiNopDon: [
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

      loaiKhieuNai1: [],
      ngayKhieuNai1: [],
      ngayTraKQ1: [],
      thamQuyen1: [null, [Validators.maxLength(KNTCValidatorConsts.MaxThamQuyenLength)]],
      soQD1: [null, [Validators.maxLength(KNTCValidatorConsts.MaxSoQDLength)]],
      ketQua1: [],

      loaiKhieuNai2: [],
      ngayKhieuNai2: [],
      ngayTraKQ2: [],
      thamQuyen2: [null, [Validators.maxLength(KNTCValidatorConsts.MaxThamQuyenLength)]],
      soQD2: [null, [Validators.maxLength(KNTCValidatorConsts.MaxSoQDLength)]],
      ketQua2: [],

      congKhai: [false, [Validators.required]],

      concurrencyStamp: [],
    });
  }

  getPermission() {
    this.hasPermissionUpdate = this.permissionService.getGrantedPolicy('Complains.Edit');
  }
  
  changeEditMode(){
    this.mode = 'update';
    this.form.enable();
  }

  getCoordiate() {    
    if (this.coordinateLabel=="Hủy"){
      this.form.get('duLieuToaDo').setValue(this.selectedEntity?.duLieuToaDo);
      this.coordinateLabel = "Lấy tọa độ";
    }
    else if (this.mapComponent?.duLieuToaDo) {
      //Đã có -> Lấy tọa độ      
      this.form.get('duLieuToaDo').setValue(this.mapComponent?.duLieuToaDo);
      this.coordinateLabel = "Hủy";
    }
    else{//Chưa có -> Cho phép chọn tọa độ      
      this.mapComponent?.letCoordinate();
      this.notificationService.showSuccess('Bạn hãy chọn một điểm trên bản đồ để thay đổi vị trí có khiếu nại!');
      this.coordinateLabel = "Cập nhật!";
    }
  }

  letDraw() {
    //Cho phép vẽ
    if (this.drawLabel=="Hủy"){
      this.form.get('duLieuToaDo').setValue(this.selectedEntity?.duLieuHinhHoc);
      this.drawLabel = "Vẽ trên bản đồ";
    }
    else if (this.mapComponent?.duLieuHinhhoc) {
      //get dữ liệu hình học   
      this.form.get('duLieuHinhHoc').setValue(this.mapComponent?.duLieuHinhhoc);
      this.drawLabel = "Hủy";
    } else {
      //Cho phép vẽ
      this.mapComponent?.letDraw();
      
      this.notificationService.showSuccess('Bạn hãy Sử dụng công cụ vẽ trên bản đồ để thể hiện thửa đất có khiếu nại!');
      this.drawLabel = "Cập nhật!";
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
