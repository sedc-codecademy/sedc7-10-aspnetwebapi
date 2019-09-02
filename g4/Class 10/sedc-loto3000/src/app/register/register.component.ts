import { Component, OnInit } from '@angular/core';
import { FormBuilder } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm;

  constructor(private formBuilder: FormBuilder,
              private http: HttpClient,
              private router: Router) {

    this.registerForm = this.formBuilder.group({
      fullName: '',
      email: '',
      password: ''
    });

   }

  ngOnInit() {
  }

  onSubmit(registerFormData) {
    console.log(registerFormData);
    this.http.post("http://localhost:60884/api/user", registerFormData)
              .subscribe(data => this.router.navigate(["/ticket"]),
                        error => console.log(error));
  }

}
