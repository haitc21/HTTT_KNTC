import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'map',
    loadChildren: () => import('./pages/search-map/search-map.module').then(m => m.SearchMapModule),
  },
  {
    path: 'dashboard',
    loadChildren: () => import('./pages/dashboard/dashboard.module').then(m => m.DashBoardModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'reports',
    loadChildren: () => import('./pages/reports/report.module').then(m => m.ReportModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'complain/:linhVuc',
    loadChildren: () => import('./pages/complain/complain.module').then(m => m.ComplainModule),
  },
  {
    path: 'denounce/:linhVuc',
    loadChildren: () => import('./pages/denounce/denounce.module').then(m => m.DenounceModule),
  },
  {
    path: 'system',
    loadChildren: () => import('./system/system.module').then(m => m.SystemModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
  },
  // login in angular
  { path: 'account/login', redirectTo: 'auth/login', pathMatch: 'full' },
  {
    path: '**',
    loadChildren: () => import('./auth/error/error.module').then(m => m.ErrorModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
