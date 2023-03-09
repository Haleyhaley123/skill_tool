import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Constants, AppSetting, MessageService, Configuration, ComboboxService } from 'src/app/shared';
import { SearchGlobalService } from 'src/app/shared/common/search-global.service';
import { MenuOptions } from 'src/app/shared/models';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'app-user-manage',
  templateUrl: './user-manage.component.html',
  styleUrls: ['./user-manage.component.scss']
})
export class UserManageComponent implements OnInit {

  constructor(
    public constant: Constants,
    public appSetting: AppSetting,
    private userService: UserService,
    private messageService: MessageService,
    public config: Configuration,
    private router: Router,
    private searchGlobalService: SearchGlobalService,
    private comboboxService: ComboboxService,

  ) {
    this._unsubscribeAll = new Subject();
  }

  users: any[] = [];
  startIndex = 1;
  _unsubscribeAll: Subject<any>;
  searchModel: any = {
    pageSize: 10,
    totalItems: 0,
    pageNumber: 1,
    userName: '',
    fullName: '',
    status: 1,
    idDonVi: null
  }

  user: any;
  userId: string;
  userName = false;
  ngOnInit(): void {
    this.user = JSON.parse(localStorage.getItem('PCMTCurrentUser'));
    if (this.user) {
      this.userId = this.user.userId;
    }

    this.appSetting.PageTitle = "Quản lý tài khoản";
    this.searchGlobalService.onDataChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      if (data) {
        this.searchModel.userName = data.userName;
        this.searchModel.status = data.status;
        this.searchModel.pageNumber = data.pageNumber;
        this.searchModel.idDonVi = data.idDonVi;


        this.search();
      }
    });

    this.setToolbarConfig();
  }

  ngOnDestroy(): void {
    // Unsubscribe from all subscriptions
    this.searchGlobalService.setConfig(null);
    this._unsubscribeAll.next();
    this._unsubscribeAll.complete();
  }

  setToolbarConfig() {
    let meuOptions: MenuOptions = {
      isExcel: false,
      isPDF: false,
      isSearch: true,
      searchModel: this.searchModel,
      searchOptions: {
        FieldContentName: 'userName',
        Placeholder: 'Tìm kiếm tên tài khoản',
        Items: [
          {
            FieldName: 'status',
            Name: 'Tình trạng',
            Type: 'ngselect',
            DisplayName: 'Name',
            ValueName: 'Id',
            Data: this.constant.User_Status
          },
          // {
          //   FieldName: 'idDonVi',
          //   Name: 'Đơn vị',
          //   Type: 'ngselect',
          //   DisplayName: 'name',
          //   ValueName: 'id',
          //   GetData: (): Observable<any> => {
          //     return this.comboboxService.getListDonVi();
          //   }
          // }
        ]
      }
    };

    this.searchGlobalService.setConfig(meuOptions);
  }

  search() {
    this.userService.searchUser(this.searchModel).subscribe(
      (data: any) => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.startIndex = ((this.searchModel.pageNumber - 1) * this.searchModel.pageSize + 1);
          this.users = data.data.dataResults;
          this.searchModel.totalItems = data.data.totalItems;
        }
        else {
          this.messageService.showListMessage(data.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  showUpdate(id: string) {
    this.router.navigate(['/nguoi-dung/tai-khoan/chinh-sua/' + id]);
  }

  showCreate() {
    this.router.navigate(['/nguoi-dung/tai-khoan/them-moi']);
  }

  showViewUser(id: string) {
    this.router.navigate(['/nguoi-dung/tai-khoan/xem-tai-khoan/' + id]);
  }

  showConfirmDelete(id: string) {
    this.messageService.showConfirm("Bạn có chắc muốn xoá tài khoản này không?").then(
      data => {
        this.deleteUser(id);
      }
    );
  }

  deleteUser(id: string) {
    this.userService.deleteUser(id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Xóa tài khoản thành công!');
          this.search();
        }
        else {
          this.messageService.showMessage(data.message);
        }
      },
      error => {
        this.messageService.showError(error);
      });
  }


  showConfirmLockUser(id: string) {
    this.messageService.showConfirm("Bạn có chắc muốn khóa tài khoản này không?").then(
      data => {
        this.lockUser(id);
      }
    );
  }

  lockUser(id: string) {
    this.userService.userAdminLock(id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.search();
          this.messageService.showSuccess('Khóa tài khoản thành công!');
        }
        else {
          this.messageService.showMessage(data.message);
        }
      },
      error => {
        this.messageService.showError(error);
      });
  }

  showConfirmUnLockUser(id: string) {
    this.messageService.showConfirm("Bạn có chắc muốn mở khóa tài khoản này không?").then(
      data => {
        this.unLockUser(id);
      }
    );
  }

  unLockUser(id: string) {
    this.userService.userAdminUnLock(id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.search();
          this.messageService.showSuccess('Mở khóa tài khoản thành công!');
        }
        else {
          this.messageService.showMessage(data.message);
        }
      },
      error => {
        this.messageService.showError(error);
      });
  }

  showConfirmResetPassword(id: string) {
    this.messageService.showConfirm("Bạn có chắc muốn đặt lại mật khẩu cho tài khoản này không?").then(
      data => {
        this.resetPassword(id);
      }
    );
  }

  resetPassword(id: string) {
    this.userService.resetPassword(id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Đặt lại mật khẩu thành công!');
          this.search();
        }
        else {
          this.messageService.showMessage(data.message);
        }
      },
      error => {
        this.messageService.showError(error);
      });
  }

  // showRefreshPassword(id: string) {
  //   let activeModal = this.modalService.open(RefreshPasswordComponent, { container: 'body', windowClass: 'refresh-password-model', backdrop: 'static' })
  //   activeModal.componentInstance.id = id;
  //   activeModal.result.then((result: any) => {
  //     if (result) {
  //       this.searchUser();
  //     }
  //   });
  // }

  clear() {
    this.searchModel = {
      pageSize: 10,
      totalItems: 0,
      pageNumber: 1,
      userName: '',
      fullName: '',
      status: 1,
    }
    this.search();
  }

}
