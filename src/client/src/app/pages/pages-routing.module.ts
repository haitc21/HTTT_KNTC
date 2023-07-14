import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard, } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'map',
    loadChildren: () => import('./search-map/search-map.module').then(m => m.SearchMapModule),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashBoardModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'reports/report',
    loadChildren: () => import('./reports/report.module').then(m => m.ReportModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'complain/:linhVuc',
    loadChildren: () => import('./complain/complain.module').then(m => m.ComplainModule),
  },
  {
    path: 'denounce/:linhVuc',
    loadChildren: () => import('./denounce/denounce.module').then(m => m.DenounceModule),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
