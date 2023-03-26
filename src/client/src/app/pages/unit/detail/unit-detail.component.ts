import { ListResultDto } from '@abp/ng.core';
import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Status } from '@proxy';
import { UnitTypeLookupDto, UnitTypeService } from '@proxy/unit-types';
import { UnitDto, UnitLookupDto, UnitService } from '@proxy/units';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { KNTCValidatorConsts } from 'src/app/shared/constants/validator.const';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: './unit-detail.component.html',
})
export class UnitDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity = {} as UnitDto;
  statusOptions = [
    { value: Status.Active, text: 'Hoạt động' },
    { value: Status.DeActive, text: 'Khóa' },
  ];
  unitTypeOptions: UnitTypeLookupDto[] = [];
  parentUnitOptions: UnitLookupDto[] = [];
  // Validate
  validationMessages = {
    unitCode: [
      { type: 'required', message: 'Mã không được để trống' },
      {
        type: 'maxLength',
        message: `Mã không vượt quá ${KNTCValidatorConsts.MaxMaHoSoLength} kí tự`,
      },
    ],
    unitName: [
      { type: 'required', message: 'Tên không được để trống' },
      {
        type: 'maxLength',
        message: `Tên không vượt quá ${KNTCValidatorConsts.MaxNameLength} kí tự`,
      },
    ],
    unitTypeId: [{ type: 'required', message: 'Loại địa danh không được để trống' }],
    parentId: [{ type: 'required', message: 'Địa danh cha không được để trống' }],
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
  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private unitService: UnitService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private unitTypeService: UnitTypeService
  ) {}

  ngOnInit() {
    this.getOptions();
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
    }
  }

  getOptions() {
    this.toggleBlockUI(true);
    this.unitTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitTypeLookupDto>) => {
          this.unitTypeOptions = res.items;
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
  }

  loadDetail(id: any) {
    this.toggleBlockUI(true);
    this.unitService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UnitDto) => {
          this.selectedEntity = response;
          this.unitTypeChange(this.selectedEntity.unitTypeId, true);
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
      ? this.unitService.create(this.form.value)
      : this.unitService.update(this.config.data.id, this.form.value);
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
      unitCode: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      unitName: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      unitTypeId: [null, [Validators.required]],
      parentId: [null],
      description: [null, [Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)]],
      orderIndex: [null],
      status: [Status.Active, [Validators.required]],
      concurrencyStamp: [null],
    });
  }
  unitTypeChange(id, isFirst: boolean = false) {
    if (!id) return;
    debugger;
    let parentControl = this.form.get('parentId');
    if (isFirst) parentControl.reset();
    if (id > 1) {
      parentControl.enable();
      parentControl.setValidators([Validators.required]);
      this.toggleBlockUI(true);
      this.unitService
        .getLookup(id - 1)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.parentUnitOptions = res.items;
            this.toggleBlockUI(false);
          },
          () => {
            this.toggleBlockUI(false);
            this.parentUnitOptions = [];
          }
        );
    } else {
      this.parentUnitOptions = [];
      parentControl.disable();
      parentControl.setValidators([]);
    }
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
