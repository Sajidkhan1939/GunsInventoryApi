import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {
  url_addguns="https://localhost:44346/api/Inventory/Add"

  constructor(private http:HttpClient) { }
  addguns(data:any) {
    let auth_token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI2MmJjNTQ4ZjJhNjc1N2Y4YjViY2I5ZWMiLCJ1bmlxdWVfbmFtZSI6IjYyYmM1NDhmMmE2NzU3ZjhiNWJjYjllYyIsImp0aSI6IjgzNjE2MWRkLWQyMjUtNDQwZi05NjFiLWExYzU2MjQwNWVhNiIsImlhdCI6MTY1NjY3MTYxOSwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiR3VuQXBwVXNlciIsIm5iZiI6MTY1NjY3MTYxOSwiZXhwIjoxNjU3MTAzNjE5LCJpc3MiOiJHdW5TdG9yZUlzc3VlciIsImF1ZCI6IkFQSUZvck1vYiJ9.YF7V1ga7WV467bmZ2zqs8pWt3IL1-aoNLZzMpcH56ms";
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${auth_token}`
    });

    const requestOptions = { headers: headers };
    return   this.http.post(this.url_addguns,data,requestOptions)
  
  }
}
