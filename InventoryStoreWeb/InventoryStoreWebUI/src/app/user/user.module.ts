import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { SignupComponent } from './signup/signup.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import {HttpClientModule} from  '@angular/common/http';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    UserComponent,
    SignupComponent,
    LoginComponent,
    DashboardComponent,
  ],
  imports: [
    CommonModule,
    UserRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule

  ]
})
export class UserModule { }
