import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { AppSetting, MessageService, Constants } from 'src/app/shared';
import { SearchGlobalService } from 'src/app/shared/common/search-global.service';
import { MenuOptions } from 'src/app/shared/models';
import { GroupUserService } from '../../service/group-user.service';
import { GroupUserCreateComponent } from '../group-user-create/group-user-create.component';

@Component({
  selector: 'app-group-user-manage',
  templateUrl: './group-user-manage.component.html',
  styleUrls: ['./group-user-manage.component.scss']
})
export class GroupUserManageComponent implements OnInit {

  constructor(
    public appSetting: AppSetting,
    private messageService: MessageService,
    private modalService: NgbModal,
    private serviceGroupUser: GroupUserService,
    public constant: Constants,
    private searchGlobalService: SearchGlobalService,
  ) { 
    this._unsubscribeAll = new Subject();
  }

  _unsubscribeAll: Subject<any>;
  searchModel: any = {
    pageSize: 10,
    totalItems: 0,
    pageNumber: 1,

    name: '',
    status: 1,
  }

  ngOnInit(): void {
    this.appSetting.PageTitle = "Quản lý nhóm người dùng";
    this.searchGlobalService.onDataChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      if (data) {
        this.searchModel.name = data.name;
        this.searchModel.status = data.status;
        this.searchModel.pageNumber = data.pageNumber;

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
        FieldContentName: 'name',
        Placeholder: 'Tìm kiếm tên nhóm',
        Items: [
          {
            FieldName: 'status',
            Name: 'Tình trạng',
            Type: 'ngselect',
            DisplayName: 'Name',
            ValueName: 'Id',
            Data: this.constant.User_Status
          }
        ]
      }
    };

    this.searchGlobalService.setConfig(meuOptions);
  }

  groups: any[] = [];
  startIndex = 0;
  search() {
    this.serviceGroupUser.searchGroupUser(this.searchModel).subscribe((data: any) => {
      if (data.statusCode == this.constant.StatusCode.Success) {
        this.startIndex = ((this.searchModel.pageNumber - 1) * this.searchModel.pageSize + 1);
        this.groups = data.data.dataResults;
        this.searchModel.totalItems = data.data.totalItems;
      }
      else {
        this.messageService.showError(data.message);
      }
    }, error => {
      this.messageService.showError(error);
    });
  }

  clear() {
    this.searchModel = {
      pageSize: 10,
      totalItems: 0,
      pageNumber: 1,

      name: '',
      status: 1,
    }
    this.search();
  }

  showConfirmDelete(Id: string) {
    this.messageService.showConfirm("Bạn có chắc muốn xoá nhóm người dùng này không?").then(
      data => {
        this.delete(Id);
      }
    );
  }

  delete(Id: string) {
    this.serviceGroupUser.deleteGroupUser(Id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Xóa nhóm người dùng thành công!');
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

  showCreateUpdate(id: string) {
    let activeModal = this.modalService.open(GroupUserCreateComponent, { container: 'body', windowClass: 'group-user-create', backdrop: 'static' })
    activeModal.componentInstance.id = id;
    activeModal.result.then((result) => {
      if (result) {
        this.search();
      }
    }, (reason) => {
    });
  }

}
