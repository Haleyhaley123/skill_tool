import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Constants, MessageService, FileProcess, Configuration, ComboboxService } from 'src/app/shared';
import { FileService } from 'src/app/shared/services/file.service';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './user-info.component.html',
  styleUrls: ['./user-info.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserInfoComponent implements OnInit {

  constructor(
    public constant: Constants,
    public config: Configuration,
    public fileProcess: FileProcess,
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private fileService: FileService,
    private userService: UserService,
    private comboboxService: ComboboxService
  ) { }

  modalInfo = {
    Title: '',
    SaveText: ''
  };

  model: any = {
    id: '',
    userName: '',
    fullName: '',
    email: '',
    phoneNumber: '',
    imageLink: '',
    password: '',
    status: 1,
    description: '',
    isChecked: false,
    idChucVu: ''
  }

  id: any;
  isAction: boolean = false;
  listChuVu: any[] = [];
  filedata = null;
  isCheck = "";

  modelDelteFile: any = {
    avatar: '',
  }

  ngOnInit(): void {
    this.fileProcess.fileModel = {};
    this.fileProcess.FileDataBase = null;
    this.getListChucVu();
    if (this.id) {
      this.modalInfo.Title = "Cập nhật tài khoản";
      this.modalInfo.SaveText = 'Lưu';
      this.getUserInfo();
    }
    else {
      this.modalInfo.Title = "Thêm mới tài khoản";
      this.modalInfo.SaveText = "Lưu";
    }
  }

  getListChucVu() {
    this.comboboxService.getListChucVu().subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.listChuVu = result.data;
        }
        else {
          this.messageService.showListMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  onFileChange($event: any) {
    this.fileProcess.onAFileChange($event);
  }

  showComfirmDeleteFile() {
    this.messageService.showConfirm("Bạn có chắc muốn xóa ảnh này không?").then(
      data => {
        this.model.imageLink = null;
        this.filedata = null;
        this.fileProcess.fileModel = {};
        this.fileProcess.FileDataBase = null;
      }
    );
  }

  getUserInfo() {
    this.userService.getUserInfo(this.id).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.model = result.data;
          this.isCheck = this.model.idChucVu;

          if (result.data.anh != null && result.data.anh != '') {
            this.filedata = this.config.ServerApi + result.data.anh;
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

  saveAndContinue() {
    this.save(true);
  }

  save(isContinue: any) {
    if (this.fileProcess.FileDataBase == null) {
      this.saveData(isContinue);
    } else if (this.fileProcess.FileDataBase) {
      this.fileService.uploadFile(this.fileProcess.FileDataBase, 'User/Image').subscribe(
        result => {
          this.model.imageLink = result.data.fileUrl;
          this.saveData(isContinue);
        },
        error => {
          this.messageService.showError(error);
        });
    } else {
      this.saveData(isContinue);
    }
  }

  saveData(isContinue: any) {
    if (this.id) {
      this.update();
    }
    else {
      this.create(isContinue);
    }
  }

  create(isContinue: any) {
    this.userService.createUser(this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Thêm mới tài khoản thành công!');
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
      }
    );
  }

  update() {
    this.userService.updateUserInfo(this.id, this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Cập nhập tài khoản thành công!');
          this.closeModal(true);
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

  closeModal(isOK: boolean) {
    this.activeModal.close(isOK ? isOK : this.isAction);
  }

  clear() {
    this.fileProcess.fileModel = {};
    this.fileProcess.FileDataBase = null;
    this.model = {
      id: '',
      userName: '',
      fullName: '',
      email: '',
      phoneNumber: '',
      imageLink: '',
      password: '',
      status: 1,
      description: '',
      isChecked: false,
    };
  }

}
