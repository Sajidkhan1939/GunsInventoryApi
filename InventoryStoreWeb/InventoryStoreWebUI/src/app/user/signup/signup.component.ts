import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import {UserservicesService} from 'src/app/user/services/userservices.service'

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {
  RegForm: any = FormGroup
   message:any
  user:any
  constructor(private formbuilder:FormBuilder, private myobj:UserservicesService ,private route:Router) {
  
    this.RegForm=formbuilder.group({
      FirstName:['',[Validators.required]],
      LastName:['',[Validators.required]],
      Email:['',[Validators.email]],
      Password:['',[Validators.required]],
      State:['',[Validators.required]],
      City:['',[Validators.required]],
      Country:['',[Validators.required]],
    })
   }

  ngOnInit(): void {
  }
  signup(value:NgForm){
    console.log(value.value);
    this.myobj.RegisterUser(this.RegForm.value).subscribe((result:any)=>{
      console.warn(result);
      console.log(result);
      this.route.navigate(['/user/login'])
      
    })
    
  }

}
