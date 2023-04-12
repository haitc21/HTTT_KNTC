import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { BlockUIModule } from 'primeng/blockui';
import { ButtonModule } from 'primeng/button';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { DynamicDialogModule } from 'primeng/dynamicdialog';
import { InputNumberModule } from 'primeng/inputnumber';
import { CheckboxModule } from 'primeng/checkbox';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { EditorModule } from 'primeng/editor';
import { BadgeModule } from 'primeng/badge';
import { ImageModule } from 'primeng/image';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CalendarModule } from 'primeng/calendar';
import { PickListModule } from 'primeng/picklist';
import { KeyFilterModule } from 'primeng/keyfilter';
import { ToolbarModule } from 'primeng/toolbar';
import { TagModule } from 'primeng/tag';
import { ListboxModule } from 'primeng/listbox';
import { TooltipModule } from 'primeng/tooltip';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { CoreModule } from '@abp/ng.core';
import { PasswordModule } from 'primeng/password';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { CardModule } from 'primeng/card';
import { HomeRoutingModule as PagesRoutingModule } from './pages-routing.module';
import { InputSwitchModule } from 'primeng/inputswitch';
import { HomeComponent } from './home/home.component';
import { SearchMapComponent } from './search-map/search-map.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ChartModule } from 'primeng/chart';
import { KNTCSharedModule } from '../shared/modules/shared.module';
import { FieldsetModule } from 'primeng/fieldset';
import { ComplainDetailComponent } from './complain/detail/complain-detail.component';
import { TabViewModule } from 'primeng/tabview';
import { FileAttachmentComponent } from './file-attachment/file-attachment.component';
import { FileAttachmentDetailComponent } from './file-attachment/detial/file-attachment-detail.component';
import { FileUploadModule } from 'primeng/fileupload';
import { DialogModule } from 'primeng/dialog';
import { ComplainComponent } from './complain/complain.component';
import { DenounceComponent } from './denounce/denounce.component';
import { DenounceDetailComponent } from './denounce/detail/denounce-detail.component';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { DividerModule } from 'primeng/divider';
import { AccordionModule } from 'primeng/accordion';
@NgModule({
  declarations: [
    HomeComponent,
    SearchMapComponent,
    DashboardComponent,
    ComplainComponent,
    ComplainDetailComponent,
    DenounceComponent,
    DenounceDetailComponent,
    FileAttachmentComponent,
    FileAttachmentDetailComponent
  ],
  imports: [
    PagesRoutingModule,
    CoreModule,
    PanelModule,
    TableModule,
    PaginatorModule,
    BlockUIModule,
    ButtonModule,
    DropdownModule,
    InputTextModule,
    ProgressSpinnerModule,
    DynamicDialogModule,
    InputNumberModule,
    CheckboxModule,
    InputTextareaModule,
    EditorModule,
    KNTCSharedModule,
    BadgeModule,
    ImageModule,
    ConfirmDialogModule,
    CalendarModule,
    PickListModule,
    KeyFilterModule,
    ToolbarModule,
    TagModule,
    ListboxModule,
    TooltipModule,
    TieredMenuModule,
    PasswordModule,
    OverlayPanelModule,
    CardModule,
    InputSwitchModule,
    ChartModule,
    FieldsetModule,
    TabViewModule,
    FileUploadModule,
    BreadcrumbModule,
    DividerModule,
    AccordionModule,
    DialogModule
  ],
  exports: [HomeComponent],
})
export class PagesModule {}
