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
export class TinhService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchTinh(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'tinh/search', model, httpOptions);
    return tr;
  }

  createTinh(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'tinh', model, httpOptions);
    return tr;
  }

  updateTinh(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'tinh/' + id, model, httpOptions);
    return tr;
  }

  deleteTinh(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'tinh/' + id, httpOptions);
    return tr;
  }

  getTinhInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'tinh/' + id, httpOptions);
    return tr;
  }
}
