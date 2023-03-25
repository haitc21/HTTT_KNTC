import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { ComplainComponent } from './complain/complain.component';
import { SearchMapComponent } from './search-map/search-map.component';
import { DenounceComponent } from './denounce/denounce.component';
import { DocumentTypeComponent } from './document-type/document-type.component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { LandTypeComponent as LandTypeComponent } from './land-type/land-type.component';
import { UnitTypeComponent } from './unit-type/unit-type.component';
import { UnitComponent } from './unit/unit.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'map', component: SearchMapComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'complain/:linhVuc', component: ComplainComponent },
  { path: 'denounce/:linhVuc', component: DenounceComponent },
  {
    path: 'document-type',
    component: DocumentTypeComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'DocumentType',
    },
  },
  {
    path: 'land-type',
    component: LandTypeComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'LandType',
    },
  },
  {
    path: 'unit-type',
    component: UnitTypeComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'UnitType',
    },
  },
  {
    path: 'unit',
    component: UnitComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPolicy: 'Unit',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
