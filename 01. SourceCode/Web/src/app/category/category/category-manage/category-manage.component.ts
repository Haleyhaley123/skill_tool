import { CdkDragDrop, moveItemInArray } from '@angular/cdk/drag-drop';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TreeGridComponent } from '@syncfusion/ej2-angular-treegrid';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Constants, AppSetting, MessageService, Configuration, FileProcess } from 'src/app/shared';
import { SearchGlobalService } from 'src/app/shared/common/search-global.service';
import { MenuOptions } from 'src/app/shared/models';
import { CategoryService } from '../../service/category.service';
import { CategoryCreateComponent } from '../category-create/category-create.component';

@Component({
  selector: 'app-category-manage',
  templateUrl: './category-manage.component.html',
  styleUrls: ['./category-manage.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class CategoryManageComponent implements OnInit {

  @ViewChild('grid')
  public grid!: TreeGridComponent;

  constructor(
    public constant: Constants,
    public appSetting: AppSetting,
    private modalService: NgbModal,
    private fileProcess: FileProcess,
    private messageService: MessageService,
    private categoryService: CategoryService,
    private searchGlobalService: SearchGlobalService,
  ) {
    this._unsubscribeAll = new Subject();
  }

  _unsubscribeAll: Subject<any>;
  startIndex = 1;
  public searchSettingModel: Object = {};
  public toolbar: string[] = [];
  groupCategories: any[] = [];
  categories: any[] = [];
  height = 0;
  type = 0;
  name: string;

  searchModel: any = {
    pageNumber: 1,
    pageSize: 10,
    totalItems: 0,

    name: '',
    tableName: '',
    //groupCategoryId: null,
  }

  categoryModel: any = {
    id: null,
    name: '',
    order: null,
    tableName: '',
  }

  ngOnInit(): void {
    this.appSetting.PageTitle = "Quản lý danh mục";
    this.searchSettingModel = { hierarchyMode: 'Parent' };
    this.toolbar = ['Search'];
    this.height = window.innerHeight - 180;
    this.searchCategory();
    this.searchGlobalService.onDataChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      if (data) {
        this.searchModel.name = data.name;
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
        Placeholder: 'Tìm kiếm tên đơn vị tính',
        Items: [
          // {
          //   FieldName: 'type',
          //   Name: 'Loại đơn vị tính',
          //   Type: 'ngselect',
          //   DisplayName: 'Name',
          //   ValueName: 'Id',
          //   Data: this.constant.listUnitType
          // }
        ]
      }
    };

    this.searchGlobalService.setConfig(meuOptions);
  }

  searchCategory() {
    this.categoryService.searchCategory().subscribe(
      (result: any) => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          setTimeout(() => {
            this.groupCategories = result.data;
            if (this.groupCategories.length > 0) {
              this.grid.selectedRowIndex = 1;
            }
          }, 200);
        }
        else {
          this.messageService.showMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  search() {
    if (this.type == 2
      && this.searchModel.tableName !== this.constant.Category_DonViTinh
      && this.searchModel.tableName !== this.constant.Category_ThamQuyenXPVPHC
      && this.searchModel.tableName !== this.constant.Category_TrangThietBi
      && this.searchModel.tableName !== this.constant.Category_LoaiTangVat
      && this.searchModel.tableName !== this.constant.Category_LoaiMaTuy
      && this.searchModel.tableName !== this.constant.Category_Tinh
      && this.searchModel.tableName !== this.constant.Category_Huyen
      && this.searchModel.tableName !== this.constant.Category_Xa) {
      this.categoryService.searchCategoryTable(this.searchModel).subscribe(
        (result: any) => {
          if (result.statusCode == this.constant.StatusCode.Success) {
            this.searchModel.totalItems = result.data.totalItems;
            this.categories = result.data.dataResults;
          }
          else {
            this.messageService.showMessage(result.message);
          }
        }, error => {
          this.messageService.showError(error);
        }
      );
    }
  }

  onChange(event: any) {
    // = event.itemData.id;
  }

  rowSelected($event: any) {
    this.searchModel.tableName = $event.data.tableName;
    this.type = $event.data.type;
    this.name = $event.data.name;
    if (this.type == 2
      && this.searchModel.tableName !== this.constant.Category_DonViTinh
      && this.searchModel.tableName !== this.constant.Category_ThamQuyenXPVPHC
      && this.searchModel.tableName !== this.constant.Category_TrangThietBi
      && this.searchModel.tableName !== this.constant.Category_LoaiTangVat
      && this.searchModel.tableName !== this.constant.Category_LoaiMaTuy
      && this.searchModel.tableName !== this.constant.Category_Tinh
      && this.searchModel.tableName !== this.constant.Category_Huyen
      && this.searchModel.tableName !== this.constant.Category_Xa) {
      this.setToolbarConfig();
      this.search();
    }
  }

  showConfirmDelete(id: string) {
    this.messageService.showConfirm("Bạn có chắc muốn xoá danh mục này không?").then(
      result => {
        this.delete(id);
      }
    );
  }

  delete(id: string) {
    this.categoryService.deleteCategoryTable(id, this.searchModel.tableName).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Xóa danh mục thành công!');
          this.search();
        }
        else {
          this.messageService.showMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  clear() {
    this.searchModel = {
      pageNumber: 1,
      pageSize: 10,
      totalItems: 0,

      name: '',
      tableName: this.searchModel.tableName,
      groupCategoryId: null,
    }
    this.search();
  }

  showCreateUpdate(id: string) {
    let activeModal = this.modalService.open(CategoryCreateComponent, { container: 'body', windowClass: 'category-create-model', backdrop: 'static' })
    activeModal.componentInstance.id = id;
    activeModal.componentInstance.tableName = this.searchModel.tableName;
    activeModal.result.then((result: any) => {
      if (result) {
        this.search();
      }
    });
  }

  onDrop(event: CdkDragDrop<string[]>) {
    let currentIndex = this.categories[event.currentIndex].order;
    let previousIndex = this.categories[event.previousIndex].order;
    this.categories[event.currentIndex].order = previousIndex;
    this.categories[event.previousIndex].order = currentIndex;
    moveItemInArray(this.categories, event.previousIndex, event.currentIndex);
    if (currentIndex < previousIndex) {
      this.categoryModel = this.categories[event.currentIndex];
    } else {
      this.categoryModel = this.categories[event.previousIndex];
    }

    this.update();
  }

  update() {
    this.categoryModel.tableName = this.searchModel.tableName;
    this.categoryService.updateCategoryTable(this.categoryModel.id, this.categoryModel).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Cập nhập danh mục thành công!');
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

  export() {
    this.categoryService.getExport().subscribe(data => {
      var blob = new Blob([data], { type: 'octet/stream' });
      var url = window.URL.createObjectURL(blob);
      this.fileProcess.downloadFileLink(url, "Danhmuc.zip");
    }, error => {
      const blb = new Blob([error.error], { type: "text/plain" });
      const reader = new FileReader();

      reader.onload = () => {
        //this.messageService.showMessage(reader.result.toString().replace('"', '').replace('"', ''));
      };
      // Start reading the blob as text.
      reader.readAsText(blb);
    });
  }
}
