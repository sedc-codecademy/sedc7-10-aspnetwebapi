import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { LoginModel } from '../models/login.model';
import { UserService } from '../services/user.service';
import { RegisteredUser } from '../models/registered-user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  loginForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
  })

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() { }

  onSubmit() {
    let username = this.loginForm.value.username;
    let password = this.loginForm.value.password;

    let model = new LoginModel();
    model.username = username;
    model.password = password;

    this.login(model);
  }

  login(user: LoginModel) {
    this.userService.login(user).subscribe({
      next: (data: any) => {
        localStorage.setItem("token", data.token);
        localStorage.setItem("id", data.id);
        localStorage.setItem("user", data.fullName);
        this.router.navigate(['/notes'])
      },
      error: err => {
        this.router.navigate(['/register'])
      },
      complete: () => console.log('login successful')
    })
  }

}
