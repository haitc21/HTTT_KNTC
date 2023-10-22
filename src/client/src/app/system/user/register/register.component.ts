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
import { CreateAndUpdateUserDto, UserDto, UsersService } from '@proxy/users';
import { IdentityUserDto } from '@abp/ng.identity/proxy';
import { DomSanitizer } from '@angular/platform-browser';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { MessageConstants } from 'src/app/_shared/constants/messages.const';
import { NotificationService } from 'src/app/_shared/services/notification.service';
import { userTypeOptions } from 'src/app/_shared/constants/consts';
import { UnitLookupDto, UnitService, UnitTreeLookupDto } from '@proxy/units';
import { ListResultDto } from '@abp/ng.core';
import { SelectItemGroup, TreeNode } from 'primeng/api';
import { MultiSelect } from 'primeng/multiselect';
import { UserType } from '@proxy';

interface Unit {
  label: string,
  value: number
}

@Component({
  templateUrl: './register.component.html',
})

export class RegisterComponent implements OnInit, OnDestroy {
  @ViewChild('multiselect') multi: MultiSelect;
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public form: FormGroup;
  public title: string;
  public btnDisabled = false;
  public countries: any[] = [];
  public provinces: any[] = [];

  userTypeOPtions = userTypeOptions;
  avatarUrl: any;
  unitOptionLabel: string = '';
  unitOptions: TreeNode<UnitTreeLookupDto>[];
  selectedNodes: TreeNode<UnitTreeLookupDto>[] = [];
  UserType = UserType;

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
    managedUnitIds: [
      { type: 'required', message: MessageConstants.REQUIRED_ERROR_MSG }
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
  ) { }

  ngOnInit() {
    this.buildForm();
    this.getListOfUnits(UserType.QuanLyTinh);
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
      isActive: [false],
      dob: [null],
      userType: [UserType.QuanLyTinh],
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
    let user = this.form.value as CreateAndUpdateUserDto;
    user.managedUnitIds = this.form.value?.managedUnitIds?.map(x => Number(x.key));
    this.userService
      .register(user)
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
  }
  changeUserType(userType: number) {
    this.unitOptions = this.resetSelectable(this.unitOptions, userType);
    this.form.get('managedUnitIds').reset();
    this.selectedNodes = [];
    this.setUnitOptionsLabel(userType);
    if (userType != UserType.QuanLyTinh)
      this.form.get('managedUnitIds').setValidators([Validators.required]);
    else
      this.form.get('managedUnitIds').setValidators([]);
  }

  getListOfUnits(userType: number, managedUnitIds: string[] = []) {
    this.layoutService.blockUI$.next(true);
    this.setUnitOptionsLabel(userType);
    this.unitService
      .getTreeLookup(24) // Thái Nguyên
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        res => {
          if (res)
            this.unitOptions = this.convertToTreeNodeList(res.items, userType, null, managedUnitIds);
          this.layoutService.blockUI$.next(false);
        });
  }

  setUnitOptionsLabel(userType: number) {
    switch (userType) {
      case UserType.QuanLyHuyen: {
        this.unitOptionLabel = "TP (thuộc Tỉnh)/Quận/Huyện người dùng được quản lý";
        break;
      }
      case UserType.QuanLyXa: {
        this.unitOptionLabel = "Xã/Phường người dùng được quản lý";
        break;
      }
      default: {
        this.unitOptionLabel = "";
        break;
      }
    }
  }

  matchPasswordValidator(otherControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl): { [key: string]: any } | null => {
      const match = otherControl.value === control.value;
      return match ? null : { passwordMismatch: true };
    };
  }

  resetSelectable(unitOptions: TreeNode<UnitTreeLookupDto>[], unitTypeId: number, parent: TreeNode<UnitTreeLookupDto> = null): TreeNode<UnitTreeLookupDto>[] {
    return unitOptions.map(unit => {
      const treeNode: TreeNode<UnitTreeLookupDto> = {
        ...unit,
        selectable: unit.data.unitTypeId == unitTypeId
      };

      if (unit.children && unit.children.length > 0) {
        treeNode.children = this.resetSelectable(unit.children, unitTypeId, treeNode);
      }

      return treeNode;
    });
  }

  convertToTreeNodeList(
    unitList: UnitTreeLookupDto[],
    unitTypeId: number,
    parent: TreeNode<UnitTreeLookupDto> = null,
    managedUnitIds: string[] = []): TreeNode<UnitTreeLookupDto>[] {
    return unitList.map(unit => {
      const treeNode: TreeNode<UnitTreeLookupDto> = {
        label: unit.unitName,
        key: String(unit.id),
        expanded: false,
        selectable: unit.unitTypeId == unitTypeId,
        data: unit,
        parent: parent,
        children: []
      };

      if (unit.children && unit.children.length > 0) {
        treeNode.children = this.convertToTreeNodeList(unit.children, unitTypeId, treeNode, managedUnitIds);
      }
      if (managedUnitIds.includes(treeNode.key)) {
        this.selectedNodes.push(treeNode);
      }

      return treeNode;
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