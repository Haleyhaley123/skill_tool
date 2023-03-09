import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

import { MessageComponent } from '../component/message/message.component';
import { MessageconfirmComponent } from '../component/messageconfirm/messageconfirm.component';
import { NTSModalService } from '../services/ntsmodal.service';
import { MessageconfirmcodeComponent } from '../component/messageconfirmcode/messageconfirmcode.component';

@Injectable({
    providedIn: 'root',
})
export class MessageService {

    constructor(private modalService: NgbModal, private ntsModalService: NTSModalService,
        public router: Router,
        private toastr: ToastrService) {

    }

    /*
    * type:
    * 0: Bình thường
    * 1: Lỗi
    */
    showMessage(message: string) {
        const activeModal = this.modalService.open(MessageComponent, { container: 'body', centered: true })
        activeModal.componentInstance.message = message;
        activeModal.result.then((result) => {
            this.ntsModalService.closeMultiModal();
        }, (reason) => {
            this.ntsModalService.closeMultiModal();
        });
    }

    showListMessage(message: string[]) {
        const activeModal = this.modalService.open(MessageComponent, { container: 'body' })
        activeModal.componentInstance.messages = message;
        activeModal.result.then((result) => {
            this.ntsModalService.closeMultiModal();
        }, (reason) => {
            this.ntsModalService.closeMultiModal();
        });
    }

    showError(error: any) {
        let message: string;
        if (error.status == 0) {
            message = 'Không thể kết nối được đến server, vui lòng kiểm tra lại';
        } else {
            if (error.error.message) {
                message = error.error.message;
            }
            else if (error.error.error_description) {
                message = error.error.error_description;
            }
            else {
                message = error.error;
            }

            if (error.status == 401) {
                localStorage.removeItem('PCMTCurrentUser');
                message = 'Bạn đã hết phiên làm việc. Bạn hãy đăng nhập lại để tiếp tục.';
                this.router.navigate(['/auth/dang-nhap']);
            }
        }

        this.showMessage(message);
    }

    showMessageErrorBlob(err: any) {
        var arrayBuffer;
        var fileReader = new FileReader();
        fileReader.onload = (event: any) => {
            arrayBuffer = event.target;
            let str = new TextDecoder().decode(arrayBuffer.result);
            this.showMessage(str);
        };
        fileReader.readAsArrayBuffer(err.error);
    }

    showConfirm(message: string): Promise<any> {
        return new Promise((resolve, reject) => {
            const activeModalConfirm = this.modalService.open(MessageconfirmComponent, { container: 'body', centered: true })
            activeModalConfirm.componentInstance.message = message;
            activeModalConfirm.result.then((result) => {
                this.ntsModalService.closeMultiModal();
                if (result) {
                    resolve(result);
                }
                // else{
                //     reject(false);
                // }
            }, (reason) => {
                reject('');
                this.ntsModalService.closeMultiModal();
            });
        });
    }

    showSuccess(message: string) {
        this.toastr.success(message);
    }

    showInfo(message: string) {
        this.toastr.info(message);
    }

    showWarning(message: string) {
        this.toastr.warning(message);
    }

    showConfirmCode(message: string): Promise<any> {
        return new Promise((resolve, reject) => {
            const activeModalConfirm = this.modalService.open(MessageconfirmcodeComponent, { container: 'body' })
            activeModalConfirm.componentInstance.message = message;
            activeModalConfirm.result.then((result) => {
                this.ntsModalService.closeMultiModal();
                if (result) {
                    resolve(result);
                }
                else {
                    reject(false);
                }
            }, (reason) => {
                reject('');
                this.ntsModalService.closeMultiModal();
            });
        });
    }
}
