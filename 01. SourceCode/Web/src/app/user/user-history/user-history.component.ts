import { Component, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { Constants, DateUtils, AppSetting, FileProcess, MessageService } from 'src/app/shared';
import { SearchGlobalService } from 'src/app/shared/common/search-global.service';
import { MenuOptions } from 'src/app/shared/models';
import { ComboboxService } from 'src/app/shared/services/combobox.service';
import { UserHistoryService } from '../service/user-history.service';

@Component({
  selector: 'app-user-history',
  templateUrl: './user-history.component.html',
  styleUrls: ['./user-history.component.scss']
})
export class UserHistoryComponent implements OnInit {

  constructor(
    public constant: Constants,
    private dateUtils: DateUtils,
    public appSetting: AppSetting,
    private fileProcess: FileProcess,
    private userHistoryService: UserHistoryService,
    private messageService: MessageService,
    private comboboxService: ComboboxService,
    private searchGlobalService: SearchGlobalService
  ) {
    this._unsubscribeAll = new Subject();
  }

  startIndex = 1;
  userHistories: any[] = [];
  dateFrom = null;
  dateTo = null;
  listUser: any[] = [];
  _unsubscribeAll: Subject<any>;

  searchModel: any = {
    pageNumber: 1,
    pageSize: 10,
    totalItems: 0,

    userId: null,
    name: '',
    type: null,
    dateToV: this.dateUtils.getDateNowToObject(),
    dateFromV: this.dateUtils.getDateNowToObject(),
    dateTo: null,
    dateFrom: null
  }

  ngOnInit(): void {
    this.appSetting.PageTitle = "Lịch sử thao tác";
    this.getListUser();
    this.searchGlobalService.onDataChanged.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      if (data) {
        this.searchModel.userId = data.userId;
        this.searchModel.type = data.type;
        this.searchModel.name = data.name;
        this.searchModel.dateToV = data.dateToV;
        this.searchModel.dateFromV = data.dateFromV;
        this.searchModel.pageNumber = data.pageNumber;

        this.search();
      }
    });

    this.searchGlobalService.onExportExcel.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      if (data) {
        this.exportExcel();
      }
    });

    this.searchGlobalService.onExportPdf.pipe(takeUntil(this._unsubscribeAll)).subscribe(data => {
      if (data) {
        this.exportPdf();
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
      isExcel: true,
      isPDF: true,
      isSearch: true,
      searchModel: this.searchModel,
      searchOptions: {
        FieldContentName: 'name',
        Placeholder: 'Tìm kiếm nội dung',
        Items: [
          {
            FieldName: 'userId',
            Name: 'Tài khoản',
            Type: 'ngselect',
            DisplayName: 'name',
            ValueName: 'id',
            GetData: (): Observable<any> => {
              return this.comboboxService.getListUser();
            }
          },
          {
            FieldName: 'type',
            Name: 'Phân loại',
            Type: 'ngselect',
            DisplayName: 'Name',
            ValueName: 'Id',
            Data: this.constant.UserHistory_Type
          },
          {
            //FieldName: 'ngayMoHS',
            Name: 'Thời điểm',
            FieldNameTo: 'dateToV',
            FieldNameFrom: 'dateFromV',
            Type: 'date',
            GetData: null,
          },
        ]
      }
    };

    this.searchGlobalService.setConfig(meuOptions);
  }

  getListUser() {
    this.comboboxService.getListUser().subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.listUser = result.data;
        }
        else {
          this.messageService.showListMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  search() {
    if (this.searchModel.dateFromV != null) {
      this.searchModel.dateFrom = this.dateUtils.convertObjectToDate(this.searchModel.dateFromV);
    } else {
      this.searchModel.dateFrom = null;
    }

    if (this.searchModel.dateToV != null) {
      this.searchModel.dateTo = this.dateUtils.convertObjectToDate(this.searchModel.dateToV);
    } else {
      this.searchModel.dateTo = null;
    }

    this.userHistoryService.searchUserHistory(this.searchModel).subscribe(
      (result: any) => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.startIndex = ((this.searchModel.pageNumber - 1) * this.searchModel.pageSize + 1);
          this.userHistories = result.data.dataResults;
          this.searchModel.totalItems = result.data.totalItems;
        }
        else {
          this.messageService.showMessage(result.message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  exportExcel() {
    this.userHistoryService.exportExcel(this.searchModel).subscribe(data => {
      var blob = new Blob([data], { type: 'octet/stream' });
      var url = window.URL.createObjectURL(blob);
      this.fileProcess.downloadFileLink(url, "LichSuThaoTac.xlsx");
    }, error => {
      const blb = new Blob([error.error], { type: "text/plain" });
      const reader = new FileReader();

      reader.onload = () => {
        if (reader && reader.result)
          this.messageService.showMessage(reader.result.toString().replace('"', '').replace('"', ''));
      };
      // Start reading the blob as text.
      reader.readAsText(blb);
    });
  }

  exportPdf() {
    this.userHistoryService.exportPdf(this.searchModel).subscribe(data => {
      var blob = new Blob([data], { type: 'octet/stream' });
      var url = window.URL.createObjectURL(blob);
      this.fileProcess.downloadFileLink(url, "LichSuThaoTac.pdf");
    }, error => {
      const blb = new Blob([error.error], { type: "text/plain" });
      const reader = new FileReader();

      reader.onload = () => {
        if (reader && reader.result)
          this.messageService.showMessage(reader.result.toString().replace('"', '').replace('"', ''));
      };
      // Start reading the blob as text.
      reader.readAsText(blb);
    });
  }

  clear() {
    this.searchModel = {
      pageNumber: 1,
      pageSize: 10,
      totalItems: 0,

      userId: null,
      name: '',
      dateToV: this.dateUtils.getDateNowToObject(),
      dateFromV: this.dateUtils.getDateNowToObject(),
      dateFrom: null,
      dateTo: null,
      type: null,
    };

    this.dateFrom = null;
    this.dateTo = null;

    this.search();
  }

}
