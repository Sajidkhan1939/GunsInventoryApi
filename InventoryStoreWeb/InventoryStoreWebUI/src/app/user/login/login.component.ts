import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { UserservicesService } from '../services/userservices.service';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthserviceService } from 'src/app/services/authservice.service';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  LoginForm: any = FormGroup
  public token:any

  constructor(private formbuilder: FormBuilder, private myobj: UserservicesService, private http: HttpClient, private route: Router,private authservice:AuthserviceService) {
    this.LoginForm = formbuilder.group({
      Username: ['', [Validators.required]],
      Password: ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
  }
  public  login(value: NgForm) {
    var formdata: any = new FormData();
    formdata.append("Username", this.LoginForm.get('Username').value);
    formdata.append("Password", this.LoginForm.get('Password').value);
    this.myobj.login( formdata).subscribe((result: any) => {
      if(result.Access_token!==null){
        localStorage.setItem('token',result.Access_token)
        alert("succesfully Login")
      }
      else{
        console.log("invalid user")
      }
      
    
      //  const obj= result.Access_token;
      //  this.token=obj
     
      // if(obj!=null){
      //   this.route.navigate(['/dashboard'])
      // }
      // else
      // {
      //   this.route.navigate(['/signup'])
      // }
      
    })
  }
  
}
