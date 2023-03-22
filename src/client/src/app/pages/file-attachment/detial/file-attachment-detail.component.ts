import {
  Component,
  OnInit,
  EventEmitter,
  OnDestroy,
  ChangeDetectorRef,
  Input,
  Output,
} from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { DocumentTypeLookupDto, DocumentTypeService } from '@proxy/document-types';
import { ListResultDto } from '@abp/ng.core';
import { CreateAndUpdateFileAttachmentDto, FileAttachmentDto } from '@proxy/file-attachments';
import { LoaiVuViec } from '@proxy';

@Component({
  selector: 'app-file-attachment-detail',
  templateUrl: './file-attachment-detail.component.html',
})
export class FileAttachmentDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  @Input() loaiVuViec: LoaiVuViec;
  @Input() item: FileAttachmentDto;
  @Output() save: EventEmitter<any> = new EventEmitter();
  @Output() close: EventEmitter<any> = new EventEmitter();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  file: File;

  documentTypeOptions: DocumentTypeLookupDto[];
  giaiDoanOptions = [
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
    noiDungChinh: [{ type: 'required', message: 'Nội dung chính không được để trống' }],
    fileName: [{ type: 'required', message: 'Vui lòng chọn tệp' }],
    thoiGianBanHanh: [{ type: 'required', message: 'Thời gian ban hành không được để trống' }],
    ngayNhan: [{ type: 'required', message: 'Ngày nhận không được để trống' }],
  };

  get formControls() {
    return this.form.controls;
  }

  constructor(
    private utilService: UtilityService,
    private fb: FormBuilder,
    private documentTypeService: DocumentTypeService
  ) {}

  ngOnInit() {
    this.getOptions();
    this.buildForm();
  }

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
    let dto = this.form.value as CreateAndUpdateFileAttachmentDto;
    if (this.file) {
      dto.contentLength = this.file.size;
      dto.contentType = this.file.type;
      dto.fileName = this.file.name;
    }
    let fileAttachment = {
      ...dto,
      file: this.file,
    };
    this.save.emit(fileAttachment);
  }

  buildForm() {
    this.form = this.fb.group({
      Id: [],
      tenTaiLieu: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxTenTaiLieuLength)],
      ],
      giaiDoan: [null, Validators.required],
      hinhThuc: [null, Validators.required],
      thoiGianBanHanh: [null, Validators.required],
      ngayNhan: [null, Validators.required],
      thuTuButLuc: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxThuTuButLucLength)],
      ],
      noiDungChinh: [null, Validators.required],
      fileName: [null, [Validators.required]],
      fileContent: [],
      contentType: [],
      contentLength: [],
      loaiVuViec: [this.loaiVuViec],
      complainId: [],
      denounceId: [],
    });
    if (this.item) {
      this.form.patchValue(this.item);
      this.form.get('ngayNhan').setValue(this.utilService.convertDateToLocal(this.item.ngayNhan));
      this.form.get('thoiGianBanHanh').setValue(this.utilService.convertDateToLocal(this.item.thoiGianBanHanh));
    }
  }

  choseFile(event) {
    this.file = event.files[0];
    this.form.get('fileName').setValue(this.file.name);
  }
  removeFile(event) {
    this.file = null;
    this.form.get('fileName').reset();
  }
  closeModal() {
    this.close.emit();
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
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
