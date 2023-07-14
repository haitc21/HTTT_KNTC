import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { CalendarModule } from 'primeng/calendar';
import { ToolbarModule } from 'primeng/toolbar';import { TooltipModule } from 'primeng/tooltip'
import { TieredMenuModule } from 'primeng/tieredmenu';
import { CoreModule } from '@abp/ng.core';
import { CardModule } from 'primeng/card';
import { InputSwitchModule } from 'primeng/inputswitch';
import { TabViewModule } from 'primeng/tabview';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { AccordionModule } from 'primeng/accordion';
import { SelectButtonModule } from 'primeng/selectbutton';
import { ComplainComponent } from './complain.component';
import { ComplainDetailComponent } from './detail/complain-detail.component';
import { ComplainRoutingModule } from './complain-routing.module';
import { ValidationMessagedModule } from 'src/app/_shared/modules/validation-message/validation-message.module';
import { MapModule } from 'src/app/_shared/modules/map/map.module';
import { EditorModule } from 'primeng/editor';
import { FileAttachmentModule } from '../file-attachment/file-attachment.module';
@NgModule({
  declarations: [
    ComplainComponent,
    ComplainDetailComponent,
  ],
  imports: [
    ComplainRoutingModule,
    CoreModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    InputNumberModule,
    InputTextareaModule,
    EditorModule,
    ValidationMessagedModule,
    MapModule,
    CalendarModule,
    ToolbarModule,
    TieredMenuModule,
    CardModule,
    InputSwitchModule,
    TabViewModule,
    BreadcrumbModule,
    AccordionModule,
    SelectButtonModule,
    FileAttachmentModule
  ],
})
export class ComplainModule {}
