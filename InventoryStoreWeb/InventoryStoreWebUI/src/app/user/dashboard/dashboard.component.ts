import { Component, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthserviceService } from 'src/app/services/authservice.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  AddGuns: any

  constructor(private formbuilder: FormBuilder,private obj:AuthserviceService, private route: Router) {
    this.AddGuns = formbuilder.group({
      Name: ['', [Validators.required]],
      FirearmType: ['', [Validators.required]],
      LocalPoliceStationName: ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
    
  }
 
  ADDGuns(value:NgForm){
    console.log(value.value);
    this.obj.addguns(this.AddGuns.value).subscribe((result:any)=>{
      console.warn(result);
      console.log(result);
    })
    
  }


}
