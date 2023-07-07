import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { CoreModule } from '@abp/ng.core';
import { CardModule } from 'primeng/card';
import { InputSwitchModule } from 'primeng/inputswitch';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { MapModule } from 'src/app/shared/modules/map/map.module';
@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    HomeRoutingModule,
    CoreModule,
    PanelModule,
    CardModule,
    InputSwitchModule,
    BreadcrumbModule,
    MapModule
  ],
  exports: [HomeComponent],
})
export class HomeModule {}
