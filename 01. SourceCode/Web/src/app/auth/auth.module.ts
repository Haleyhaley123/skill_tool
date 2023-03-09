import { ChangePasswordComponent } from './change-password/change-password.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AuthRoutingModule } from './auth-routing.routing';
import { LoginComponent } from './login/login.component';
import { BlockUIModule } from 'ng-block-ui';
import { SharedModule } from '../shared/shared.module';
import { LoginNewComponent } from './login-new/login-new.component';

@NgModule({
    declarations: [
        LoginComponent,
        ChangePasswordComponent,
        LoginNewComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        HttpClientModule,
        AuthRoutingModule,
        BlockUIModule,
        SharedModule
    ],
    providers: [
    ],
    entryComponents: [ChangePasswordComponent],
})

export class AuthModule {
}