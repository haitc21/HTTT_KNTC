import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder, ValidatorFn, AbstractControl } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { UserDto, UsersService } from '@proxy/users';
import { IdentityUserDto } from '@abp/ng.identity/proxy';

@Component({
  templateUrl: './user-detail.component.html',
})
export class UserDetailComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public countries: any[] = [];
  public provinces: any[] = [];
  selectedEntity = {} as UserDto;
  public avatarImage;
  mode: string;
  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private userService: UsersService,
    private utilService: UtilityService,
    private fb: FormBuilder
  ) {}
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  // Validate
  validationMessages = {
    name: [{ type: 'required', message: 'Tên không được để trống' }],
    surname: [{ type: 'required', message: 'Họ không được để trống' }],
    email: [{ type: 'required', message: 'Email không được để trống' }],
    userName: [{ type: 'required', message: 'Tên tài khoản không được để trống' }],
    password: [
      { type: 'required', message: 'Mật khẩu không được để trống' },
      {
        type: 'pattern',
        message: 'Mật khẩu ít nhất 8 ký tự, ít nhất 1 số, 1 ký tự đặc biệt, và một chữ hoa',
      },
    ],
    confirmPassword: [
      { type: 'required', message: 'Xác nhận mật khẩu không đúng' },
      { type: 'passwordMismatch', message: 'Xác nhận mật khẩu không đúng' },
    ],
    phoneNumber: [{ type: 'required', message: 'Số ĐT không được để trống' }],
  };
  get formControls() {
    return this.form.controls;
  }
  ngOnInit() {
    //Init form
    this.buildForm();

    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadFormDetails(this.config.data?.id);
    } else {
      this.setMode('create');
    }
  }
  loadFormDetails(id: string) {
    this.toggleBlockUI(true);
    this.userService
      .get(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: UserDto) => {
          this.selectedEntity = response;
          this.form.patchValue(this.selectedEntity);
          this.form
            .get('dob')
            .setValue(this.utilService.convertDateToLocal(this.selectedEntity.userInfo.dob));
          this.setMode('update');
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.userService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.ref.close(this.form.value);
            this.toggleBlockUI(false);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
    } else {
      let user = this.form.value;
      user.userName = this.selectedEntity.userName;
      user.email = this.selectedEntity.email;

      this.userService
        .update(this.config.data?.id, user)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.toggleBlockUI(false);

            this.ref.close(this.form.value);
          },
          error: () => {
            this.toggleBlockUI(false);
          },
        });
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

  setMode(mode: string) {
    this.mode = mode;
    if (mode == 'update') {
      this.form.controls['userName'].clearValidators();
      this.form.controls['userName'].disable();
      this.form.controls['email'].clearValidators();
      this.form.controls['email'].disable();
      this.form.controls['password'].clearValidators();
      this.form.controls['password'].disable();
    } else if (mode == 'create') {
      this.form.controls['userName'].enable();
      this.form.controls['userName'].addValidators(Validators.required);
      this.form.controls['email'].enable();
      this.form.controls['email'].addValidators(Validators.required);
      this.form.controls['password'].enable();
      this.form.controls['password'].addValidators([
        Validators.required,
        Validators.pattern(
          '^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-zd$@$!%*?&].{8,}$'
        ),
      ]);
    }
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
      concurrencyStamp: [null],
      name: [null, [Validators.required]],
      surname: [null, [Validators.required]],
      userName: [null, [Validators.required]],
      email: [null, [Validators.required]],
      phoneNumber: [null, [Validators.required]],
      password: password,
      confirmPassword: new FormControl(null, [this.matchPasswordValidator(password)]),
      isActive: [true],
      dob: [null],
    });
  }
  matchPasswordValidator(otherControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const match = otherControl.value === control.value;
      return match ? null : { passwordMismatch: true };
    };
  }
}
