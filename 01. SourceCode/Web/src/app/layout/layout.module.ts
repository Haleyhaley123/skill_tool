import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { NgSelectModule } from '@ng-select/ng-select';


import { LayoutComponent } from './layout.component';
import { LayoutRoutingModule } from './layout-routing.routing';
import { SharedModule } from '../shared/shared.module';
import { TopBarComponent } from './top-bar/top-bar.component';
import { LeftBarComponent } from './left-bar/left-bar.component';
import { NavCollapsableComponent } from './collapsable/collapsable.component';
import { NavItemComponent } from './item/item.component';
import { NtsNavigationService } from './navigation/navigation.service';
import { ScreenWaitComponent } from './screen-wait/screen-wait.component';
import { NotifyModule } from '../notify/notify.module';
import { FooterComponent } from './footer/footer.component';
import { PageModule } from '../page/page.module';
import { ViewTabComponent } from './view-tab/view-tab.component';
import { PageToolbarComponent } from './page-toolbar/page-toolbar.component';

@NgModule({
    declarations: [
        LayoutComponent,
        TopBarComponent,
        LeftBarComponent,
        NavCollapsableComponent,
        NavItemComponent,
        ScreenWaitComponent,
        FooterComponent,
        ViewTabComponent,
        PageToolbarComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        LayoutRoutingModule,
        SharedModule,
        NgbModule,
        PerfectScrollbarModule,
        NotifyModule,
        PageModule,
        NgSelectModule
    ],
    providers: [
    ],
    entryComponents: [],
})

export class LayoutModule {
}