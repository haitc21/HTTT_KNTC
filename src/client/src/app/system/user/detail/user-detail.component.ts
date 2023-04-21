import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import {
  Validators,
  FormControl,
  FormGroup,
  FormBuilder,
  ValidatorFn,
  AbstractControl,
} from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { UserDto, UsersService } from '@proxy/users';
import { IdentityUserDto } from '@abp/ng.identity/proxy';
import { DomSanitizer } from '@angular/platform-browser';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

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
  avatarUrl: any;
  // Validate
  validationMessages = {
    name: [{ type: 'required', message: 'Tên không được để trống' }],
    surname: [{ type: 'required', message: 'Họ không được để trống' }],
    email: [
      { type: 'required', message: 'Email không được để trống' },
      { type: 'email', message: 'Địa chỉ email không chính xác' },
    ],
    userName: [{ type: 'required', message: 'Tên tài khoản không được để trống' }],
    password: [
      { type: 'required', message: 'Mật khẩu không được để trống' },
      {
        type: 'pattern',
        message: 'Mật khẩu ít nhất 8 ký tự, ít nhất 1 số, 1 ký tự đặc biệt, và một chữ hoa',
      },
    ],
    confirmPassword: [
      { type: 'required', message: 'Xác nhận mật khẩu không chính xác' },
      { type: 'passwordMismatch', message: 'Xác nhận mật khẩu không chính xác' },
    ],
    phoneNumber: [
      { type: 'required', message: 'Số ĐT không được để trống' },
      { type: 'pattern', message: 'Số ĐT không chính xác' },
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
    private sanitizer: DomSanitizer,
    private layoutService: LayoutService,
  ) {}

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
    this.layoutService.blockUI$.next(true);
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
          if (response.avatarContent) {
            let objectURL = 'data:image/png;base64,' + response.avatarContent;
            this.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
          }
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  saveChange() {
this.utilService.markAllControlsAsDirty([this.form]);
    if(this.form.invalid) return;;
    this.layoutService.blockUI$.next(true);
    if (this.utilService.isEmpty(this.config.data?.id)) {
      this.userService
        .create(this.form.value)
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe({
          next: () => {
            this.ref.close(this.form.value);
            this.layoutService.blockUI$.next(false);
          },
          error: () => {
            this.layoutService.blockUI$.next(false);
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
            this.layoutService.blockUI$.next(false);

            this.ref.close(this.form.value);
          },
          error: () => {
            this.layoutService.blockUI$.next(false);
          },
        });
    }
  }
  

  setMode(mode: string) {
    this.mode = mode;
    if (mode == 'update') {
      this.form.controls['userName'].clearValidators();
      this.form.controls['userName'].disable();
      this.form.controls['password'].clearValidators();
      this.form.controls['password'].disable();
    } else if (mode == 'create') {
      this.form.controls['userName'].enable();
      this.form.controls['userName'].addValidators(Validators.required);
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
      email: [null, [Validators.required, Validators.email]],
      phoneNumber: [null, [Validators.required, Validators.pattern('^(0[0-9]{9}|\\+84[0-9]{9})$')]],
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
