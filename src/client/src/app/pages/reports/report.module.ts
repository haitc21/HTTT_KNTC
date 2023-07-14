import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ToolbarModule } from 'primeng/toolbar';
import { TooltipModule } from 'primeng/tooltip'
import { TieredMenuModule } from 'primeng/tieredmenu';
import { CoreModule } from '@abp/ng.core';
import { CardModule } from 'primeng/card';
import { InputSwitchModule } from 'primeng/inputswitch';
import { FieldsetModule } from 'primeng/fieldset';
import { TabViewModule } from 'primeng/tabview';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { DividerModule } from 'primeng/divider';
import { AccordionModule } from 'primeng/accordion';
import { SelectButtonModule } from 'primeng/selectbutton';
import { MapModule } from 'src/app/shared/modules/map/map.module';
import { EditorModule } from 'primeng/editor';
import { FileAttachmentModule } from '../file-attachment/file-attachment.module';
import { ReportRoutingModule } from './report-routing.module';
import { ReportComponent } from './report.component';
@NgModule({
  declarations: [
    ReportComponent
  ],
  imports: [
    ReportRoutingModule,
    CoreModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    MapModule,
    CalendarModule,
    KeyFilterModule,
    ToolbarModule,
    TieredMenuModule,
    CardModule,
    InputSwitchModule,
    FieldsetModule,
    TabViewModule,
    BreadcrumbModule,
    DividerModule,
    AccordionModule,
    SelectButtonModule,
    FileAttachmentModule
  ],
})
export class ReportModule {}
