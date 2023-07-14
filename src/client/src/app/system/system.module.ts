import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';
import { RoleComponent } from './role/role.component';
import { RoleDetailComponent } from './role/detail/role-detail.component';
import { PermissionGrantComponent } from './permission-grant/permission-grant.component';
import { UserComponent } from './user/user.component';
import { SystemRoutingModule } from './system-routing.module';
import { PickListModule } from 'primeng/picklist';
import { ToolbarModule } from 'primeng/toolbar';
import { TagModule } from 'primeng/tag';
import { ListboxModule } from 'primeng/listbox';
import { TooltipModule } from 'primeng/tooltip';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { UserDetailComponent } from './user/detail/user-detail.component';
import { RoleAssignComponent } from './user/role-assign/role-assign.component';
import { SetPasswordComponent } from './user/set-password/set-password.component';
import { CoreModule } from '@abp/ng.core';
import { PasswordModule } from 'primeng/password';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { CardModule } from 'primeng/card';
import { FileUploadModule } from 'primeng/fileupload';
import { ProfileComponent } from './user/profile/profile.component';
import { DocumentTypeComponent } from './document-type/document-type.component';
import { DocumentTypeDetailComponent } from './document-type/detail/document-type-detail.component';
import { LandTypeComponent } from './land-type/land-type.component';
import { LandTypeDetailComponent } from './land-type/detail/land-type-detail.component';
import { UnitTypeComponent } from './unit-type/unit-type.component';
import { UnitTypeDetailComponent } from './unit-type/detail/unit-type-detail.component';
import { UnitComponent } from './unit/unit.component';
import { UnitDetailComponent } from './unit/detail/unit-detail.component';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SysConfigComponent } from './sys-config/sys-config.component';
import { SysConfigDetailComponent } from './sys-config/detail/sys-config-detail.component';
import { ValidationMessagedModule } from '../_shared/modules/validation-message/validation-message.module';
import { InputSwitchModule } from 'primeng/inputswitch';

const COMPONENT = [
  RoleComponent,
  RoleDetailComponent,
  PermissionGrantComponent,
  UserComponent,
  UserDetailComponent,
  RoleAssignComponent,
  SetPasswordComponent,
  ProfileComponent,
  DocumentTypeComponent,
  DocumentTypeDetailComponent,
  LandTypeComponent,
  LandTypeDetailComponent,
  UnitTypeComponent,
  UnitTypeDetailComponent,
  UnitComponent,
  UnitDetailComponent,
  SysConfigComponent,
  SysConfigDetailComponent,
];

@NgModule({
  declarations: [...COMPONENT],
  imports: [
    CoreModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    InputTextareaModule,
    ValidationMessagedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule,
    SystemRoutingModule,
    PickListModule,
    ToolbarModule,
    TagModule,
    ListboxModule,
    TooltipModule,
    TieredMenuModule,
    PasswordModule,
    OverlayPanelModule,
    CardModule,
    FileUploadModule,
    BreadcrumbModule,
    InputSwitchModule
  ],
  entryComponents: [...COMPONENT],
})
export class SystemModule {}
