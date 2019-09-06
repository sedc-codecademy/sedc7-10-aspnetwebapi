import { Component, OnInit } from '@angular/core';
import { LoginModel } from './login.model';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public loginModel: LoginModel;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.loginModel = new LoginModel();
  }

  public onSubmit() {
    this.http.post('http://localhost:61661/api/users/authenticate', this.loginModel).subscribe((data: any) => {
      localStorage.setItem("token", data.token);
      localStorage.setItem("user", data.fullName);
      this.router.navigate(['/notes']);
    }, error => console.log(error));
  }

}
