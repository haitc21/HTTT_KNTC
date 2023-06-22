import { PermissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoleComponent } from './role/role.component';
import { UserComponent } from './user/user.component';
import { DocumentTypeComponent } from './document-type/document-type.component';
import { LandTypeComponent } from './land-type/land-type.component';
import { UnitTypeComponent } from './unit-type/unit-type.component';
import { UnitComponent } from './unit/unit.component';

const routes: Routes = [
  {
    path: 'role',
    component: RoleComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'AbpIdentity.Roles',
    },
  },
  {
    path: 'user',
    component: UserComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'AbpIdentity.Users',
    },
  },

  {
    path: 'document-type',
    component: DocumentTypeComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'DocumentTypes',
    },
  },
  {
    path: 'land-type',
    component: LandTypeComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'LandTypes',
    },
  },
  {
    path: 'unit-type',
    component: UnitTypeComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'UnitTypes',
    },
  },
  {
    path: 'unit',
    component: UnitComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'Units',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SystemRoutingModule {}
