import { Injectable } from '@angular/core';
import {
  HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest,
  HttpResponse
} from '@angular/common/http';
import {LoginService} from '../../shared/login.service';
import 'rxjs/add/operator/do';



@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  private req: HttpRequest<any>;

  constructor(private loginService: LoginService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler) {

    return next.handle(this.req).do((event: HttpEvent<any>) => {
      if (event instanceof HttpResponse) {
        // do stuff with response if you want
      }
    }, (err: any) => {
      if (err instanceof HttpErrorResponse) {
        if (err.status === 401) {
          this.loginService.logout();
        }
      }
    });
  }}


