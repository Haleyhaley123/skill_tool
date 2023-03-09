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
export class ThamQuyenXpvphcService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchThamQuyenXPVPHC(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc/search', model, httpOptions);
    return tr;
  }

  createThamQuyenXPVPHC(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc', model, httpOptions);
    return tr;
  }

  updateThamQuyenXPVPHC(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc/' + id, model, httpOptions);
    return tr;
  }

  deleteThamQuyenXPVPHC(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc/' + id, httpOptions);
    return tr;
  }

  getThamQuyenXPVPHCInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc/' + id, httpOptions);
    return tr;
  }

  getListOder(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc/get-list-oder?id=' + id, httpOptions);
    return tr;
  }

  getListThamQuyenXPVPHC(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'tham-quyen-xp-vphc/get-list-tham-quyen-xp-vphc', httpOptions);
    return tr;
  }
}
