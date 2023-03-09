import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExtraOptions, Routes, RouterModule } from '@angular/router';

import { AuthGuard } from '../auth/guards/auth.guard';

import { LayoutComponent } from './layout.component';
import { ScreenWaitComponent } from './screen-wait/screen-wait.component';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('../auth/auth.module').then(m => m.AuthModule)
  },
  {
    path: '',
    canActivate: [AuthGuard],
    component: LayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '',
        pathMatch: 'full'
      },
      {
        path: 'page',
        canActivate: [AuthGuard],
        loadChildren: () => import('../page/page.module').then(m => m.PageModule)
      },
      {
        path: 'danh-muc',
        canActivate: [AuthGuard],
        loadChildren: () => import('../category/category.module').then(m => m.CategoryModule)
      },
      {
        path: 'nguoi-dung',
        canActivate: [AuthGuard],
        loadChildren: () => import('../user/user.module').then(m => m.UserModule)
      },
      {
        path: 'about',
        canActivate: [AuthGuard],
        loadChildren: () => import('../page/page.module').then(m => m.PageModule)
      },
      {
        path: 'pcmt&tp',
        component: ScreenWaitComponent
      }
    ]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})

export class LayoutRoutingModule { }