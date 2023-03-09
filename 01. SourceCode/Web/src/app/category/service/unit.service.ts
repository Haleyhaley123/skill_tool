import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Configuration } from 'src/app/shared';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class UnitService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchUnit(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'don-vi-tinh/search', model, httpOptions);
    return tr;
  }

  createUnit(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'don-vi-tinh', model, httpOptions);
    return tr;
  }

  updateUnit(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'don-vi-tinh/' + id, model, httpOptions);
    return tr;
  }

  deleteUnit(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'don-vi-tinh/' + id, httpOptions);
    return tr;
  }

  getUnitInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'don-vi-tinh/' + id, httpOptions);
    return tr;
  }

  getListOder(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'don-vi-tinh/get-list-oder?id=' + id, httpOptions);
    return tr;
  }

  getListUnit(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'don-vi-tinh/get-list-don-vi-tinh', httpOptions);
    return tr;
  }
}
