import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, Subscription } from 'rxjs';
import { filter, takeUntil } from 'rxjs/operators';

import { AppSetting } from 'src/app/shared'
import { NtsNavigationService } from '../navigation/navigation.service';
import { navigation } from '../navigation/navigation';

@Component({
  selector: 'app-left-bar',
  templateUrl: './left-bar.component.html',
  styleUrls: ['./left-bar.component.scss']
})
export class LeftBarComponent implements OnInit, OnDestroy {

  constructor(public appSetting: AppSetting,
    private _ntsNavigationService: NtsNavigationService,
    private router: Router
  ) {
    // Set the private defaults
    this._unsubscribeAll = new Subject();

  }
  navigation: any;

  // Private
  private _unsubscribeAll: Subject<any>;

  ngOnInit(): void {

    this._ntsNavigationService.register('menu', navigation);
    this._ntsNavigationService.setCurrentNavigation('menu');

    if (this._ntsNavigationService.isCurrentNavigation()) {
      this.navigation = this._ntsNavigationService.getCurrentNavigation();
    }

    this._ntsNavigationService.onNavigationChanged
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(() => {
        this.navigation = this._ntsNavigationService.getCurrentNavigation();
      });
  }

  /**
     * On destroy
     */
  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }
}
