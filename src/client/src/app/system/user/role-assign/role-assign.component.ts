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
import { LayoutService } from 'src/app/layout/service/app.layout.service';

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
    private roleService: RolesService,
    private layoutService: LayoutService
  ) {}

  ngOnInit() {
    this.loadData();
  }
  loadData() {
    this.layoutService.blockUI$.next(true);
    this.userService
      .getAssignableRoles(this.config.data.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (repsonse: PagedResultDto<IdentityRoleDto>) => {
          this.availableRoles = repsonse.items;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
    this.layoutService.blockUI$.next(true);
    this.userService
      .getRoles(this.config.data.id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: (repsonse: PagedResultDto<IdentityRoleDto>) => {
          this.seletedRoles = repsonse.items;
          this.layoutService.blockUI$.next(false);
        },
        error: () => {
          this.layoutService.blockUI$.next(false);
        },
      });
  }

  saveChange() {
    this.layoutService.blockUI$.next(true);
    let roleNames = this.seletedRoles.map(x => x.name);
    let updateRoleDto: IdentityUserUpdateRolesDto = { roleNames: [...roleNames] };
    this.userService
      .updateRoles(this.config.data.id, updateRoleDto)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe(
        () => {
          this.ref.close(this.seletedRoles);
          this.layoutService.blockUI$.next(false);
        },
        () => {
          this.layoutService.blockUI$.next(false);
        }
      );
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
