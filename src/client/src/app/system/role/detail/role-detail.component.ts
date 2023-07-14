import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { RoleDto, RolesService } from '@proxy/roles';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MessageConstants } from 'src/app/shared/constants/messages.const';
import { NotificationService } from 'src/app/shared/services/notification.service';
import { UtilityService } from 'src/app/shared/services/utility.service';

@Component({
  templateUrl: './role-detail.component.html',
})
export class RoleDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity = {} as RoleDto;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private roleService: RolesService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private notificationService: NotificationService,
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
    name: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    description: [
      { type: 'required', message: 'Bạn phải mô tả' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
    ],
  };

  get formControls() {
    return this.form.controls;
  }

  loadDetail(id: any) {
    this.layoutService.blockUI$.next(true);
    this.roleService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: RoleDto) => {
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
      ? this.roleService.create(this.form.value)
      : this.roleService.update(this.config.data.id, this.form.value);
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
      name: [null, [Validators.required, Validators.maxLength(255), Validators.minLength(3)]],
      description: [null, [Validators.required, Validators.maxLength(500)]],
      isPublic: [true],
      isDefault: [false],
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
