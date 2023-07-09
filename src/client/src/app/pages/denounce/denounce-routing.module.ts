import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DenounceComponent } from './denounce.component';

const routes: Routes = [
  { path: '', component: DenounceComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DenounceRoutingModule {}
