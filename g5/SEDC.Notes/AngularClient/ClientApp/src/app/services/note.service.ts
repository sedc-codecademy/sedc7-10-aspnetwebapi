import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Note } from '../models/note.model';

@Injectable()
export class NoteService {

  constructor(private httpClient: HttpClient) { }

  getNotes(token: any): Observable<Note[]> {
    let bearer = 'Bearer ' + token;
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': bearer
    })

    let url = "http://localhost:50890/api/note";

    return this.httpClient.get<Note[]>(url, { headers })
  }

  createNote(token: any, note: Note): Observable<Note> {
    let bearer = 'Bearer ' + token;
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': bearer
    })

    let url = "http://localhost:50890/api/note";

    return this.httpClient.post<Note>(url, note, { headers })
  }

  deleteNote(token: any, id: string): Observable<string> {
    let bearer = 'Bearer ' + token;
    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': bearer
    })

    let url = "http://localhost:50890/api/note/" + id;
    return this.httpClient.delete<string>(url, { headers })
  }

}
