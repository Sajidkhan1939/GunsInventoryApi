import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Token } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {
  url_addguns="https://localhost:44346/api/Inventory/Add"

  constructor(private http:HttpClient) { }
  gettoken(){
    return localStorage.getItem('token')
  }
  addguns(data:any) {
    
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${this.gettoken}`
    });

    const requestOptions = { headers: headers };
    return  this.http.post(this.url_addguns,data,requestOptions)
  }
 

}
