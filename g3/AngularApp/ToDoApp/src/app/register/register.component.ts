import { Component, OnInit } from '@angular/core';
import { RegisterModel } from './register.model';
import { HttpClient } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  private registerModel: RegisterModel;

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    this.registerModel = new RegisterModel();
  }

  public onSubmit(){
    this.http.post("https://localhost:44399/api/user/register", this.registerModel).subscribe((data) => {
      this.router.navigate(['/login']);
    }, (error) => {
      this.toastr.error(error.error.message, "Error");
    });
  }

}
