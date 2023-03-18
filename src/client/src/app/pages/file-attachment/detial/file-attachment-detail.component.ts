import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { DocumentTypeLookupDto, DocumentTypeService } from '@proxy/document-types';
import { ListResultDto } from '@abp/ng.core';

@Component({
  templateUrl: './file-attachment-detail.component.html',
})
export class FileAttachmentDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  file: File;

  documentTypeOptions: DocumentTypeLookupDto[];
  giaiDoanOptions = [
    { value: 0, text: 'Tất cả' },
    { value: 1, text: 'Khiếu nại lần I' },
    { value: 2, text: 'Khiếu nại lần II' },
  ];

  // Validate
  validationMessages = {
    tenTaiLieu: [
      { type: 'required', message: 'Tên tài liệu không được để trống' },
      {
        type: 'maxlength',
        message: `Tên tài liệu tối đa ${KNTCValidatorConsts.MaxTenTaiLieuLength} ký tự`,
      },
    ],
    giaiDoan: [{ type: 'required', message: 'Giai đoạn không được để trống' }],
    hinhThuc: [{ type: 'required', message: 'Hình thức không được để trống' }],
    thuTuButLuc: [
      { type: 'required', message: 'Thứ tự bút lục không được để trống' },
      {
        type: 'maxlength',
        message: `Thứ tự bút lục tối đa ${KNTCValidatorConsts.MaxThuTuButLucLength} ký tự`,
      },
    ],
    noiDungChinh: [{ type: 'required', message: 'Hình thức không được để trống' }],
  };

  get formControls() {
    return this.form.controls;
  }

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private documentTypeService: DocumentTypeService
  ) {}

  ngOnInit() {
    //Init form
    this.buildForm();

    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadFormDetails(this.config.data?.id);
    }
  }
  loadFormDetails(id: string) {}
  getOptions() {
    this.toggleBlockUI(true);
    this.documentTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<DocumentTypeLookupDto>) => {
          this.documentTypeOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }

  saveChange() {
    this.ref.close(this.form.value);
  }

  buildForm() {
    this.form = this.fb.group({
      tenTaiLieu: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTenTaiLieuLength)],
      ],
      giaiDoan: [null, Validators.required],
      hinhThuc: [null, Validators.required],
      thoiGianBanHanh: [],
      ngayNhan: [],
      thuTuButLuc: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxThuTuButLucLength)],
      ],
      noiDungChinh: [null, Validators.required],
      fileContent: []
    });
  }
  choseFile(event) {
    this.file = event.files[0];
  }
  removeFile(event) {
    this.file = null;
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
