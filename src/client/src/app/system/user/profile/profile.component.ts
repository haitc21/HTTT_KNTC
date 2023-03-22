import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { UserDto, UsersService } from '@proxy/users';
import { FileService } from 'src/app/shared/services/file.service';
import { DomSanitizer } from '@angular/platform-browser';
import { NotificationService } from 'src/app/shared/services/notification.service';

@Component({
  templateUrl: './profile.component.html',
})
export class ProfileComponent implements OnInit, OnDestroy {
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
  avatarContent: File;

  // Validate
  validationMessages = {
    name: [{ type: 'required', message: 'Tên không được để trống' }],
    surname: [{ type: 'required', message: 'Họ không được để trống' }],
    email: [
      { type: 'required', message: 'Email không được để trống' },
      { type: 'email', message: 'Địa chỉ email không chính xác' },
    ],
    userName: [{ type: 'required', message: 'Tên tài khoản không được để trống' }],
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
    private fileService: FileService,
    private notificationService: NotificationService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit() {
    //Init form
    this.buildForm();

    if (this.utilService.isEmpty(this.config.data?.id) == false) {
      this.loadFormDetails(this.config.data?.id);
    }
    this.getAvatar();
    this.fileService.avatarUrl$.subscribe(url => {
      if (url) this.avatarUrl = url; // Cập nhật đường dẫn tới avatar của người dùng
    });
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
this.utilService.markAllControlsAsDirty([this.form]);
    if(this.form.invalid) return;;
    this.toggleBlockUI(true);
    let user = this.form.value;
    user.userName = this.selectedEntity.userName;
    user.email = this.selectedEntity.email;
    if (this.avatarContent) this.uploadAvatar();
    this.userService
      .updateUserInfo(this.config.data?.id, user)
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

  uploadAvatar() {
    this.toggleBlockUI(true);
    this.fileService
      .uploadAvatar(this.avatarContent)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: string) => {
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }
  setMode(mode: string) {
    this.mode = mode;
    this.form.controls['userName'].clearValidators();
    this.form.controls['userName'].disable();
    this.form.controls['email'].clearValidators();
    this.form.controls['email'].disable();
  }
  buildForm() {
    this.form = this.fb.group({
      concurrencyStamp: [null],
      name: [null, [Validators.required]],
      surname: [null, [Validators.required]],
      userName: [null, [Validators.required]],
      email: [null, [Validators.required, Validators.email]],
      phoneNumber: [null, [Validators.required, Validators.pattern('^(0[0-9]{9}|\\+84[0-9]{9})$')]],
      dob: [null],
    });
  }
  choseFile(event) {
    this.avatarContent = event.files[0];
  }
  removeFile(event) {
    this.avatarContent = null;
  }

  getAvatar() {
    this.fileService.getAvatar(this.config.data?.id).subscribe(data => {
      if (data) {
        let objectURL = 'data:image/png;base64,' + data;
        this.avatarUrl = this.sanitizer.bypassSecurityTrustUrl(objectURL);
      }
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
