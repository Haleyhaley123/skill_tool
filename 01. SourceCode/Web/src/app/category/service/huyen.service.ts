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
export class HuyenService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchHuyen(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'huyen/search', model, httpOptions);
    return tr;
  }

  createHuyen(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'huyen', model, httpOptions);
    return tr;
  }

  updateHuyen(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'huyen/' + id, model, httpOptions);
    return tr;
  }

  deleteHuyen(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'huyen/' + id, httpOptions);
    return tr;
  }

  getHuyenInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'huyen/' + id, httpOptions);
    return tr;
  }
}
