import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryManageComponent } from './category/category-manage/category-manage.component';
import { CategoryCreateComponent } from './category/category-create/category-create.component';
import { FormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgSelectModule } from '@ng-select/ng-select';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { SharedModule } from '../shared/shared.module';
import { TreeGridModule } from '@syncfusion/ej2-angular-treegrid';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { CurrencyMaskModule } from 'ng2-currency-mask';

@NgModule({
  declarations: [
    CategoryManageComponent,
    CategoryCreateComponent,
  ],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    NgbModule,
    PerfectScrollbarModule,
    NgSelectModule,
    SharedModule,
    FormsModule,
    TreeGridModule,
    DragDropModule,
    CurrencyMaskModule
  ]
})
export class CategoryModule { }