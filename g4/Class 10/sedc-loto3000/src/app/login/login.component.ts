import { Component, OnInit } from '@angular/core';
import { FormBuilder } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm;

  constructor(private formBuilder: FormBuilder,
    private http: HttpClient,
    private router: Router) {
      this.loginForm = this.formBuilder.group({
        email: '',
        password: ''
      });
     }

  ngOnInit() {
  }

  onSubmit(loginFormData) {
    console.log(loginFormData);
    this.http.post("http://localhost:60884/api/user/authenticate", loginFormData,
                {observe: "response"})
              .subscribe(resp => 
                {
                  console.log(resp.headers.get("Authorization"));
                  localStorage.setItem("token", resp.headers.get("Authorization"));
                  this.router.navigate(["/ticket"]);
                },
                        error => console.log(error));
  }

}
