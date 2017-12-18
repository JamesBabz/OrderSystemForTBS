import {Injectable} from '@angular/core';
import {Router, CanActivate} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {LoginService} from '../../shared/login.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private loginService: LoginService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {


    // if (req.url.startsWith('https://outlook.office.com')) {
    //   return next.handle(req);
    // }


    // get the token from a service

    const token: string = this.loginService.token;

    // add it if we have one

    if (token) {
      req = req.clone({headers: req.headers.set('Authorization', 'Bearer ' + token)});
    }

    // if this is a login-request the header is

    // already set to x/www/formurl/encoded.

    // so if we already have a content-type, do not

    // set it, but if we don't have one, set it to

    // default --> json

    if (!req.headers.has('Content-Type')) {
      req = req.clone({headers: req.headers.set('Content-Type', 'application/json')});
    }

    // setting the accept header
    req = req.clone({headers: req.headers.set('', 'application/json')});

    req = req.clone({headers: req.headers.set('Accept', 'application/json')});
    return next.handle(req);
  }
}


