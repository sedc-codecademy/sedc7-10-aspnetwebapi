import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-items',
  templateUrl: './items.component.html',
  styleUrls: ['./items.component.scss']
})
export class ItemsComponent implements OnInit {
  public todoItems: any[];
  public user: string;

  constructor(private http: HttpClient, private toastr: ToastrService, private router: Router) { }

  ngOnInit() {
    let token = localStorage.getItem('token');

    if (!token) {
      this.router.navigate(['/login']);
    }
    
    let bearer = 'Bearer ' + token;
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': bearer
    })

    this.user = localStorage.getItem('user');
    this.http.get("https://localhost:44399/api/todo", { headers: headers }).subscribe((data: any[]) => {
      this.todoItems = data;
    }, (error) => {
      this.toastr.error(error.error.message);
    });
  }

}
