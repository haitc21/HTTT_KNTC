import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EnviromentalAccusationComponent } from './enviromental-accusation/enviromental-accusation.component';
import { EnviromentalComplaintComponent } from './enviromental-complaint/enviromental-complaint.component';
import { HomeComponent } from './home/home.component';
import { LandAccusationComponent } from './land-accusation/land-accusation.component';
import { LandComplainComponent } from './land-complain/land-complain.component';
import { MineralResourceAccusationComponent } from './mineral-resource-accusation/mineral-resource-accusation.component';
import { MineralResourceComplaintComponent } from './mineral-resource-complaint/mineral-resource-complaint.component';
import { SearchMapComponent } from './search-map/search-map.component';
import { WaterResourcAccusationComponent } from './water-resource-accusation/water-resource-accusation.component';
import { WaterResourceComplaintComponent } from './water-resource-complaint/water-resource-complaint.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'map', component: SearchMapComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'land-complain', component: LandComplainComponent },
  { path: 'land-accusation', component: LandAccusationComponent },
  { path: 'enviromental-complaint', component: EnviromentalComplaintComponent },
  { path: 'enviromental-accusation', component: EnviromentalAccusationComponent },
  { path: 'mineral-resource-complaint', component: MineralResourceComplaintComponent },
  { path: 'mineral-resource-accusation', component: MineralResourceAccusationComponent },
  { path: 'water-resource-complaint', component: WaterResourceComplaintComponent },
  { path: 'water-resource-accusation', component: WaterResourcAccusationComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class HomeRoutingModule {}
