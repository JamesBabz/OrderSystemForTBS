import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';
import {Employee} from './employee-model';
import {HttpClient} from '@angular/common/http';
import { Http, Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class LoginService {
    public token: string;

  constructor(private http: HttpClient) {
  }


  login(username: string, password: string): Observable<boolean> {
    return this.http.post('http://localhost:55000/api/login', {username: username, password: password});
      }
}
