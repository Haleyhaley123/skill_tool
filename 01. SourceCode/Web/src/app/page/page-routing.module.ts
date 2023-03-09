import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/guards/auth.guard';
import { AboutComponent } from './about/about.component';
import { SetupAboutComponent } from './setup-about/setup-about.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'gioi-thieu', component: AboutComponent, data: { animation: 'About' } },
      { path: 'setup-about', component: SetupAboutComponent, data: { animation: 'SetupAbout' } },
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PageRoutingModule { }
