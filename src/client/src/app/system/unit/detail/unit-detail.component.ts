import { ListResultDto } from '@abp/ng.core';
import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { Status } from '@proxy';
import { UnitTypeLookupDto, UnitTypeService } from '@proxy/category-unit-types';
import { UnitDto, UnitLookupDto, UnitService } from '@proxy/units';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
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
    shortName: [
      { type: 'required', message: 'Tên thu gọn không được để trống' },
      {
        type: 'maxLength',
        message: `Tên thu gọn không vượt quá ${KNTCValidatorConsts.MaxNameLength} kí tự`,
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
    private unitTypeService: UnitTypeService,
    private layoutService: LayoutService,
  ) {}

  ngOnInit() {
    this.getOptions();
    this.buildForm();
    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadDetail(this.config.data.id);
    }
  }

  getOptions() {
    this.layoutService.blockUI$.next(true);
    this.unitTypeService
      .getLookup()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        (res: ListResultDto<UnitTypeLookupDto>) => {
          this.unitTypeOptions = res.items;
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  loadDetail(id: any) {
    this.layoutService.blockUI$.next(true);
    this.unitService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UnitDto) => {
          this.selectedEntity = response;
          this.unitTypeChange(this.selectedEntity.unitTypeId, true);
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
    if (this.form.invalid) return;
    let obs$ = this.utilService.isEmpty(this.config.data?.id)
      ? this.unitService.create(this.form.value)
      : this.unitService.update(this.config.data.id, this.form.value);
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
      unitCode: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      unitName: [
        null,
        [Validators.required, Validators.maxLength(KNTCValidatorConsts.MaxCodeLength)],
      ],
      shortName: [
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
    
    let parentControl = this.form.get('parentId');
    if (isFirst) parentControl.reset();
    if (id > 1) {
      parentControl.enable();
      parentControl.setValidators([Validators.required]);
      this.layoutService.blockUI$.next(true);
      this.unitService
        .getLookup(id - 1)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => {
            this.parentUnitOptions = res.items;
            this.layoutService.blockUI$.next(false);
          },
          () => {
            this.layoutService.blockUI$.next(false);
            this.parentUnitOptions = [];
          }
        );
    } else {
      this.parentUnitOptions = [];
      parentControl.disable();
      parentControl.setValidators([]);
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
