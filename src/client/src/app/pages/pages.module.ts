import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagesComponent } from './pages.component';
import { PagesRoutingModule } from './pages.routing';
import { AppLayoutModule } from '../layout/app.layout.module';
import { ComplainModule } from './complain/complain.module';
import { DenounceModule } from './denounce/denounce.module';

@NgModule({
  imports: [
    CommonModule,
    PagesRoutingModule,
    ComplainModule,
    DenounceModule
  ],
  declarations: [PagesComponent],
  entryComponents: [PagesComponent]
})
export class PagesModule { }
