import { Component, OnInit } from '@angular/core';
import { RegisterModel } from './register.model';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerModel: RegisterModel

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    this.registerModel = new RegisterModel();
  }

  public onSubmit() {
    this.http.post('http://localhost:61661/api/users/register', this.registerModel).subscribe((data: any) => {
      this.router.navigate(['/login']);
    }, error => console.log(error));
  }

}
