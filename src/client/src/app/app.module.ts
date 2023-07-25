import { CoreModule } from '@abp/ng.core';
import { registerLocale } from '@abp/ng.core/locale';
import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DialogService } from 'primeng/dynamicdialog';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppLayoutModule } from './layout/app.layout.module';
import { APP_ROUTE_PROVIDER } from './route.provider';
import { MessageService } from 'primeng/api';
import { NotificationService } from './_shared/services/notification.service';
import { UtilityService } from './_shared/services/utility.service';
import { ConfirmationService } from 'primeng/api';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from './_shared/interceptors/token.interceptor';
import { GlobalHttpInterceptorService } from './_shared/interceptors/error-handler.interceptor';

import { storeLocaleData } from '@abp/ng.core/locale';
import(`@/../@angular/common/locales/vi.mjs`).then(m => storeLocaleData(m.default, 'vi'));

import localeVi from '@angular/common/locales/vi';


import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PanelModule } from 'primeng/panel';

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    AppLayoutModule,
    CoreModule.forRoot({
      environment,
      registerLocaleFn: registerLocale(),
    }),
    ConfirmDialogModule,
    ToastModule,
    BlockUIModule,
    ProgressSpinnerModule,
    PanelModule
  ],
  declarations: [AppComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: GlobalHttpInterceptorService,
      multi: true,
    },
    APP_ROUTE_PROVIDER,
    DialogService,
    MessageService,
    NotificationService,
    UtilityService,
    ConfirmationService,
    { provide: LOCALE_ID, useValue: 'vi' },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
