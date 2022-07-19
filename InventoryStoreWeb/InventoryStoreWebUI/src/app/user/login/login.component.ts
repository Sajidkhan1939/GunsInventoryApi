import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { UserservicesService } from '../services/userservices.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  LoginForm: any = FormGroup
  responsedata:any

  constructor(private formbuilder: FormBuilder, private myobj: UserservicesService, private http: HttpClient, private route: Router) {
    this.LoginForm = formbuilder.group({
      Username: ['', [Validators.required]],
      Password: ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
  }
  login(value: NgForm) {
    var formdata: any = new FormData();
    formdata.append("Username", this.LoginForm.get('Username').value);
    formdata.append("Password", this.LoginForm.get('Password').value);
    this.myobj.login( formdata).subscribe((result: any) => {
      console.log(result)
      if(result!=null){
        this.responsedata=result;
      }
    })
  }
}
