import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy{
  title = 'SKILLTOOOL WEB';
  constructor(
    // private signalRService: SignalRService,
  ) {

  }

  ngOnInit() {
    // this.signalRService.initSignalR("http://localhost:2712/signalr");
    // this.signalRService.startConnection();
  }

  ngOnDestroy() {
    // this.signalRService.disConnect();
  }
}
