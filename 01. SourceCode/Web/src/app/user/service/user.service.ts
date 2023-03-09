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
export class UserService {

  constructor(
    private http: HttpClient,
    private config: Configuration
  ) { }

  searchMentor(model: any): Observable<any> {
    var tr = this.http.post<any>(this.config.ServerWithApiUrl + 'employee/search-mentor', model, httpOptions);
    return tr
  }

  getUserStudentByid(id: string): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'fontend/userlogins/' + id, httpOptions);
    return tr
  }

  searchUser(model: any): Observable<any> {
    return this.http.post<any>(this.config.ServerWithApiUrl + 'users/search', model, httpOptions);
  }

  search(model: any): Observable<any> {
    return this.http.post<any>(this.config.ServerWithApiUrl + 'users/search-user', model, httpOptions);
  }

  deleteUser(id: string): Observable<any> {
    return this.http.delete<any>(this.config.ServerWithApiUrl + 'users/delete/' + id, httpOptions);
  }

  createUser(model: any): Observable<any> {
    return this.http.post<any>(this.config.ServerWithApiUrl + 'users/create', model, httpOptions);
  }

  updateUser(id: string, model: any): Observable<any> {
    return this.http.put<any>(this.config.ServerWithApiUrl + 'users/update/' + id, model, httpOptions);
  }

  getUserById(id: string): Observable<any> {
    return this.http.get<any>(this.config.ServerWithApiUrl + 'users/get-user-by-id/' + id, httpOptions);
  }

  userAdminLock(id: string): Observable<any> {
    return this.http.put<any>(this.config.ServerWithApiUrl + 'users/lock/' + id, httpOptions);
  }

  userAdminUnLock(id: string): Observable<any> {
    return this.http.put<any>(this.config.ServerWithApiUrl + 'users/lock/' + id, httpOptions);
  }

  getGroupPermission(id: string): Observable<any> {
    return this.http.get<any>(this.config.ServerWithApiUrl + 'users/GetGroupPermission/' + id, httpOptions);
  }

  changePassword(id: string, model: any): Observable<any> {
    return this.http.put<any>(this.config.ServerWithApiUrl + 'users/change-password/' + id, model, httpOptions);
  }

  getGroupPermissionById(groupUserId: string): Observable<any> {
    return this.http.get<any>(this.config.ServerWithApiUrl + 'users/get-group-permission/' + groupUserId, httpOptions);
  }

  getPermission(): Observable<any> {
    var tr = this.http.get<any>(this.config.ServerWithApiUrl + 'users/get-permission', httpOptions);
    return tr;
  }

  resetPassword(id: string): Observable<any> {
    return this.http.put<any>(this.config.ServerWithApiUrl + 'users/reset-pass/' + id, httpOptions);
  }

  updateUserInfo(id: string, model: any): Observable<any> {
    return this.http.put<any>(this.config.ServerWithApiUrl + 'users/update-info/' + id, model, httpOptions);
  }

  getUserInfo(id: string): Observable<any> {
    return this.http.get<any>(this.config.ServerWithApiUrl + 'users/get-user-by-info/' + id, httpOptions);
  }
}
