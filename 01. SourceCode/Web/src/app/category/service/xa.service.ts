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
export class XaService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchXa(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'xa/search', model, httpOptions);
    return tr;
  }

  createXa(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'xa', model, httpOptions);
    return tr;
  }

  updateXa(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'xa/' + id, model, httpOptions);
    return tr;
  }

  deleteXa(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'xa/' + id, httpOptions);
    return tr;
  }

  getXaInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'xa/' + id, httpOptions);
    return tr;
  }
}
