import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { SysConfigDto, SysConfigService } from '@proxy/sys-configs';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { UtilityService } from 'src/app/_shared/services/utility.service';

@Component({
  templateUrl: './sys-config-detail.component.html',
})
export class SysConfigDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity = {} as SysConfigDto;

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private sysConfigService: SysConfigService,
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
    name: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
    value: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    description: [
      { type: 'required', message: 'Bạn phải mô tả' },
      { type: 'minlength', message: 'Bạn phải nhập ít nhất 3 kí tự' },
      { type: 'maxlength', message: 'Bạn không được nhập quá 255 kí tự' },
    ],
  };

  get formControls() {
    return this.form.controls;
  }

  loadDetail(id: any) {
    this.layoutService.blockUI$.next(true);
    this.sysConfigService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: SysConfigDto) => {
          this.selectedEntity = response;
          if (!this.utilService.isEmpty(this.config.data?.id)) {
            this.form.get('name').disable();
          }
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
      ? this.sysConfigService.create(this.form.value)
      : this.sysConfigService.update(this.config.data.id, this.form.value);
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
      value: [null, [Validators.required]],
      description: [null, [Validators.required, Validators.maxLength(500)]],
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
