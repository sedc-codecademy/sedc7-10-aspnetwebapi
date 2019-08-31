import { Component, OnInit } from '@angular/core';
import {LoginModel} from './login.model';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginModel: LoginModel;

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) {
  }

  ngOnInit() {
    this.loginModel = new LoginModel();
  }

  public onSubmit() {
    this.http.post("https://localhost:44399/api/user/authenticate", this.loginModel).subscribe((data: any) => {
      localStorage.setItem("token", data.token);
      localStorage.setItem("user", data.fullName);
      this.router.navigate(['/items'])
    }, (error) => {
      this.toastr.error(error.error.message);
    });
  }

}
