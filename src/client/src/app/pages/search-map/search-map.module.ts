import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { CoreModule } from '@abp/ng.core';
import { CardModule } from 'primeng/card';
import { InputSwitchModule } from 'primeng/inputswitch';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { SearchMapRoutingModule } from './search-map-routing.module';
import { MapModule } from 'src/app/_shared/modules/map/map.module';
import { ButtonModule } from 'primeng/button';
import { SearchMapComponent } from './search-map.component';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
@NgModule({
  declarations: [
    SearchMapComponent
  ],
  imports: [
    SearchMapRoutingModule,
    CoreModule,
    PanelModule,
    CardModule,
    InputSwitchModule,
    BreadcrumbModule,
    MapModule,
    ButtonModule,
    InputTextModule,
    CalendarModule,
    DropdownModule,
    TableModule,
    ToolbarModule
  ],
  exports: [SearchMapComponent],
})
export class SearchMapModule {}
