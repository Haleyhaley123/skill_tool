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
export class CategoryService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchCategory(): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'category/search', httpOptions);
    return tr;
  }

  createCategory(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'category', model, httpOptions);
    return tr;
  }

  updateCategory(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'category/' + id, model, httpOptions);
    return tr;
  }

  deleteCategory(id: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'category/' + id, httpOptions);
    return tr;
  }

  getCategoryInfo(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'category/' + id, httpOptions);
    return tr;
  }

  getListOder(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'category/get-list-oder?id=' + id, httpOptions);
    return tr;
  }

  searchCategoryTable(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'category/table/search', model, httpOptions);
    return tr;
  }

  createCategoryTable(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'category/table', model, httpOptions);
    return tr;
  }

  updateCategoryTable(id: string, model: any): Observable<any> {
    var tr = this.http.put<any>(this.config.ServerWithApiUrl + 'category/table/' + id, model, httpOptions);
    return tr;
  }

  deleteCategoryTable(id: string, tableName: string): Observable<any> {
    var tr = this.http.delete<any>(this.config.ServerWithApiUrl + 'category/table/' + id + '?tableName=' + tableName, httpOptions);
    return tr;
  }

  getCategoryTableInfo(id: string, tableName: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'category/table/' + id + '?tableName=' + tableName, httpOptions);
    return tr;
  }

  getListOderTable(id: string, tableName: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'category/table/get-list-oder?id=' + id + '&tableName=' + tableName, httpOptions);
    return tr;
  }

  getExport(): Observable<any> {
    var tr = this.http.get(this.config.ServerWithApiUrl + 'category/export', {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: "blob"
    });
    return tr
  }
}
