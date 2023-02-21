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
import { LandComplaintComponent } from './land-complaint/land-complaint.component';
import { LandAccusationComponent } from './land-accusation/land-accusation.component';
import { EnviromentalAccusationComponent } from './enviromental-accusation/enviromental-accusation.component';
import { MineralResourceAccusationComponent } from './mineral-resource-accusation/mineral-resource-accusation.component';
import { EnviromentalComplaintComponent } from './enviromental-complaint/enviromental-complaint.component';
import { MineralResourceComplaintComponent } from './mineral-resource-complaint/mineral-resource-complaint.component';
import { WaterResourceComplaintComponent } from './water-resource-complaint/water-resource-complaint.component';
import { WaterResourcAccusationComponent } from './water-resource-accusation/water-resource-accusation.component';

@NgModule({
  declarations: [
    HomeComponent,
    SearchMapComponent,
    DashboardComponent,
    LandComplaintComponent,
    LandAccusationComponent,
    EnviromentalComplaintComponent,
    EnviromentalAccusationComponent,
    MineralResourceComplaintComponent,
    MineralResourceAccusationComponent,
    WaterResourceComplaintComponent,
    WaterResourcAccusationComponent
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
  ],
  exports: [HomeComponent],
})
export class PagesModule {}
