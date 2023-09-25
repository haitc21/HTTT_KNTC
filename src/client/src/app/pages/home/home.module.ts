import { NgModule } from '@angular/core';
import { PanelModule } from 'primeng/panel';
import { CardModule } from 'primeng/card';
import { InputSwitchModule } from 'primeng/inputswitch';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { ListboxModule } from 'primeng/listbox';
import { HomeComponent } from './home.component';
import { HomeRoutingModule } from './home-routing.module';
import { MapModule } from 'src/app/_shared/modules/map/map.module';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
@NgModule({
  declarations: [
    HomeComponent
  ],
      imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HomeRoutingModule,
    PanelModule,
    ListboxModule,
    CardModule,
    InputSwitchModule,
    BreadcrumbModule,
    MapModule,
    ButtonModule
  ],
  exports: [HomeComponent],
})
export class HomeModule {}
