import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';
import { NoteService } from '../services/note.service';
import { Note } from '../models/note.model';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-notes',
  templateUrl: './notes.component.html',
  styleUrls: ['./notes.component.css']
})
export class NotesComponent implements OnInit {

  token: string
  user: string
  userId: string
  isInCreateMode: boolean = false

  notes: Note[]

  createNoteForm = new FormGroup({
    text: new FormControl('', Validators.required),
    color: new FormControl('', Validators.required),
    tag: new FormControl('', Validators.required)
  })

  constructor(private noteService: NoteService, private router: Router) { }

  ngOnInit() {
    let token = localStorage.getItem('token')
    if (!token) {
      this.router.navigate(['/login']);
    }
    this.token = token;

    this.user = localStorage.getItem('user');
    this.userId = localStorage.getItem('id');

    this.getNotes(token);
  }

  getNotes(token: any) {
    this.noteService.getNotes(token).subscribe({
      next: data => {
        this.notes = data
      },
      error: err => {
        console.log(err.error)
      }
    })
  }

  onSubmit() {
    let text = this.createNoteForm.value.text;
    let color = this.createNoteForm.value.color;
    let tag = this.createNoteForm.value.tag;

    let model = new Note()
    model.text = text
    model.color = color
    model.tag = tag
    model.userId = +this.userId

    this.createNote(model);
  }

  createNote(note: Note) {
    this.noteService.createNote(this.token, note).subscribe({
      error: (err) => {
        this.getNotes(this.token)
      },
      complete: () => {
        this.getNotes(this.token)
      }
    })
  }

  openCreateMode() {
    if (this.isInCreateMode === false) {
      this.isInCreateMode = true;
      return;
    }

    if (this.isInCreateMode === true) {
      this.isInCreateMode = false;
      return;
    }
  }

  deleteNote(id: string) {
    this.noteService.deleteNote(this.token, id).subscribe({
      error: (err) => {
        this.getNotes(this.token)
      },
      complete: () => {
        this.getNotes(this.token)
      }
    })
  }

  resolveTag(tagNumber: number) {
    if (tagNumber === 1) {
      return "Work";
    }
    if (tagNumber === 2) {
      return "Education";
    }
    if (tagNumber === 3) {
      return "Misc";
    }
    if (tagNumber === 4) {
      return "Other";
    }
  }

}
