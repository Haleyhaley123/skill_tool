import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService, Constants } from 'src/app/shared';
import { ComboboxService } from 'src/app/shared/services/combobox.service';
import { GroupUserService } from '../../service/group-user.service';

@Component({
  selector: 'app-group-user-create',
  templateUrl: './group-user-create.component.html',
  styleUrls: ['./group-user-create.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class GroupUserCreateComponent implements OnInit {

  constructor(
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private service: GroupUserService,
    private combobox: ComboboxService,
    public constant: Constants
  ) { }

  modalInfo = {
    Title: 'Thêm mới nhóm người dùng',
    SaveText: 'Lưu',
  };

  isAction: boolean = false;
  id: string;
  checkeds = false;

  model: any = {
    id: '',
    name: '',
    status: 1,
    description: '',
    listPermission: []
  }

  ngOnInit(): void {
    if (this.id) {
      this.modalInfo.Title = 'Chỉnh sửa nhóm người dùng';
      this.modalInfo.SaveText = 'Lưu';
    }
    else {
      this.modalInfo.Title = "Thêm mới nhóm người dùng";
    }

    this.getGroupUserInfo();
  }

  getPermisstion() {
    this.service.getPermisstion().subscribe(data => {
      if (data.statusCode == this.constant.StatusCode.Success) {
        this.model.listPermission = data.data;
      }
      else {
        this.messageService.showError(data.message);
      }
    }, error => {
    });
  }

  getGroupUserInfo() {
    this.service.getGroupUserInfo(this.id).subscribe(data => {
      if (data.statusCode == this.constant.StatusCode.Success) {
        this.model = data.data;
      }
      else {
        this.messageService.showError(data.message);
      }
    }, error => {
    });
  }

  create(isContinue: any) {
    this.service.createGroupUser(this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Thêm mới nhóm người dùng thành công!');
          if (isContinue) {
            this.isAction = true;
            this.clear();
          } else {
            this.closeModal(true);
          }
        } else if (result.statusCode == this.constant.StatusCode.Validate) {
          this.messageService.showListMessage(result.data);
        } else {
          this.messageService.showMessage(result.message);
        }
      },
      error => {
        this.messageService.showError(error);
      });
  }

  update() {
    this.service.updateGroupUser(this.id, this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.activeModal.close(true);
          this.messageService.showSuccess('Cập nhật nhóm người dùng thành công!');
        }
        else if (result.statusCode == this.constant.StatusCode.Validate) {
          this.messageService.showListMessage(result.data);
        } else {
          this.messageService.showMessage(result.message);
        }
      },
      error => {
        this.messageService.showError(error);
      });
  }

  save(isContinue: boolean) {
    if (this.id) {
      this.update();
    } else {
      this.create(isContinue);
    }
  }

  saveAndContinue() {
    this.save(true);
  }

  closeModal(isOK: boolean) {
    this.activeModal.close(isOK ? isOK : this.isAction);
  }

  selectAllFunction() {
    if (this.checkeds) {
      this.model.listPermission.forEach(element => {
        element.checked = true;
      });
    } else {
      this.model.listPermission.forEach(element => {
        element.checked = false;
      });
    }
  }

  checkParent(groupFunctionId: any, checked: any, index: any) {
    if (!groupFunctionId) {
      if (checked) {
        for (let i = index + 1; i < this.model.listPermission.length; i++) {
          if (!this.model.listPermission[i].index) {
            this.model.listPermission[i].checked = true;
          } else break;
        }
      } else {
        for (let i = index + 1; i < this.model.listPermission.length; i++) {
          if (!this.model.listPermission[i].index) {
            this.model.listPermission[i].checked = false;
          } else break;
        }
      }
    } else {
      let groupChilds = this.model.listPermission.filter((i: { groupFunctionId: any; }) => i.groupFunctionId == groupFunctionId);
      let groupCheck = groupChilds.filter((i: { checked: boolean; }) => i.checked == false);
      if (groupCheck.length > 0) {
        for (let i = index - 1; i < this.model.listPermission.length; i++) {
          if (this.model.listPermission[i].functionId == groupFunctionId) {
            this.model.listPermission[i].checked = false;
          } else break;
        }
      } else {
        for (let i = index - 1; i < this.model.listPermission.length; i++) {
          if (this.model.listPermission[i].functionId == groupFunctionId) {
            this.model.listPermission[i].checked = true;
          } else break;
        }
      }

      let checkAll = this.model.listPermission.filter((i: { checked: boolean; }) => i.checked == false);
      if (checkAll.length > 0) {
        this.checkeds = false;
      } else {
        this.checkeds = true;
      }
    }
  }

  clear() {
    this.model = {
      id: '',
      name: '',
      status: 1,
      description: '',
      listPermission: []
    };

    this.getGroupUserInfo();
  }
}
