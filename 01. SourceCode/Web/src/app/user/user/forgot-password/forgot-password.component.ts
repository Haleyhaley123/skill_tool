import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AuthenticationService } from 'src/app/auth/services';
import { Constants, Configuration, FileProcess, MessageService, ComboboxService } from 'src/app/shared';
import { FileService } from 'src/app/shared/services/file.service';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  constructor(
    public constant: Constants,
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private authenticationService: AuthenticationService,
    private comboboxService: ComboboxService
  ) { }

  modalInfo = {
    Title: '',
    SaveText: ''
  };

  email: string;
  otp: string;
  step = 1;

  ngOnInit(): void {
    this.modalInfo.Title = "Quên mật khẩu";
    if (this.step = 1) {
      this.modalInfo.SaveText = "Lấy mã";
    } else {
      this.modalInfo.SaveText = "Lấy mật khẩu";
    }
  }

  getOTP() {
    this.authenticationService.getOTP(this.email).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          if (result.data) {
            this.messageService.showSuccess("Mã OTP đã được gửi vào email của bạn!");
            this.step = 2;
          }
        }
        else {
          this.messageService.showListMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  forgotPassword() {
    this.authenticationService.forgotPassword(this.email, this.otp).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          if (result.data) {
            this.messageService.showSuccess("Mật khẩu mới đã được gửi vào email của bạn!");
            this.closeModal();
          }
        }
        else {
          this.messageService.showListMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  closeModal() {
    this.activeModal.close();
  }
}
