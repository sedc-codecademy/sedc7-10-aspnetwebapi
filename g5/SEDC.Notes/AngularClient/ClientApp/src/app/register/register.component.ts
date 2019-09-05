import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { RegisterModel } from '../models/register.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm = new FormGroup({
    username: new FormControl(''),
    password: new FormControl(''),
    confirmPassword: new FormControl(''),
    firstName: new FormControl(''),
    lastName: new FormControl(''),
  })

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() { }

  onSubmit() {
    let username = this.registerForm.value.username;
    let password = this.registerForm.value.password;
    let confirmPassword = this.registerForm.value.confirmPassword;
    let firstName = this.registerForm.value.firstName;
    let lastName = this.registerForm.value.lastName;

    let model = new RegisterModel();
    model.username = username;
    model.password = password;
    model.confirmPassword = confirmPassword;
    model.firstName = firstName;
    model.lastName = lastName;

    this.register(model);
  }

  register(user: RegisterModel) {
    this.userService.register(user).subscribe({
      next: data => {
        this.router.navigate(['/login'])
      },
      error: err => console.log(err.error),
      complete: () => console.log('register successfoul')
    })
  }

}
