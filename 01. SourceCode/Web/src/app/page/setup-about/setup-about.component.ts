import { Component, ElementRef, OnInit, ViewEncapsulation } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MessageService, Constants, FileProcess, Configuration, AppSetting } from 'src/app/shared';
import { FileService } from 'src/app/shared/services/file.service';
import { ImageService } from 'src/app/shared/services/image.service';
import { AboutService } from '../service/about.service';

@Component({
  selector: 'app-setup-about',
  templateUrl: './setup-about.component.html',
  styleUrls: ['./setup-about.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class SetupAboutComponent implements OnInit {

  constructor(
    public constant: Constants,
    public fileProcess: FileProcess,
    public config: Configuration,
    public fileProcessDataSheet: FileProcess,
    private fileService: FileService,
    private elRef: ElementRef,
    public appSetting: AppSetting,
    private _sanitizer: DomSanitizer,
    private aboutService: AboutService,
    private messageService: MessageService,) { }

  discoveryConfig = {
    plugins: ['image code', 'visualblocks', 'print preview', 'table', 'directionality', 'link', 'media', 'codesample', 'table', 'charmap', 'hr', 'pagebreak', 'nonbreaking', 'anchor', 'toc', 'insertdatetime', 'advlist', 'lists', 'textcolor', 'wordcount', 'imagetools', 'contextmenu', 'textpattern', 'searchreplace visualblocks code fullscreen',],
    language: 'vi_VN',
    // file_picker_types: 'file image media',
    automatic_uploads: true,
    toolbar: 'undo redo | fontselect | fontsizeselect | bold italic forecolor backcolor |alignleft aligncenter alignright alignjustify alignnone | numlist | table | link | outdent indent',
    convert_urls: false,
    height: window.innerHeight - 350,
    auto_focus: false,
    plugin_preview_width: 1000,
    plugin_preview_height: 650,
    readonly: 0,
    content_style: "body {font-size: 12pt;font-family: Arial;}",
    aligncenter: {
      selector: 'media', classes: 'center',
      styles: { display: 'block', margin: '0px auto', textAlign: 'center' }
    },
    // file_browser_callback: function RoxyFileBrowser(field_name, url, type, win) {
    //   //var roxyFileman = '/fileman/index.html';
    //   var roxyFileman = "https://nhantinsoft.vn:9566/fileServer/fileman/index.html";
    //   if (roxyFileman.indexOf("?") < 0) {
    //     roxyFileman += "?type=" + type;
    //   }
    //   else {
    //     roxyFileman += "&type=" + type;
    //   }
    //   roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
    //   if (tinymce.activeEditor.settings.language) {
    //     roxyFileman += '&langCode=' + tinymce.activeEditor.settings.language;
    //   }
    //   tinymce.activeEditor.windowManager.open({
    //     file: roxyFileman,
    //     title: 'Roxy Fileman',
    //     width: 850,
    //     height: 650,
    //     resizable: "yes",
    //     plugins: "media",
    //     inline: "yes",
    //     close_previous: "no"
    //   }, {
    //     window: win,
    //     input: field_name
    //   });
    //   return false;
    // },
    //setup: TinymceUserConfig.setup,
    // content_css: '/assets/css/custom_editor.css',
    images_upload_handler: (blobInfo, success, failure) => {
      this.fileService.uploadFile(blobInfo.blob(), 'About').subscribe(
        result => {
          success(this.config.ServerApi + result.data.fileUrl);
        },
        error => {
          return;
        }
      );
    },
  };

  height: number;
  model: any = {
    content: ''
  }

  ngOnInit(): void {
    this.appSetting.PageTitle = 'Cấu hình giới thiệu';
    this.getAbout();
  }

  getAbout() {
    this.aboutService.getAbout().subscribe(
      data => {
        if (data.statusCode == this.constant.StatusCode.Success) {
          this.model = data.data;
        }
        else {
          this.messageService.showListMessage(data.Message);
        }
      }, error => {
        this.messageService.showError(error);
      }
    );
  }

  create() {
    this.aboutService.create(this.model).subscribe(
      result => {
        if (result.statusCode == this.constant.StatusCode.Success) {
          this.messageService.showSuccess('Chỉnh sửa giới thiệu thành công!');
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
}
