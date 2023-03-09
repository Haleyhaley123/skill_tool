import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Constants, AppSetting, MessageService } from 'src/app/shared';
import { ComboboxService } from 'src/app/shared/services/combobox.service';
import { CategoryService } from '../../service/category.service';

@Component({
  selector: 'app-category-create',
  templateUrl: './category-create.component.html',
  styleUrls: ['./category-create.component.scss']
})
export class CategoryCreateComponent implements OnInit {

  constructor(
    public constant: Constants,
    public appSetting: AppSetting,
    private activeModal: NgbActiveModal,
    private messageService: MessageService,
    private categoryService: CategoryService,
    private comboboxService: ComboboxService
  ) { }

  id: any;
  tableName: any;
  isAction: boolean = false;
  listOrder: any[] = [];
  listParent: any[] = [];
  modalInfo = {
    Title: 'Thêm mới danh mục',
  };

  categoryModel: any = {
    id: null,
    parentId: null,
    name: '',
    order: null,
    tableName: '',
  }

  ngOnInit(): void {
    this.categoryModel.tableName = this.tableName;
    this.getListHeLoai();
    this.getListOder();
    if (this.id) {
      this.modalInfo.Title = 'Cập nhật danh mục';
      this.getCategoryById();
    } else {
      this.modalInfo.Title = 'Thêm mới danh mục';
    }
  }

  getListHeLoai() {
    this.comboboxService.getListHeLoai().subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.listParent = result.data;
        }
        else {
          this.messageService.showMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  getListOder() {
    this.categoryService.getListOderTable(this.id, this.tableName).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.listOrder = result.data;
          if (!this.id) {
            this.categoryModel.order = this.listOrder[this.listOrder.length - 1];
          }
        }
        else {
          this.messageService.showMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  getCategoryById() {
    this.categoryService.getCategoryTableInfo(this.id, this.categoryModel.tableName).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.categoryModel = result.data;
        }
        else {
          this.messageService.showMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  saveAndContinue() {
    this.save(true);
  }

  save(isContinue: boolean) {
    if (this.id) {
      this.update();
    } else {
      this.create(isContinue);
    }
  }

  create(isContinue: any) {
    this.categoryService.createCategoryTable(this.categoryModel).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Thêm mới danh mục thành công!');
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
    this.categoryModel.tableName = this.tableName;
    this.categoryService.updateCategoryTable(this.id, this.categoryModel).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Cập nhập danh mục thành công!');
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
    this.categoryModel = {
      id: null,
      parentId: null,
      name: '',
      order: null,
      tableName: this.categoryModel.tableName,
    }

    this.getListOder();
  }

}
