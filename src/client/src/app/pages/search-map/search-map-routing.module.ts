import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SearchMapComponent } from './search-map.component';

const routes: Routes = [
  { path: '', component: SearchMapComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SearchMapRoutingModule {}
