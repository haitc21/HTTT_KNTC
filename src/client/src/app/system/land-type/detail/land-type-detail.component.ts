import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Status } from '@proxy';
import { LandTypeDto, LandTypeService } from '@proxy/land-types';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: './land-type-detail.component.html',
})
export class LandTypeDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity = {} as LandTypeDto;
  statusOptions = [
    { value: Status.Active, text: 'Hoạt động' },
    { value: Status.DeActive, text: 'Khóa' },
  ];
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private landTypeService: LandTypeService,
    private utilService: UtilityService,
    private notificationService: NotificationService,
    private fb: FormBuilder,
    private layoutService: LayoutService
  ) {}

  ngOnInit() {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
    }
  }

  // Validate
  validationMessages = {
    landTypeCode: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Mã không vượt quá ${KNTCValidatorConsts.MaxMaHoSoLength} kí tự`,
      },
    ],
    landTypeName: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      {
        type: 'maxLength',
        message: `Tên không vượt quá ${KNTCValidatorConsts.MaxNameLength} kí tự`,
      },
    ],
    status: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    description: [
      {
        type: 'maxLength',
        message: `Mô tả không vượt quá ${KNTCValidatorConsts.MaxDescriptionLength} kí tự`,
      },
    ],
  };

  get formControls() {
    return this.form.controls;
  }

  loadDetail(id: any) {
    this.layoutService.blockUI$.next(true);
    this.landTypeService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: LandTypeDto) => {
          this.selectedEntity = response;
          this.form.patchValue(this.selectedEntity);
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  saveChange() {
    this.layoutService.blockUI$.next(true);
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(MessageConstants.FORM_INVALID);
      this.layoutService.blockUI$.next(false);
      return;
    }
    let obs$ = this.utilService.isEmpty(this.config.data?.id)
      ? this.landTypeService.create(this.form.value)
      : this.landTypeService.update(this.config.data.id, this.form.value);
    obs$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      () => {
        this.layoutService.blockUI$.next(false);
        this.ref.close(this.form.value);
      },
      err => {
        this.layoutService.blockUI$.next(false);
      }
    );
  }

  buildForm() {
    this.form = this.fb.group({
      landTypeCode: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      landTypeName: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      description: [null, [Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)]],
      orderIndex: [null],
      status: [Status.Active, [Validators.required]],
      concurrencyStamp: [null],
    });
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
