import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { ButtonModule } from 'primeng/button';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PasswordModule } from 'primeng/password';
import { InputTextModule } from 'primeng/inputtext';
import { AuthService, CoreModule } from '@abp/ng.core';
import { MessageService } from 'primeng/api';
import { BlockUIModule } from 'primeng/blockui';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ValidationMessagedModule } from 'src/app/_shared/modules/validation-message/validation-message.module';
import { InputSwitchModule } from 'primeng/inputswitch';

@NgModule({
  imports: [
    CommonModule,
    LoginRoutingModule,
    ButtonModule,
    InputTextModule,
    FormsModule,
    PasswordModule,
    ReactiveFormsModule,
    BlockUIModule,
    ProgressSpinnerModule,
    CoreModule,
    ValidationMessagedModule,
    InputSwitchModule
  ],
  declarations: [LoginComponent],
  providers: [MessageService],
})
export class LoginModule {}
