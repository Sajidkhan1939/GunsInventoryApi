import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserservicesService {
  user_Register="https://localhost:44346/api/User/Register"
  user_Login="https://localhost:44346/api/Token"

  constructor(private http:HttpClient) { }
  RegisterUser(data:any){
    return this.http.post(this.user_Register,data)
  }
  login(data:any){
    return this.http.post(this.user_Login,data)
  }
}
