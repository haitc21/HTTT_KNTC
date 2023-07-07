import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { ComplainComponent } from './complain/complain.component';
import { SearchMapComponent } from './search-map/search-map.component';
import { DenounceComponent } from './denounce/denounce.component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';
import { ReportComponent } from './reports/report.component';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  { path: 'map', component: SearchMapComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'reports/logbook', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'reports/report', component: ReportComponent, canActivate: [AuthGuard] },
  { path: 'complain/:linhVuc', component: ComplainComponent },
  { path: 'denounce/:linhVuc', component: DenounceComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
