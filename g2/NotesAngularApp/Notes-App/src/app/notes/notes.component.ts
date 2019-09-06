import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.scss']
})
export class NotesComponent implements OnInit {
  public notes: any[];
  public user: string;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    const token = localStorage.getItem('token');

    if (!token) {
      this.router.navigate(['/login']);
    }

    const bearer = 'Bearer ' + token;
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': bearer
    });

    this.user = localStorage.getItem('user');

    this.http.get('http://localhost:61661/api/notes', {headers: headers}).subscribe((data: any[]) => {
      this.notes = data;
    }, error => console.log(error));
  }

}
