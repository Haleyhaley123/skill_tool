import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../auth/guards/auth.guard';
import { CategoryManageComponent } from './category/category-manage/category-manage.component';

const routes: Routes = [
  {
    path: 'tat-ca-danh-muc',
    canActivate: [AuthGuard],
    component: CategoryManageComponent, data: { animation: 'CategoryManage' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }
