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
export class AboutService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  getAbout(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'about', httpOptions);
    return tr;
  }

  create(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'about', model, httpOptions);
    return tr;
  }
}
