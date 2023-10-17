import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesComponent } from './pages.component';
import { PagesRoutingModule } from './pages.routing';
import { AppLayoutModule } from '../layout/app.layout.module';

@NgModule({
  imports: [
    CommonModule,
    PagesRoutingModule
  ],
  declarations: [PagesComponent],
  entryComponents: [PagesComponent]
})
export class PagesModule { }
