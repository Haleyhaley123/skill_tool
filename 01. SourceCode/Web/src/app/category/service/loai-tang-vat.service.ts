import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/app/shared';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class LoaiTangVatService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchLoaiTangVat(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'loai-tang-vat/search', model, httpOptions);
    return tr;
  }

  createLoaiTangVat(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'loai-tang-vat', model, httpOptions);
    return tr;
  }

  updateLoaiTangVat(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'loai-tang-vat/' + id, model, httpOptions);
    return tr;
  }

  deleteLoaiTangVat(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'loai-tang-vat/' + id, httpOptions);
    return tr;
  }

  getLoaiTangVatInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'loai-tang-vat/' + id, httpOptions);
    return tr;
  }

  getListOder(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'loai-tang-vat/get-list-oder?id=' + id, httpOptions);
    return tr;
  }

  getListDonViTinh(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'loai-tang-vat/get-list-don-vi-tinh', httpOptions);
    return tr;
  }
}
