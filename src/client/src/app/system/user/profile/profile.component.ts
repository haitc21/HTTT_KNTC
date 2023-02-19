import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { Validators, FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';
import { UtilityService } from 'src/app/shared/services/utility.service';
import { UserDto, UsersService } from '@proxy/users';
import { IdentityUserDto } from '@abp/ng.identity/proxy';
import { FileService } from 'src/app/shared/services/file.service.spec';
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

  formSavedEventEmitter: EventEmitter<any> = new EventEmitter();

  // Validate
  validationMessages = {
    name: [{ type: 'required', message: 'Tên không được để trống' }],
    surname: [{ type: 'required', message: 'Họ không được để trống' }],
    email: [{ type: 'required', message: 'Email không được để trống' }],
    userName: [{ type: 'required', message: 'Tên tài khoản không được để trống' }],
    phoneNumber: [{ type: 'required', message: 'Số ĐT không được để trống' }],
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
    this.toggleBlockUI(true);
    debugger;
    let user = this.form.value;
    user.userName = this.selectedEntity.userName;
    user.email = this.selectedEntity.email;

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
      email: [null, [Validators.required]],
      phoneNumber: [null, [Validators.required]],
      dob: [null],
    });
  }
  myUploader(event) {
    this.toggleBlockUI(true);
    let fike = event.files[0];
    this.fileService
      .uploadAvatar(fike)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (response: string) => {
          this.notificationService.showSuccess('Tải lên ảnh đại diện thành công');
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
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
  ngOnDestroy(): void {
    if (this.ref) {
      this.ref.close();
    }
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
