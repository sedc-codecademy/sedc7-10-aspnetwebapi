import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RegisterModel } from '../models/register.model';
import { LoginModel } from '../models/login.model';

@Injectable()
export class UserService {

  constructor(private httpClient: HttpClient) { }

  register(user: RegisterModel): Observable<RegisterModel> {
    let url = "http://localhost:50890/api/user/register";
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post<RegisterModel>(url, user, options);
  }

  login(user: LoginModel): Observable<LoginModel> {
    let url = "http://localhost:50890/api/user/authenticate";
    let options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post<LoginModel>(url, user, options);
  }

}
