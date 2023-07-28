import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ComplainComponent } from './complain.component';

const routes: Routes = [{ path: '', component: ComplainComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ComplainRoutingModule {}
