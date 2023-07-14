import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import {
  Validators,
  FormControl,
  FormGroup,
  FormBuilder,
  ValidatorFn,
  AbstractControl,
} from '@angular/forms';
import { RoleDto } from '@proxy/roles';
import { UsersService } from '@proxy/users';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { UtilityService } from 'src/app/_shared/services/utility.service';

@Component({
  templateUrl: './set-password.component.html',
})
export class SetPasswordComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  selectedEntity = {} as RoleDto;
  // Validate
  noSpecial: RegExp = /^[^<>*!_~]+$/;
  validationMessages = {
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
    private layoutService: LayoutService,
  ) {}

  ngOnInit() {
    this.buildForm();
  }

  saveChange() {
this.utilService.markAllControlsAsDirty([this.form]);
    if(this.form.invalid) return;;
    this.layoutService.blockUI$.next(true);
    this.userService
      .setPassword(this.config.data.id, this.form.value)
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
    let password = new FormControl(
      null,
      Validators.compose([
        Validators.required,
        Validators.pattern(
          '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
        ),
      ])
    );
    this.form = this.fb.group({
      newPassword: password,
      confirmNewPassword: new FormControl(null, [this.matchPasswordValidator(password)]),
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
