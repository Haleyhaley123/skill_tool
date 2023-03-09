import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

import { AuthenticationService } from '../services';
import { Constants, MessageService } from 'src/app/shared';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ForgotPasswordComponent } from 'src/app/user/user/forgot-password/forgot-password.component';
declare var $: any;
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class LoginComponent implements OnInit {
  message: string = '';
  returnUrl: string = '';
  model: any = {
  }
  constructor(
    private router: Router,
    private titleservice: Title,
    private constant: Constants,
    private route: ActivatedRoute,
    private modalService: NgbModal,
    private messageService: MessageService,
    private authenticationService: AuthenticationService,
  ) { }

  ngOnInit() {
    $(".toggle-password").click(function () {

      $(this).toggleClass("fa-eye fa-eye-slash");
      var input = $($(this).attr("toggle"));
      if (input.attr("type") == "password") {
        input.attr("type", "text");
        return;
      } else {
        input.attr("type", "password");
        return;
      }
    });

    // reset login status
    localStorage.removeItem('pcmtCurrentUser');

    this.titleservice.setTitle("CHƯƠNG TRÌNH QUẢN LÝ DỮ LIỆU NGHIỆP VỤ PCMT & TP");
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    this.authenticationService.login(this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          // store user details and jwt token in local storage to keep user logged in between page refreshes
          result.data.LoginDate = new Date();
          localStorage.setItem('pcmtCurrentUser', JSON.stringify(result.data));
          this.router.navigate(['dtcb/dtcb-dia-ban']);
        }
        else if (result.message) {
          this.message = result.message;
        }
        else {
          this.message = 'Tài khoản hoặc mật khẩu không đúng';
        }
      },
      error => {
        if (error.status == 0 || error.status == 404) {
          this.message = "Không kết nối server";
        } else {
          this.message = error.error.error_description;
        }
      }
    );
  }

  forgotPassword() {
    let activeModal = this.modalService.open(ForgotPasswordComponent, { container: 'body', windowClass: 'forgot-password-model', backdrop: 'static' })
    activeModal.result.then((result: any) => {
      if (result) {
        //this.logout();
      }
    });
  }
}
