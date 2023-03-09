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
export class TrangThietBiService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchTrangThietBi(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi/search', model, httpOptions);
    return tr;
  }

  createTrangThietBi(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi', model, httpOptions);
    return tr;
  }

  updateTrangThietBi(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi/' + id, model, httpOptions);
    return tr;
  }

  deleteTrangThietBi(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi/' + id, httpOptions);
    return tr;
  }

  getTrangThietBiInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi/' + id, httpOptions);
    return tr;
  }

  getListOder(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi/get-list-oder?id=' + id, httpOptions);
    return tr;
  }

  getListTrangThietBi(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'trang-thiet-bi/get-list-trang-thiet-bi', httpOptions);
    return tr;
  }
}
