import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { ToolbarModule } from 'primeng/toolbar';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { CoreModule } from '@abp/ng.core';
import { CardModule } from 'primeng/card';
import { InputSwitchModule } from 'primeng/inputswitch';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { AccordionModule } from 'primeng/accordion';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ReportRoutingModule } from './report-routing.module';
import { ReportComponent } from './report.component';
import { LogBookComponent } from './logbook/logbook.component';
import { CommonModule } from '@angular/common';
@NgModule({
  declarations: [
    ReportComponent,
    LogBookComponent
  ],
    imports: [
    CommonModule,
    CardModule,
    ReportRoutingModule,
    CoreModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    CalendarModule,
    ToolbarModule,
    TieredMenuModule,
    InputSwitchModule,
    BreadcrumbModule,
    AccordionModule,
    SelectButtonModule
  ],
})
export class ReportModule {}
