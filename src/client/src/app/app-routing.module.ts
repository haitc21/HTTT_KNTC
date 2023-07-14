import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './layout/app.layout.component';
import { AuthGuard } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule),
    component: AppLayoutComponent,
  },
  {
    path: 'system',
    loadChildren: () => import('./system/system.module').then(m => m.SystemModule),
    component: AppLayoutComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'auth',
    loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule),
  },
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
