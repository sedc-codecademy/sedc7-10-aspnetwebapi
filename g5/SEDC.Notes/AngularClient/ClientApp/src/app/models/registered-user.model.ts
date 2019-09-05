import { Note } from "./note.model";

export class RegisteredUser {
  id: string
  username: string
  firstName: string
  lastName: string
  fullName: string
  token: string
  noteList: Note[]
}
