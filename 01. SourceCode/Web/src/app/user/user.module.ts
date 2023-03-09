import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserManageComponent } from './user/user-manage/user-manage.component';
import { UserCreateComponent } from './user/user-create/user-create.component';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { SharedModule } from '../shared/shared.module';
import { GroupUserManageComponent } from './group-user/group-user-manage/group-user-manage.component';
import { GroupUserCreateComponent } from './group-user/group-user-create/group-user-create.component';
import { UserViewComponent } from './user/user-view/user-view.component';
import { UserInfoComponent } from './user/user-info/user-info.component';
import { UserHistoryComponent } from './user-history/user-history.component';
import { ForgotPasswordComponent } from './user/forgot-password/forgot-password.component';


@NgModule({
  declarations: [
    UserManageComponent,
    UserCreateComponent,
    GroupUserManageComponent,
    GroupUserCreateComponent,
    UserViewComponent,
    UserInfoComponent,
    UserHistoryComponent,
    ForgotPasswordComponent
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    NgbModule,
    PerfectScrollbarModule,
    NgSelectModule,
    SharedModule,
    FormsModule,
  ]
})
export class UserModule { }
