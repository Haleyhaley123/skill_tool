import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

import { Observable, OperatorFunction, Subject } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { ChangePasswordComponent } from 'src/app/auth/change-password/change-password.component';
import { AuthenticationService } from 'src/app/auth/services';
import { NotifyService } from 'src/app/notify/services/notify.service';

import { AppSetting, Configuration, Constants, MessageService } from 'src/app/shared'
import { UserInfoComponent } from 'src/app/user/user/user-info/user-info.component';

@Component({
  selector: 'app-top-bar',
  templateUrl: './top-bar.component.html',
  styleUrls: ['./top-bar.component.scss']
})
export class TopBarComponent implements OnInit {

  constructor(
    public appSetting: AppSetting,
    private notifyService: NotifyService,
    private authenticationService: AuthenticationService,
    public config: Configuration,
    private router: Router,
    private constant: Constants,
    private modalService: NgbModal,
    private messageService: MessageService,
  ) {
  }

  fileTemplate = this.config.ServerApi + this.config.UrlUserManual;
  fullName: string;
  linkImage: string;
  account: string;
  user: any;

  ngOnInit(): void {
    //this.fullName = 'Administrator';
    let userString = localStorage.getItem('pcmtCurrentUser');
    if (userString) {
      this.user = JSON.parse(userString);
      this.fullName = this.user.name;
    }

  }

  menuChatToggle(type: string) {
    if (type == 'menu') {
      this.appSetting.MenuFolded = !this.appSetting.MenuFolded;
      this.appSetting.chatFolded = false;
    }
  }

  navToggle() {
    // this.appSetting.MenuFolded = !this.appSetting.MenuFolded;
  }

  colorConfig() {
    this.notifyService.showTheme(true);
  }

  logout() {
    this.authenticationService.logout().subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          localStorage.removeItem('pcmtCurrentUser');
          this.router.navigate(['/auth/dang-nhap']);
        }
        else {
          this.messageService.showListMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  fnChangePassword() {
    let activeModal = this.modalService.open(ChangePasswordComponent, { container: 'body' });
    activeModal.result.then((result) => {

    }, (reason) => {

    });
  }

  dowloadFile() {
    // var link = document.createElement('a');
    // link.setAttribute("type", "hidden");
    // link.href = this.fileTemplate;
    // link.download = 'Download.zip';
    // document.body.appendChild(link);
    // // link.focus();
    // link.click();
    // document.body.removeChild(link);

    var redirectWindow = window.open(this.fileTemplate, '_blank');
    redirectWindow.location;
  }

  showCreateUpdate() {
    let activeModal = this.modalService.open(UserInfoComponent, { container: 'body', windowClass: 'user-info-model', backdrop: 'static' })
    activeModal.componentInstance.id = this.user.userId;
    activeModal.result.then((result: any) => {
      if (result) {
        //this.logout();
      }
    });
  }
}
