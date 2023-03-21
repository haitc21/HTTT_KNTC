import { PagedResultDto } from '@abp/ng.core';
import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { RoleDto, RolesService } from '@proxy/roles';
import { UserDto, UsersService } from '@proxy/users';
import {
  IdentityRoleDto,
  IdentityUserDto,
  IdentityUserUpdateRolesDto,
} from '@proxy/volo/abp/identity/models';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { forkJoin, Subject, takeUntil } from 'rxjs';

@Component({
  templateUrl: './role-assign.component.html',
})
export class RoleAssignComponent implements OnInit, OnDestroy {
  private ngUnsubscribe = new Subject<void>();

  // Default
  public blockedPanelDetail: boolean = false;
  public title: string;
  public btnDisabled = false;
  public closeBtnName: string;
  public availableRoles: IdentityRoleDto[] = [];
  public seletedRoles: IdentityRoleDto[] = [];

  constructor(
    public ref: DynamicDialogRef,
    public config: DynamicDialogConfig,
    private userService: UsersService,
    private roleService: RolesService
  ) {}

  ngOnInit() {
    this.loadData();
  }
  loadData() {
    this.toggleBlockUI(true);
    this.userService
      .getAssignableRoles(this.config.data.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (repsonse: PagedResultDto<IdentityRoleDto>) => {
          this.availableRoles = repsonse.items;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
    this.toggleBlockUI(true);
    this.userService
      .getRoles(this.config.data.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (repsonse: PagedResultDto<IdentityRoleDto>) => {
          this.seletedRoles = repsonse.items;
          this.toggleBlockUI(false);
        },
        error: () => {
          this.toggleBlockUI(false);
        },
      });
  }

  saveChange() {
    this.toggleBlockUI(true);
    let roleNames = this.seletedRoles.map(x => x.name);
    let updateRoleDto: IdentityUserUpdateRolesDto = { roleNames: [...roleNames] };
    this.userService
      .updateRoles(this.config.data.id, updateRoleDto)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        () => {
          this.ref.close(this.seletedRoles);
          this.toggleBlockUI(false);
        },
        () => {
          this.toggleBlockUI(false);
        }
      );
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
