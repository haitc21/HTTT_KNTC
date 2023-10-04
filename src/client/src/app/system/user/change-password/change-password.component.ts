import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import {
  Validators,
  FormControl,
  FormGroup,
  FormBuilder,
  ValidatorFn,
  AbstractControl,
} from '@angular/forms';
import { UsersService } from '@proxy/users';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { UtilityService } from 'src/app/_shared/services/utility.service';
import { ChangePasswordInput } from '@proxy/volo/abp/account';

@Component({
  templateUrl: './change-password.component.html',
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
    currentPassword: [
      { type: 'required', message: 'Bạn phải nhập mật khẩu' },
      {
        type: 'pattern',
        message: 'Mật khẩu ít nhất 8 ký tự, ít nhất 1 số, 1 ký tự đặc biệt, và một chữ hoa',
      },
    ],
    newPassword: [
      { type: 'required', message: 'Bạn phải nhập mật khẩu' },
      {
        type: 'pattern',
        message: 'Mật khẩu ít nhất 8 ký tự, ít nhất 1 số, 1 ký tự đặc biệt, và một chữ hoa',
      },
    ],
    confirmNewPassword: [
      { type: 'required', message: 'Xác nhận mật khẩu không đúng' },
      { type: 'passwordMismatch', message: 'Xác nhận mật khẩu không đúng' },
    ],
  };
  get formControls() {
    return this.form.controls;
  }

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private userService: UsersService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private layoutService: LayoutService
  ) { }

  ngOnInit() {
    this.buildForm();
  }

  saveChange() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) return;;
    debugger
    let value = {
      currentPassword: this.form.value.currentPassword,
      newPassword: this.form.value.newPassword,
    } as ChangePasswordInput;
    this.layoutService.blockUI$.next(true);
    this.userService
      .changePassword(value)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        () => {
          this.layoutService.blockUI$.next(false);
          this.ref.close(this.form.value);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
  }

  buildForm() {
    let currentPassword = new FormControl(
      null,
      Validators.compose([
        Validators.required,
        Validators.pattern(
          '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
        ),
      ])
    );
    let newPassword = new FormControl(
      null,
      Validators.compose([
        Validators.required,
        Validators.pattern(
          '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
        ),
      ])
    );
    this.form = this.fb.group({
      currentPassword: currentPassword,
      newPassword: newPassword,
      confirmNewPassword: new FormControl(null, [this.matchPasswordValidator(newPassword)]),
    });
  }
  matchPasswordValidator(otherControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const match = otherControl.value === control.value;
      return match ? null : { passwordMismatch: true };
    };
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
