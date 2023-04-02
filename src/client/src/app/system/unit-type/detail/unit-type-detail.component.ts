import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Status } from '@proxy';
import { UnitTypeDto, UnitTypeService } from '@proxy/category-unit-types';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: './unit-type-detail.component.html',
})
export class UnitTypeDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity = {} as UnitTypeDto;
  statusOptions = [
    { value: Status.Active, text: 'Hoạt động' },
    { value: Status.DeActive, text: 'Khóa' },
  ];
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private unitTypeService: UnitTypeService,
    private utilService: UtilityService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
    }
  }

  // Validate
  validationMessages = {
    unitTypeCode: [
      { type: 'required', message: 'Mã không được để trống' },
      {
        type: 'maxLength',
        message: `Mã không vượt quá ${KNTCValidatorConsts.MaxMaHoSoLength} kí tự`,
      },
    ],
    unitTypeName: [
      { type: 'required', message: 'Tên không được để trống' },
      {
        type: 'maxLength',
        message: `Tên không vượt quá ${KNTCValidatorConsts.MaxNameLength} kí tự`,
      },
    ],
    status: [{ type: 'required', message: 'Tên không được để trống' }],
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
    this.toggleBlockUI(true);
    this.unitTypeService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UnitTypeDto) => {
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
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) return;
    let obs$ = this.utilService.isEmpty(this.config.data?.id)
      ? this.unitTypeService.create(this.form.value)
      : this.unitTypeService.update(this.config.data.id, this.form.value);
    obs$.pipe(takeUntil(this.ngUnsubscribe)).subscribe(
      () => {
        this.toggleBlockUI(false);
        this.ref.close(this.form.value);
      },
      err => {
        this.toggleBlockUI(false);
      }
    );
  }

  buildForm() {
    this.form = this.fb.group({
      unitTypeCode: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      unitTypeName: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      description: [null, [Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)]],
      orderIndex: [null],
      status: [Status.Active, [Validators.required]],
      concurrencyStamp: [null],
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