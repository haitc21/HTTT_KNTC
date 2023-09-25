import { Component, OnInit, EventEmitter, OnDestroy, ChangeDetectorRef, ViewChild } from '@angular/core';
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
import { UtilityService } from 'src/app/_shared/services/utility.service';
import { UserDto, UsersService } from '@proxy/users';
import { IdentityUserDto } from '@abp/ng.identity/proxy';
import { DomSanitizer } from '@angular/platform-browser';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { userTypeOptions } from 'src/app/_shared/constants/consts';
import { UnitLookupDto, UnitService } from '@proxy/units';
import { ListResultDto } from '@abp/ng.core';
import { SelectItemGroup } from 'primeng/api';
import { MultiSelect } from 'primeng/multiselect';

interface Unit {
  label: string,
  value: number
}

@Component({
  templateUrl: './user-detail.component.html',
})

export class UserDetailComponent implements OnInit, OnDestroy {
  @ViewChild('multiselect') multi: MultiSelect;
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public countries: any[] = [];
  public provinces: any[] = [];
  selectedEntity = {} as UserDto;

  listOfUnits!: any;
  listGroup = false;
  userType:number = 1;
  managedUnitIds!: number[];
  selectedUnits!: any;
  userTypeOPtions = userTypeOptions;

  public avatarImage;
  mode: string;
  avatarUrl: any;
  listType: string = '';
  unitLoadedCount = 0;
  // Validate
  validationMessages = {
    name: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    surname: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    email: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      { type: 'email', message: 'Địa chỉ email không chính xác' },
    ],
    userName: [{ type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }],
    password: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
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
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      { type: 'pattern', message: 'Số ĐT không chính xác' },
    ],
    userType: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG },
      { type: 'pattern', message: 'Người dùng phải thuộc một trong các loại tài khoản trong hệ thống' },
    ],
  };
  get formControls() {
    return this.form.controls;
  }

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private userService: UsersService,
    private unitService: UnitService,
    private utilService: UtilityService,
    private fb: FormBuilder,
    private sanitizer: DomSanitizer,
    private notificationService: NotificationService,
    private layoutService: LayoutService
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
          this.form
            .get('userType')
            .setValue(this.selectedEntity.userInfo.userType);
          
          //set list of units
          this.getListOfUnits(this.selectedEntity.userInfo.userType);
          
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
      userType: [1],
      managedUnitIds: [null]
    });
  }
  
  saveChange() {
    this.utilService.markAllControlsAsDirty([this.form]);
    if (this.form.invalid) {
      this.notificationService.showWarn(MessageConstants.FORM_INVALID);
      return;
    }
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

  getListOfUnits(type: number){
    //set the caption
    switch (type){
      case 2: { 
        this.listGroup = false;
        this.listType = "Chọn danh sách TP (thuộc Tỉnh)/Quận/Huyện người dùng được quản lý";
        break; 
      } 
      case 3: { 
          this.listGroup = true;
          this.listType = "Chọn danh sách Xã/Phường người dùng được quản lý";
          break; 
      } 
      default: { 
          this.listGroup = false;
          this.listType = "";
          break; 
      }       
    }
    //get the list of Units tree
    if (this.listGroup){//Phường/Xã
      this.listOfUnits = [] as SelectItemGroup[];
      this.unitService
        .getLookup(2, 24) //Tìm tất cả các Quận/Huyện của Thái Nguyên
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          async (res: ListResultDto<UnitLookupDto>) => {
            this.listOfUnits = res.items.map(x => {
              return {
                label: x?.unitName,
                value: x?.id,
                items: [],
              }
            })
            this.unitLoadedCount = res.items.length;
            
            this.listOfUnits.forEach((x, i) => {
              this.unitService.getLookup(3, x?.value)
              .pipe(takeUntil(this.ngUnsubscribe)).subscribe(
                (res2: ListResultDto<UnitLookupDto>) => {
                    res2.items.forEach((y, j) => {
                      x.items.push(
                        {
                          label: y?.unitName,
                          value: y?.id,
                        } as Unit
                      );
                    });
                    this.unitLoadedCount --;
                    //Only set the unit when load completed
                    if (this.unitLoadedCount == 0)
                      this.setListOfUnits();     
              });
            })            
          });        
    }
    else 
    {//Quận/Huyện/TP trực thuộc
      this.unitService
        .getLookup(type, 24) //Fix cứng 24 là Tỉnh Thái Nguyên
        .pipe(takeUntil(this.ngUnsubscribe))
        .subscribe(
          (res: ListResultDto<UnitLookupDto>) => 
          {
            this.listOfUnits = [] as Unit[];
            this.listOfUnits = res.items.map((x, i) => {
              return {
                label: x?.unitName,
                value: x?.id,
              }
            });
            this.setListOfUnits();
          });
    }
  }

  setListOfUnits(){
    //Set selected MultiSelect by managedUnitIds
    this.selectedUnits = this.selectedEntity.userInfo.managedUnitIds; 
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
