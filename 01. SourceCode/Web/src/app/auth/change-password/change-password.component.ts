import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../services';
import { Constants, MessageService } from '../../shared';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { state } from '@angular/animations';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  private message: string = '';
  returnUrl: string = '';
  OldPassword: any;
  ConfirmOldPassword: any;
  model: any = {
    Id: '',
    OldPassword: '',
    NewPassword: '',
    ConfirmPassword: '',

  }
  constructor(
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private router: Router,
    private constant: Constants,
    private messageService: MessageService,
    private activeModal: NgbActiveModal,
  ) { }

  ngOnInit() {
    let user = localStorage.getItem('pcmtCurrentUser');
    if (user) {
      this.model.id = JSON.parse(user).userid;
    }
  }

  closeModal(isOK: boolean) {
    this.activeModal.close(true);
  }

  ConfirmChangePassword() {
    this.messageService.showConfirm("Bạn có chắc muốn thay đổi mật khẩu không?").then(
      data => {
        this.ChangePassword();
      }
    );
  }

  ChangePassword() {
    let user = localStorage.getItem('pcmtCurrentUser');
    if (user) {
      this.model.id = JSON.parse(user).userid;
    }
    this.authenticationService.ChangePassword(this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Thay đổi mật khẩu thành công!');
          this.closeModal(true);
          this.authenticationService.logout();
          this.router.navigate(['/auth/dang-nhap']);
        } else if (result.statusCode == this.constant.StatusCode.Validate) {
          this.messageService.showListMessage(result.data);
        } else {
          this.messageService.showMessage(result.message);
        }
      },
      error => {
        this.messageService.showError(error);
      }
    );
  }
}
