import { Component, OnInit } from '@angular/core';
import { TicketModel } from "./ticket.model";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Router } from "@angular/router";

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {

  allNumbers: number[] = [];
  ticketModel: TicketModel;
  requestHeaders: HttpHeaders; 

  constructor(private http: HttpClient,
              private router: Router) { }

  ngOnInit() {
    let token = localStorage.getItem("token");
    if (!token)
      this.router.navigate(["/login"]);

    this.requestHeaders = new HttpHeaders({
      "Content-Type":"application/json",
      "Authorization": `Bearer ${token}`
    });

    this.ticketModel = new TicketModel();
    this.ticketModel.pickedNumbers = [];

    for (let index = 1; index < 38; index++) {
      this.allNumbers.push(index);
    }
  }

  onSubmit(){
    this.http.post("http://localhost:60884/api/ticket", this.ticketModel,
                  {headers: this.requestHeaders})
              .subscribe(resp => console.log(resp),
                          error => console.log(error));
  }

  pickedNumbersChanged(event){
    let value = event.target.value;
    let checked = event.target.checked;
    let index = this.ticketModel.pickedNumbers.indexOf(value);

    if (!checked) {
        this.ticketModel.pickedNumbers.splice(index, 1);
    }
    else {
      this.ticketModel.pickedNumbers.push(value);
    }
  }

}
