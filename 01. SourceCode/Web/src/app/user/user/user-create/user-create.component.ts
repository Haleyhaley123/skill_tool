import { AfterViewInit, Component, ElementRef, OnDestroy, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FileProcess, AppSetting, MessageService, Constants, Configuration, DateUtils } from 'src/app/shared';
import { ComboboxService } from 'src/app/shared/services/combobox.service';
import { FileService } from 'src/app/shared/services/file.service';
import { UserService } from '../../service/user.service';

@Component({
  selector: 'app-user-create',
  templateUrl: './user-create.component.html',
  styleUrls: ['./user-create.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class UserCreateComponent implements OnInit, AfterViewInit, OnDestroy {

  constructor(
    public fileProcess: FileProcess,
    private routeA: ActivatedRoute,
    public appSetting: AppSetting,
    private messageService: MessageService,
    public constant: Constants,
    public config: Configuration,
    public dateUtils: DateUtils,
    private userService: UserService,
    private router: Router,
    private fileService: FileService,
    private comboboxService: ComboboxService
  ) { }

  height = 0;
  @ViewChild('scrollPracticeMaterial') scrollPracticeMaterial: ElementRef;
  @ViewChild('scrollPracticeMaterialHeader') scrollPracticeMaterialHeader: ElementRef;

  @ViewChild('scrollPermession', { static: false }) scrollPermession: ElementRef;
  @ViewChild('scrollPermessionHeader', { static: false }) scrollPermessionHeader: ElementRef;
  id: string;
  type: string;
  filedata = null;
  minDateNotificationV: NgbDateStruct;
  dateOfBirth = null;
  isAction: boolean = false;
  listFunction: any[] = [];
  listPermission: any[] = [];
  listUserGroup: any[] = [];
  groupFunctions: any[] = [];
  listChuVu: any[] = [];
  listDonVi: any[] =[];

  listManageUnit = [];

  isSelectAll = false;
  listFunctionIndex = 0;
  model: any = {
    userName: '',
    fullName: '',
    email: '',
    phoneNumber: '',
    anh: '',
    password: '',
    status: 1,
    description: '',
    isChecked: false,
    groupId: null,
    idChucVu: null,
    idDonVi: null,
  }


  modelGroupUser: any = {
    PageSize: 10,
    TotalItems: 0,
    PageNumber: 1,
    OrderBy: 'Name',
    OrderType: true,

    Id: '',
    Name: '',
    Status: '',
  }


  modelDelteFile: any = {
    anh: '',
  }

  isIndeterminate = false;
  groupSelectIndex = -1;

  ngOnInit(): void {
    this.fileProcess.fileModel = {};
    this.fileProcess.FileDataBase = null;
    this.id = this.routeA.snapshot.paramMap.get('id');
    this.height = window.innerHeight - 620;
    this.getListGroupuser();
    this.getListChucVu();
    this.getListDonVi();
    if (this.id != null) {
      this.appSetting.PageTitle = "Cập nhật tài khoản";
      this.getUserById();
    } else {
      this.appSetting.PageTitle = "Thêm mới tài khoản";
      this.getPermission();
    }
  }

  ngAfterViewInit() {
    this.scrollPracticeMaterial.nativeElement.addEventListener('ps-scroll-x', (event: any) => {
      this.scrollPracticeMaterialHeader.nativeElement.scrollLeft = event.target.scrollLeft;
    }, true);

    this.scrollPermession.nativeElement.addEventListener('ps-scroll-x', (event: any) => {
      this.scrollPermessionHeader.nativeElement.scrollLeft = event.target.scrollLeft;
    }, true);
  }

  ngOnDestroy() {
    this.scrollPracticeMaterial.nativeElement.removeEventListener('ps-scroll-x', null);
    this.scrollPermession.nativeElement.removeEventListener('ps-scroll-x', null);
  }

  listData = [];
  pathFile: string;

  getListGroupuser() {
    this.comboboxService.getListGroupuser().subscribe(
      (data: any) => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.listUserGroup = data.data;
        }
        else {
          this.messageService.showListMessage(data.message);
        }
      }
    );
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

  getUserById() {
    this.userService.getUserById(this.id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.model = data.data;

          if (data.data.anh != null && data.data.anh != '') {
            this.filedata = this.config.ServerApi + data.data.anh;
          }

          this.groupFunctions = data.data.listGroupFunction;
          if (this.groupFunctions.length > 0) {
            this.selectGroupFunction(this.groupFunctions[0], 0);
          }
          this.groupSelectIndex = 0;
          for (let i = 0; i < this.groupFunctions.length; i++) {
            let checkCount = this.groupFunctions[i].checkCount;
            let length = this.groupFunctions[i].permissions.length;
            if (checkCount == 0) {
              this.groupFunctions[i].isIndeterminate = false;
            }
            else {
              if (checkCount < length) {
                this.groupFunctions[i].isIndeterminate = true;
              }
              else {
                this.groupFunctions[i].isIndeterminate = false;
                this.isSelectAll = this.groupFunctions[i].checkCount == this.groupFunctions[i].permissions.length;
                this.groupFunctions[i].isChecked = this.isSelectAll;
              }
            }
          }

          // if (this.model.groupId) {
          //   this.groupFunctions = this.groupFunctions.filter(i => i.checkCount && i.checkCount > 0);
          // }
        }
        else {
          this.messageService.showListMessage(data.Message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  getGroupPermission() {
    this.userService.getGroupPermission(this.id).subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.groupFunctions = data.data;
          this.groupSelectIndex = 0;
          for (let i = 0; i < this.groupFunctions.length; i++) {
            let checkCount = this.groupFunctions[i].checkCount;
            let length = this.groupFunctions[i].permissions.length;
            if (checkCount == 0) {
              this.groupFunctions[i].isIndeterminate = false;
            }
            else {
              if (checkCount < length) {
                this.groupFunctions[i].isIndeterminate = true;
              }
              else {
                this.groupFunctions[i].isIndeterminate = false;
                this.isSelectAll = this.groupFunctions[i].checkCount == this.groupFunctions[i].permissions.length;
                this.groupFunctions[i].isChecked = this.isSelectAll;
              }
            }
          }
        }
        else {
          this.messageService.showMessage(data.message);
        }
      }
    )
  }

  getPermission() {
    this.userService.getPermission().subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.groupFunctions = data.data;
          if (this.groupFunctions.length > 0) {
            this.selectGroupFunction(this.groupFunctions[0], 0);
          }

          this.groupSelectIndex = 0;
          for (let i = 0; i < this.groupFunctions.length; i++) {
            let checkCount = this.groupFunctions[i].checkCount;
            let length = this.groupFunctions[i].permissions.length;
            if (checkCount == 0) {
              this.groupFunctions[i].isIndeterminate = false;
            }
            else {
              if (checkCount < length) {
                this.groupFunctions[i].isIndeterminate = true;
              }
              else {
                this.groupFunctions[i].isIndeterminate = false;
                this.isSelectAll = this.groupFunctions[i].checkCount == this.groupFunctions[i].permissions.length;
                this.groupFunctions[i].isChecked = this.isSelectAll;
              }
            }
          }
        }
        else {
          this.messageService.showListMessage(data.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  listFunctions: number;
  selectGroupFunction(group, index) {
    this.groupSelectIndex = index;
    this.isSelectAll = this.groupFunctions[index].checkCount == this.groupFunctions[index].permissions.length;
    this.listFunctions = this.groupFunctions[index].permissions.length;
  }

  changeGroupFunctionCheck(group, index) {
    group.permissions.forEach(permission => {
      if (permission.isChecked && !group.isChecked) {
        group.checkCount--;
      }
      if (!permission.isChecked && group.isChecked) {
        group.checkCount++;
      }
      permission.isChecked = group.isChecked;
    });

    if (index == this.groupSelectIndex) {
      this.isSelectAll = group.isChecked;
    }
    this.groupFunctions[index].isIndeterminate = false;
  }

  selectAllPermission() {
    this.groupFunctions[this.groupSelectIndex].permissions.forEach(permission => {
      if (permission.isChecked && !this.isSelectAll) {
        this.groupFunctions[this.groupSelectIndex].checkCount--;
      }
      if (!permission.isChecked && this.isSelectAll) {
        this.groupFunctions[this.groupSelectIndex].checkCount++;
      }
      permission.isChecked = this.isSelectAll;
    });
    this.groupFunctions[this.groupSelectIndex].isChecked = this.isSelectAll;
    this.groupFunctions[this.groupSelectIndex].isIndeterminate = false;
  }

  selectPermission(permission) {
    if (!permission.isChecked) {
      this.groupFunctions[this.groupSelectIndex].checkCount--;
    }
    else {
      this.groupFunctions[this.groupSelectIndex].checkCount++;
    }

    let checkCount = this.groupFunctions[this.groupSelectIndex].checkCount;
    let length = this.groupFunctions[this.groupSelectIndex].permissions.length;
    if (checkCount == 0) {
      this.groupFunctions[this.groupSelectIndex].isChecked = false;
      this.groupFunctions[this.groupSelectIndex].isIndeterminate = false;
      this.isSelectAll = false;
    }
    else {
      if (checkCount < length) {
        this.groupFunctions[this.groupSelectIndex].isIndeterminate = true;
        this.isSelectAll = false;
      }
      else {
        this.groupFunctions[this.groupSelectIndex].isIndeterminate = false;
        this.isSelectAll = this.groupFunctions[this.groupSelectIndex].checkCount == this.groupFunctions[this.groupSelectIndex].permissions.length;
        this.groupFunctions[this.groupSelectIndex].isChecked = this.isSelectAll;
      }
    }

  }

  changeGroupUser() {
    if (this.model.groupId) {
      this.isSelectAll = false;
      this.userService.getGroupPermissionById(this.model.groupId).subscribe(
        data => {
          this.groupFunctions.forEach(group => {
            group.isChecked = false;
            group.checkCount = 0;
            group.permissions.forEach(permission => {
              permission.isChecked = false;
              data.data.forEach(groupF => {
                if (groupF.id == permission.id) {
                  permission.isChecked = true;
                  group.checkCount++;
                }
              });
            });
            let checkCount = group.checkCount;
            let length = group.permissions.length;
            group.isChecked = group.checkCount == group.permissions.length;
            if (checkCount > 0 && checkCount < length) {
              group.isIndeterminate = true;
            }
            else {
              group.isIndeterminate = false;
            }
          });

          this.groupFunctions = this.groupFunctions.filter(i => i.checkCount && i.checkCount > 0);
          this.isSelectAll = this.groupFunctions[this.groupSelectIndex].checkCount == this.groupFunctions[this.groupSelectIndex].permissions.length;
        }, error => {
          this.messageService.showError(error);
        });
    } else {
      this.getPermission();
    }
  }

  onFileChange($event) {
    this.fileProcess.onAFileChange($event);
  }

  saveAndContinue() {
    this.save(true);
  }

  save(isContinue: boolean) {
    let regex = this.constant.validEmailRegEx;

    if (this.model.email) {
      if (!regex.test(this.model.email)) {

        this.messageService.showMessage("E-mail không hợp lệ!");
        return;
      }
    }

    this.model.listGroupFunction = this.groupFunctions;

    if (this.fileProcess.FileDataBase == null) {
      if (this.id) {
        this.update();
      }
      else {
        this.create(isContinue);
      }
    } else if (this.fileProcess.FileDataBase) {
      this.fileService.uploadFile(this.fileProcess.FileDataBase, 'User/Image').subscribe(
        result => {
          this.model.anh = result.data.fileUrl;
          if (this.id) {
            this.update();
          }
          else {
            this.create(isContinue);
          }
        },
        error => {
          this.messageService.showError(error);
        });
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
    this.userService.updateUser(this.id, this.model).subscribe(
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

  clear() {
    this.fileProcess.fileModel = {};
    this.fileProcess.FileDataBase = null;

    this.model = {
      userName: '',
      fullName: '',
      email: '',
      phoneNumber: '',
      anh: '',
      password: '',
      status: 1,
      description: '',
      isChecked: false,
      groupId: null,
      idChucVu: null,
    }
  }

  showComfirmDeleteFile() {
    this.messageService.showConfirm("Bạn có chắc muốn xóa ảnh này không?").then(
      data => {
        this.model.anh = null;
        this.filedata = null;
        this.fileProcess.fileModel = {};
        this.fileProcess.FileDataBase = null;
      }
    );
  }

  // deleteFileError() {
  //   this.modelDelteFile.anh = this.model.anh;
  //   this.fileService.deleteFile(this.modelDelteFile).subscribe(
  //     data => {
  //       if (data.statusCode == this.constant.StatusCode.Success) {
  //       }
  //       else {
  //         this.messageService.showMessage(data.message);
  //       }
  //     },
  //     error => {
  //       this.messageService.showError(error);
  //     }
  //   );
  // }

  deleteFile(anh: any) {
    this.modelDelteFile.anh = anh;
    if (this.modelDelteFile.anh != null && this.modelDelteFile.anh != '') {
      // this.fileService.deleteFile(anh).subscribe(
      //   data => {
      //     if (data.statusCode == this.constant.StatusCode.Success) {
      //       this.model.anh = null;
      //       this.update();
      //     }
      //     else {
      //       this.messageService.showMessage(data.message);
      //     }
      //   }
      //);
    }
    if (this.fileProcess.FileDataBase != null && anh == null) {
      this.filedata = null;
      this.fileProcess.fileModel.DataURL = null;
    }

  }

  closeModal(isOK: boolean) {
    if (this.fileProcess.fileModel.DataURL != undefined) {
      this.fileProcess.fileModel.DataURL = null;
    }
    this.router.navigate(['/nguoi-dung/tai-khoan']);
  }

  fieldTextType: boolean;
  toggleFieldTextType() {
    this.fieldTextType = !this.fieldTextType;
  }

  getListDonVi() {
    this.comboboxService.getListDonVi().subscribe(
      (data: any) => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.listDonVi = data.data;
        }
        else {
          this.messageService.showListMessage(data.message);
        }
      }
    );
  }

}
