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
export class LoaiMaTuyService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchLoaiMaTuy(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy/search', model, httpOptions);
    return tr;
  }

  createLoaiMaTuy(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy', model, httpOptions);
    return tr;
  }

  updateLoaiMaTuy(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy/' + id, model, httpOptions);
    return tr;
  }

  deleteLoaiMaTuy(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy/' + id, httpOptions);
    return tr;
  }

  getLoaiMaTuyInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy/' + id, httpOptions);
    return tr;
  }

  getListOder(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy/get-list-oder?id=' + id, httpOptions);
    return tr;
  }

  getListDonViTinh(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'loai-ma-tuy/get-list-don-vi-tinh', httpOptions);
    return tr;
  }
}
