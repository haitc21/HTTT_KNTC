import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import {PasswordModule} from 'primeng/password';

@NgModule({
    imports: [
        CommonModule,
        AuthRoutingModule,
        PasswordModule
    ]
})
export class AuthModule { }
