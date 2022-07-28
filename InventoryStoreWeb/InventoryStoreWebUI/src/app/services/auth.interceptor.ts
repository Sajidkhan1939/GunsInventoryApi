import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<any>, next: HttpHandler){
    let token= localStorage.getItem('token');
    // request = this.addtoken(request,token)
    return next.handle(request);
  }
  private addtoken(request: HttpRequest<any>, token:string){
    return request.clone({
      setHeaders: {'Authorization': `Bearer ${token}`}
    })

  }
}
