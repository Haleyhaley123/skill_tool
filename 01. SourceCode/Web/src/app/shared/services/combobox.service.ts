import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Constants } from '../common/Constants';
import { Configuration } from '../config/configuration';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class ComboboxService {

  constructor(
    private http: HttpClient,
    private config: Configuration,
    public constant: Constants,
  ) { }

  getTinhs(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh', model, httpOptions);
    return tr;
  }

  getHuyens(id: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-huyen-by-tinh/' + id, httpOptions);
    return tr;
  }

  getListXaByHuyen(id: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xa-by-huyen/' + id, httpOptions);
    return tr;
  }

  getListXaByDon(id: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xa-by-don/' + id, httpOptions);
    return tr;
  }

  getTuyens(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tuyen-bg', httpOptions);
    return tr;
  }

  getDons(id: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don/' + id, httpOptions);
    return tr;
  }

  getListHeLoai(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-he-loai', httpOptions);
    return tr;
  }

  getListGroupuser(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-group-user', httpOptions);
    return tr;
  }

  getListUser(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-user', httpOptions);
    return tr;
  }

  getListDon(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-all', httpOptions);
    return tr;
  }

  getListChucVu(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-chuc-vu', httpOptions);
    return tr;
  }

  getListPhanLoaiQLNV(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-phan-loai-qlnv', httpOptions);
    return tr;
  }

  getListLoaiNghien(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-nghien', httpOptions);
    return tr;
  }

  getListDonVi(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vi', httpOptions);
    return tr;
  }

  getListTinhChat(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh-chat', httpOptions);
    return tr;
  }

  getListNgheNghiep(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-nghe-nghiep', httpOptions);
    return tr;
  }

  getListKetThucKTNV(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-ketthuc-ktnv', httpOptions);
    return tr;
  }

  getListKetThucQLNV(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-ketthuc-qlnv', httpOptions);
    return tr;
  }

  getListDanToc(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-dan-toc', httpOptions);
    return tr;
  }

  getListTinhChatCA(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh-chat-chuyen-an', httpOptions);
    return tr;
  }

  getTinhTrangCA(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh-trang-chuyen-an', httpOptions);
    return tr;
  }

  getSoTruongLLM(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-so-truong-llm', httpOptions);
    return tr;
  }

  getPhuongPhapXDLLM(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xay-dung-llm', httpOptions);
    return tr;
  }

  getQuanHeDB(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-quanhe-diaban', httpOptions);
    return tr;
  }

  getKetThucLLM(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-kethuc-llm', httpOptions);
    return tr;
  }

  getChatLuongLLM(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-chatluong-llm', httpOptions);
    return tr;
  }

  getLoaiLLM(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-llm', httpOptions);
    return tr;
  }

  getListCapBac(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-cap-bac', httpOptions);
    return tr;
  }

  getListMucDichMBN(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-muc-dich-mbn', httpOptions);
    return tr;
  }

  getListNoiChuyenTuyen(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-noi-chuyen-tuyen', httpOptions);
    return tr;
  }

  getListTienAn(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tien-an', httpOptions);
    return tr;
  }

  getListCoSoCaiNghien(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-co-so-cai-nghien', httpOptions);
    return tr;
  }

  getListNoiChuyenTuyenMBN(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-noi-chuyen-tuyen-mbn', httpOptions);
    return tr;
  }

  getListDonViParent(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vi-child', httpOptions);
    return tr;
  }

  getDonViChild(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vis', httpOptions);
    return tr;
  }

  getListXaByHuyenAllAddress(id: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xa-by-huyen-all/' + id, httpOptions);
    return tr;
  }

  getListTinh(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh-all', httpOptions);
    return tr;
  }

  getListTonGiao(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-ton-giao', httpOptions);
    return tr;
  }

  getListHinhThucKhenThuong(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-hinh-thuc-khen-thuong', httpOptions);
    return tr;
  }

  getListCanBo(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-can-bo', httpOptions);
    return tr;
  }

  getListHinhThucKyLuat(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-hinh-thuc-ky-luat', httpOptions);
    return tr;
  }

  getListCanBoAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'can-bo/get-list-can-bo-all', httpOptions);
    return tr;
  }

  getListCoQuanBanHanhAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-co-quan-ban-hanh', httpOptions);
    return tr;
  }

  getListLoaiVanBanAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-van-ban', httpOptions);
    return tr;
  }

  getListDonViPhoiHopAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vi-phoi-hop', httpOptions);
    return tr;
  }

  getListNghiDinhAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-nghi-dinh', httpOptions);
    return tr;
  }

  getLisHinhThucPhatAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-hinh-thuc-phat', httpOptions);
    return tr;
  }

  getListLoaiTangVat(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-tang-vat', httpOptions);
    return tr;
  }

  getListDonViTinh(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vi-tinh', httpOptions);
    return tr;
  }

  getListLoaiPhuongTien(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-phuong-tien', httpOptions);
    return tr;
  }

  getListQuocTich(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-quoc-tich', httpOptions);
    return tr;
  }

  getListNguoi(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-nguoi', httpOptions);
    return tr;
  }

  getListNguoiAll(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-all-nguoi', httpOptions);
    return tr;
  }

  getDonBPByDonVis(id: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-don-bien-phong-by-don-vi/' + id, httpOptions);
    return tr;
  }

  getListNguonTin(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-nguon-tin', httpOptions);
    return tr;
  }

  getListTruongHopBat(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-truong-hop-bat', httpOptions);
    return tr;
  }

  getListNhomToi(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-nhom-toi', httpOptions);
    return tr;
  }

  getListTinhChatVuViec(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh-chat-vu-viec', httpOptions);
    return tr;
  }

  getListXuLyDTHS(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xu-ly-dths', httpOptions);
    return tr;
  }

  getListNguoiChoose(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'combobox/get-list-nguoi-choose', model, httpOptions);
    return tr;
  }

  getListThamQuyen(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tham-quyen-xu-phat', httpOptions);
    return tr;
  }

  getLisHinhThucPhatByThamQuyen(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-hinh-thuc-phat-by-tham-quyen/' + id, httpOptions);
    return tr;
  }

  getListLoaiDonViTinh(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-don-vi-tinh', httpOptions);
    return tr;
  }

  getListHinhThucCungCapTin(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-hinh-thuc-cung-cap-tin', httpOptions);
    return tr;
  }

  getListLoaiDoiTuong(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-doi-tuong', httpOptions);
    return tr;
  }

  getListPhanLoaiTin(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-phan-loai-tin', httpOptions);
    return tr;
  }

  getListXuLyTinBao(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xu-ly-tin-bao', httpOptions);
    return tr;
  }

  getListLyLuanChinhTri(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-ly-luan-chinh-tri', httpOptions);
    return tr;
  }

  getListCheDoCS(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-che-do-chinh-sach', httpOptions);
    return tr;
  }

  getListDonViGiaiCuuMBN(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vi-giai-cuu-mbn', httpOptions);
    return tr;
  }

  getListLinhVuc(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-linh-vuc', httpOptions);
    return tr;
  }

  getListTruongDaoTao(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-truong-dao-tao', httpOptions);
    return tr;
  }

  getListCapRaQuyetDinhKhenThuong(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-cap-ra-quyet-dinh-khen-thuong', httpOptions);
    return tr;
  }

  getListCapRaQuyetDinhKyLuat(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-cap-ra-quyet-dinh-ky-luat', httpOptions);
    return tr;
  }

  getListCapRaQuyetDinhThuongNong(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-cap-ra-quyet-dinh-thuong-nong', httpOptions);
    return tr;
  }

  getListDonViByIdTinh(IdTinh: any): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-don-vi/' + IdTinh, httpOptions);
    return tr;
  }

  getListDieuLuat(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-dieu-luat/', httpOptions);
    return tr;
  }
  getListThamQuyenPheDuyet(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tham-quen-phe-duyet', httpOptions);
    return tr;
  }

  getAllTinh(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-tinh-all', httpOptions);
    return tr;
  }

  getListLoaiDoiTuongTruyNa(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-loai-doi-tuong-truy-na', httpOptions);
    return tr;
  }

  getListDienDoiTuong(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-dien-doi-tuong', httpOptions);
    return tr;
  }

  getListXuLyXNCTP(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-list-xu-ly-xuat-nhap-canh-trai-phep', httpOptions);
    return tr;
  }

  getTimeTypeReport(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-time-type-report', httpOptions);
    return tr;
  }

  getDateNowServer(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-date-now-server', httpOptions);
    return tr;
  }

  getAllWeekReport(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-week-report', httpOptions);
    return tr;
  }

  getAllMonthReport(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-month-report', httpOptions);
    return tr;
  }

  getAllYearReport(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-year-report', httpOptions);
    return tr;
  }

  getThaiDoHopTac(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'combobox/get-thai-do-hop-tac', httpOptions);
    return tr;
  }
}
