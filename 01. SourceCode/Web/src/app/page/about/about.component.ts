import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { AppSetting, Configuration, Constants, MessageService } from 'src/app/shared';
import { AboutService } from '../service/about.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class AboutComponent implements OnInit {

  constructor(
    public constant: Constants,
    private appSetting: AppSetting,
    private aboutService: AboutService,
    private messageService: MessageService
  ) { }

  height: number;
  model: any = {
    content: ''
  }
  ngOnInit(): void {
    this.appSetting.PageTitle = 'Giới thiệu';
    //this.height = window.innerHeight - 175;
    this.getAbout();
  }

  getAbout() {
    this.aboutService.getAbout().subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.model = data.data;
        }
        else {
          this.messageService.showListMessage(data.Message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }
}
