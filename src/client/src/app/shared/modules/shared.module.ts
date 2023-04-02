import { CoreModule } from '@abp/ng.core';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ValidationMessageComponent } from './validation-message/validation-message.component';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { MapComponent } from './map/map.component';
import { MapPopupComponent } from './map-popup/map-popup.component';
import { InputTextModule } from 'primeng/inputtext';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { PanelModule } from 'primeng/panel';

@NgModule({
  imports: [
    CoreModule,
    CommonModule,
    MessagesModule,
    MessageModule,
    InputTextModule,
    CalendarModule,
    DropdownModule,
    PanelModule,
  ],
  declarations: [ValidationMessageComponent, MapComponent, MapPopupComponent],
  exports: [ValidationMessageComponent, MapComponent, MapPopupComponent],
})
export class KNTCSharedModule {}
