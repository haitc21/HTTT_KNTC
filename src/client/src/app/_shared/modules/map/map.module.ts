import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { PanelModule } from 'primeng/panel';
import { MapComponent } from './map.component';
import { MapPopupComponent } from './map-popup/map-popup.component';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
@NgModule({
      imports: [
    CommonModule,
    ReactiveFormsModule,
    InputTextModule,
    CalendarModule,
    DropdownModule,
    PanelModule,
    BlockUIModule,
    ProgressSpinnerModule,
  ],
  declarations: [MapComponent, MapPopupComponent],
  exports: [MapComponent],
})
export class MapModule {}
