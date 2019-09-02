import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from "./home/home.component";
import { RegisterComponent } from "./register/register.component";
import { LoginComponent } from "./login/login.component";
import { TicketComponent } from "./ticket/ticket.component";


const routes: Routes = [
  { path: "register", component: RegisterComponent},
  { path: "login", component: LoginComponent},
  { path: "ticket", component: TicketComponent},
  { path: "", component: HomeComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
