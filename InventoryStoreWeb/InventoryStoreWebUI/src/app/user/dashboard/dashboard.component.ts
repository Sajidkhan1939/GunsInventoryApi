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
      Manufacturer: ['', [Validators.required]],
      Model: ['', [Validators.required]],
      Caliber: ['', [Validators.required]],
      CountryOfOrigin: ['', [Validators.required]],
      DateOfPurchase: ['', [Validators.required]],
      PurchasedStoreName: ['', [Validators.required]],
      SerialNo: ['', [Validators.required]],
      Codition: ['', [Validators.required]],
      StoredLocation: ['', [Validators.required]],
      PurchasePrice: ['', [Validators.required]],
      InsuranceValue: ['', [Validators.required]],
      Action: ['', [Validators.required]],
      OAL: ['', [Validators.required]],
      Weight: ['', [Validators.required]],
      TwistRate: ['', [Validators.required]],
      LengthOfPull: ['', [Validators.required]],
      BarrelLength: ['', [Validators.required]],
      BarrelFinish: ['', [Validators.required]],
      GunFinish: ['', [Validators.required]],
      AmmoTypeUsed: ['', [Validators.required]],
      LastMaintenanceDate: ['', [Validators.required]],
      GeneralNotes: ['', [Validators.required]],
      Sights: ['', [Validators.required]],
      Scope: ['', [Validators.required]],
      TypeOfScope: ['', [Validators.required]],
      Laser: ['', [Validators.required]],
      TypeOfLaser: ['', [Validators.required]],
      StockType: ['', [Validators.required]],
      TriggerType: ['', [Validators.required]],
      TriggerPull: ['', [Validators.required]],
      CostOfFirearm: ['', [Validators.required]],
      AnyOtherAccessories: ['', [Validators.required]],
      PreBan: ['', [Validators.required]],
      NFAApprovalDate: ['', [Validators.required]],
      NFAPaperworkScanned: ['', [Validators.required]],
      NameOfTrustOrCorporation: ['', [Validators.required]],
      LocalPoliceApproval: ['', [Validators.required]],
      LocalPoliceStationName: ['', [Validators.required]],
      ModifiedBy: ['', [Validators.required]],
      ModifiedDate: ['', [Validators.required]],
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
