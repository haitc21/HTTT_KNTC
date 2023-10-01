import { ListResultDto, PermissionService } from '@abp/ng.core';
import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { LoaiVuViec, TrangThai, UserType } from '@proxy';
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
import { KNTCValidatorConsts } from 'src/app/_shared/constants/validator.const';
import { FileUploadDto } from 'src/app/_shared/models/file-upload.class';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { UtilityService } from 'src/app/_shared/services/utility.service';
import { FileAttachmentComponent } from '../../file-attachment/file-attachment.component';
import { MapComponent } from 'src/app/_shared/modules/map/map.component';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { trangthaiOptions, loaiKQOptions, loaiKhieuNaiOptions } from 'src/app/_shared/constants/consts';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { AppValidatorFn } from 'src/app/_shared/functions/validator-fn';
import { OAuthService } from 'angular-oauth2-oidc';
import { UserDto, UserInfoDto, UsersService } from '@proxy/users';
@Component({
  templateUrl: './complain-detail.component.html',
  styleUrls: ['./complain-detail.component.scss'],
})
export class ComplainDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();
  @ViewChild(FileAttachmentComponent) fileAttachmentComponent: FileAttachmentComponent;
  @ViewChild(MapComponent) mapComponent: MapComponent;

  complainId: string;
  userId = '';
  userInfo: UserInfoDto;

  mode: 'create' | 'update' | 'view' = 'view';

  // Permissions
  hasPermissionUpdate = false;

  loaiVuViec = LoaiVuViec.KhieuNai;
  fileUploads: FileUploadDto[] = [];
  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public luuTru = false;
  public showBtnLuuTru = false;

  selectedEntity: ComplainDto;
  dataMap: any[] = [];
  toado: string;

  // option
  tinhOptions: UnitLookupDto[] = [];
  huyenOptions: UnitLookupDto[] = [];
  xaOptions: UnitLookupDto[] = [];
  huyenThuaDatOptions: UnitLookupDto[] = [];
  xaThuaDatOptions: UnitLookupDto[] = [];
  landTypeOptions: LandTypeLookupDto[];
  trangThaiOPtions = trangthaiOptions;
  loaiKQOPtions = loaiKQOptions;
  loaiKhieuNaiOptions = loaiKhieuNaiOptions;
  LoaiVuViec = LoaiVuViec;

  //
  coordinateLabel = 'Lấy tọa độ';
  drawLabel = 'Vẽ trên bản đồ';

  // Validate
  validationMessages = {
    maHoSo: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Mã hồ sơ không vượt quá ${KNTCValidatorConsts.MaxMaHoSoLength} kí tự`,
      },
    ],
    tieuDe: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Tiêu đề không vượt quá ${KNTCValidatorConsts.MaxTieuDeLength} kí tự`,
      },
    ],
    nguoiNopDon: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Người nộp đơn không vượt quá ${KNTCValidatorConsts.MaxTenNguoiLength} kí tự`,
      },
    ],
    cccdCmnd: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `CCCD/CMND không vượt quá ${KNTCValidatorConsts.MaxCccdCmndLength} kí tự`,
      },
    ],
    // ngayCapCccdCmnd: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG  }],
    // noiCapCccdCmnd: [
    //   { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG  },
    //   {
    //     type: 'maxLength',
    //     message: `Nơi cấp CCCD/CMND không vượt quá ${KNTCValidatorConsts.MaxNoiCapCccdCmnd} kí tự`,
    //   },
    // ],
    ngaySinh: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    dienThoai: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      { type: 'pattern', message: 'Số điện thoại không chính xác' },
    ],
    email: [{ type: 'email', message: 'Địa chỉ email không chính xác' }],
    diaChiThuongTru: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Địa chỉ thường trú không vượt quá ${KNTCValidatorConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    diaChiLienHe: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Địa chỉ liên hệ không vượt quá ${KNTCValidatorConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    maTinhTP: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    maQuanHuyen: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    maXaPhuongTT: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    thoiGianTiepNhan: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    thoiGianHenTraKQ: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    noiDungVuViec: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    boPhanDangXL: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Bộ phận thực hiện không vượt quá ${KNTCValidatorConsts.MaxBoPhanXLLength} kí tự`,
      },
    ],
    soThua: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Số thửa không vượt quá ${KNTCValidatorConsts.MaxSoThuaLength} kí tự`,
      },
    ],
    toBanDo: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Tờ bản đồ không vượt quá ${KNTCValidatorConsts.MaxToBanDoLength} kí tự`,
      },
    ],
    dienTich: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    loaiDat: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    diaChiThuaDat: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Địa chỉ thửa đất không vượt quá ${KNTCValidatorConsts.MaxDiaChiLength} kí tự`,
      },
    ],
    tinhThuaDat: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    huyenThuaDat: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    xaThuaDat: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    duLieuToaDo: [
      {
        type: 'maxLength',
        message: `Dữ liệu tọa độ không vượt quá ${KNTCValidatorConsts.MaxToaDoLength} kí tự`,
      },
      {
        type: 'invalidCoordiate',
        message: MessageConstants.COORDIATE_INVALID,
      },
    ],
    duLieuHinhHoc: [
      // {
      //   type: 'maxLength',
      //   message: `Dữ liệu hình học không vượt quá ${KNTCValidatorConsts.MaxHinhHocLength} kí tự`,
      // },
      {
        type: 'invalidGeoJson',
        message: MessageConstants.GEOJSON_INVALID,
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
    congKhai: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
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
    private userService: UsersService,
    private landTypeService: LandTypeService,
    private oAuthService: OAuthService,
    private permissionService: PermissionService,
    private notificationService: NotificationService,
    private layoutService: LayoutService
  ) { }

  ngOnInit() {
    this.buildForm();
    this.getUserInfo();
    this.loadOptions();

    if (this.utilService.isEmpty(this.config.data?.mode) == false) {
      this.mode = this.config.data?.mode;
    }

    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.complainId = this.config.data?.id;

      this.loadDetail(this.complainId)
    }
    else //Tạo mới
      this.checkPermission();
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
      duLieuToaDo: [
        null,
        [
          Validators.maxLength(KNTCValidatorConsts.MaxToaDoLength),
          AppValidatorFn.oordiateValidator(),
        ],
      ],
      // duLieuHinhHoc: [null, [Validators.maxLength(KNTCValidatorConsts.MaxHinhHocLength)]],
      duLieuHinhHoc: [null, [AppValidatorFn.geoJsonValidator()]],
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
      luuTru: [false],
      trangThai: [0, [Validators.required]],
      concurrencyStamp: [],
    });
  }

  loadDetail(id: any) {
    this.layoutService.blockUI$.next(true);
    this.dataMap.splice(0); //clear the array
    this.complainService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (res: ComplainDto) => {
          if (!res) {
            this.notificationService.showError(MessageConstants.NOT_FOUND);
            return;
          }
          this.selectedEntity = res;

          //now check the permission
          this.checkPermission();

          this.dataMap.push(res);
          this.dataMap[0].loaiVuViec = LoaiVuViec.KhieuNai;
          this.toado = res?.duLieuToaDo ?? '';

          this.tinhChange(this.selectedEntity.maTinhTP, true);
          this.huyenChange(this.selectedEntity.maQuanHuyen, true);
          this.tinhThuaDatChange(this.selectedEntity.tinhThuaDat, true);
          this.huyenThuaDatChange(this.selectedEntity.huyenThuaDat, true);

          //setTimeout(() => {
          this.patchValueForm();
          //  this.layoutService.blockUI$.next(false);
          //}, 100);

          //determine the mode if haspermission and not luutru
          this.luuTru = this.selectedEntity.luuTru;
          if (!this.luuTru && this.hasPermissionUpdate) {
            if (this.selectedEntity.trangThai == TrangThai.DaKetLuan)
              this.showBtnLuuTru = true;
            this.changeMode(true);
          }
          else this.changeMode(false);

          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  patchValueForm() {
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
    //if (this.mode == 'view' && !this.hasPermissionUpdate) this.form.disable();    
  }

  getUserInfo() {
    const accessToken = this.oAuthService.getAccessToken();

    if (accessToken) {//Chỉ có giá trị khi token là valid
      this.userInfo = JSON.parse(localStorage.getItem('userInfo'));
      if (this.userInfo)
        this.userId = this.userInfo?.userId;
      else {
        let decodedAccessToken = atob(accessToken.split('.')[1]);
        let accessTokenJson = JSON.parse(decodedAccessToken);
        this.userId = accessTokenJson.sub ?? '';

        if (this.userId) {
          this.userService.getUserInfo(this.userId)
            .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
              next: (response: UserDto) => {
                this.userInfo = response.userInfo;
              },
              error: () => { },
            });
        }
      }
    }
  }

  checkPermission() {
    var hasRoleUpdate = this.permissionService.getGrantedPolicy('Complains.Edit');
    var hasPermissionUpdate = false;
    if (this.userId) {//Chỉ có quyền update nếu được quản lý Huyen hoac Xa nay
      if (this.complainId) {
        if (((this.userInfo?.userType == UserType.QuanLyHuyen) && (this.userInfo?.managedUnitIds.includes(this.selectedEntity?.maQuanHuyen)))
          || ((this.userInfo?.userType == UserType.QuanLyXa) && (this.userInfo?.managedUnitIds.includes(this.selectedEntity?.maXaPhuongTT))))
          hasPermissionUpdate = true;
      }
      else //Tạo mới thì luôn có quyền
        hasPermissionUpdate = true;
    }
    this.hasPermissionUpdate = hasRoleUpdate && hasPermissionUpdate;
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
      if (this.hasPermissionUpdate && (this.userInfo?.userType == UserType.QuanLyHuyen)) {
        //Nếu quản lý huyện thì load toàn bộ huyện quản lý

        this.unitService.getLookupByIds(this.userInfo?.managedUnitIds)
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
      }
      else {
        this.unitService.getLookup(2, id)
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
      }
    } else this.huyenOptions = [];
  }

  huyenChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);

      if (this.hasPermissionUpdate && (this.userInfo?.userType == UserType.QuanLyXa)) {

        this.unitService //Nếu quản lý xã thì chỉ load các xã quản lý
          .getLookupByIds(this.userInfo?.managedUnitIds)
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
      }
      else {
        //Còn lại chỉ load các xã theo parentid, kể cả quản lý huyện vì huyện đã giới hạn trong huyenOptions
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
      }
    } else this.xaOptions = [];
  }

  tinhThuaDatChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);

      if (this.hasPermissionUpdate && (this.userInfo?.userType == UserType.QuanLyHuyen)) {

        //Nếu quản lý huyện thì load toàn bộ huyện quản lý
        this.unitService.getLookupByIds(this.userInfo?.managedUnitIds)
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe(
            (res: ListResultDto<UnitLookupDto>) => {
              this.huyenThuaDatOptions = res.items;
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
      }
      else {
        this.unitService
          .getLookup(2, id)
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe(
            (res: ListResultDto<UnitLookupDto>) => {
              this.huyenThuaDatOptions = res.items;
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
      }

    } else this.huyenThuaDatOptions = [];
  }

  huyenThuaDatChange(id: number, isFirst: boolean = false) {
    if (id) {
      this.layoutService.blockUI$.next(true);

      if (this.hasPermissionUpdate && (this.userInfo?.userType == UserType.QuanLyXa)) {

        this.unitService //Nếu quản lý xã thì chỉ load các xã quản lý
          .getLookupByIds(this.userInfo?.managedUnitIds)
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
      }
      else {
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
      }
    } else this.xaThuaDatOptions = [];
  }
  //#endregion

  saveChange() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(MessageConstants.FORM_INVALID);
      this.layoutService.blockUI$.next(false);
      return;
    }
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
            choPhepDownload: x.choPhepDownload
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
      this.complainService
        .update(this.config.data.id, value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          data => {
            this.layoutService.blockUI$.next(false);
            this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
            this.ref.close(data);
          },
          err => {
            this.layoutService.blockUI$.next(false);
          }
        );
    }
  }

  Lock() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(MessageConstants.FORM_INVALID);
      this.layoutService.blockUI$.next(false);
      return;
    }
    this.layoutService.blockUI$.next(true);

    if (!this.utilService.isEmpty(this.complainId))
      this.UpdateLock(true);

    this.layoutService.blockUI$.next(false);
  }

  UnLock() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(MessageConstants.FORM_INVALID);
      this.layoutService.blockUI$.next(false);
      return;
    }
    this.layoutService.blockUI$.next(true);

    if (!this.utilService.isEmpty(this.complainId))
      this.UpdateLock(false);

    this.layoutService.blockUI$.next(false);
  }

  private UpdateLock(trangthaiLuuTru: boolean) {
    let value = this.form.value as UpdateComplainDto;
    value.luuTru = trangthaiLuuTru;

    this.complainService
      .update(this.config.data.id, value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        data => {
          this.layoutService.blockUI$.next(false);
          this.notificationService.showSuccess(MessageConstants.UPDATED_OK_MSG);
          this.ref.close(data);
        },
        err => {

        }
      );
  }

  changeMode(edit: boolean) {
    if (edit) {
      this.mode = 'update';
      this.form.enable();
    }
    else {
      this.mode = 'view';
      this.form.disable();
    }
  }

  getCoordiate() {
    if (this.coordinateLabel == 'Lấy tọa độ') {
      //Chưa có -> Cho phép chọn tọa độ
      this.mapComponent?.letCoordinate();
      this.notificationService.showInfo(MessageConstants.CHOSE_COORDIATE);
      this.coordinateLabel = 'Cập nhật!';
    } else if (this.coordinateLabel == 'Hủy') {
      this.form.get('duLieuToaDo').setValue(this.selectedEntity?.duLieuToaDo);
      this.coordinateLabel = 'Lấy tọa độ';
    } else if (this.mapComponent?.duLieuToaDo) {
      //Đã có -> Lấy tọa độ
      this.form.get('duLieuToaDo').setValue(this.mapComponent?.duLieuToaDo);
      this.coordinateLabel = 'Hủy';
    }
  }

  getDraw() {
    //Cho phép vẽ
    if (this.drawLabel == 'Vẽ trên bản đồ') {
      //Chưa có -> Cho phép vẽ
      this.mapComponent?.letDraw();
      this.notificationService.showInfo(MessageConstants.USE_MAP_DRAW);
      this.drawLabel = 'Cập nhật!';
    } else if (this.drawLabel == 'Hủy') {
      this.form.get('duLieuHinhHoc').setValue(this.selectedEntity?.duLieuHinhHoc);
      this.drawLabel = 'Vẽ trên bản đồ';
    } else if (this.mapComponent?.duLieuHinhhoc) {
      //get dữ liệu hình học
      this.form.get('duLieuHinhHoc').setValue(this.mapComponent?.duLieuHinhhoc);
      this.drawLabel = 'Hủy';
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
