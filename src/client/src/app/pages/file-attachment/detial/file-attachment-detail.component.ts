import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';

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
  fileContent: File;

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
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    //Init form
    this.buildForm();

    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadFormDetails(this.config.data?.id);
    }
  }
  loadFormDetails(id: string) {}

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
    });
  }
  choseFile(event) {
    this.fileContent = event.files[0];
  }
  removeFile(event) {
    this.fileContent = null;
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
