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
export class TraCuuDoiTuongService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchDoiTuong(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'tra-cuu-doi-tuong/search', model, httpOptions);
    return tr;
  }

  getDoiTuongInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'tra-cuu-doi-tuong/' + id, httpOptions);
    return tr;
  }

  exportExcel(model: any): Observable<any> {
    let apiPath = this.config.ServerWithApiUrl + 'tra-cuu-doi-tuong/export-excel';
    var tr = this.http.post(apiPath, model, { responseType: "blob" });
    return tr;
  }

  exportPDF(model: any): Observable<any> {
    let apiPath = this.config.ServerWithApiUrl + 'tra-cuu-doi-tuong/export-pdf';
    var tr = this.http.post(apiPath, model, { responseType: "blob" });
    return tr;
  }
}
