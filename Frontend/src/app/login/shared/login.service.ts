import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Http, Response} from '@angular/http';
import 'rxjs/add/operator/map';
import {Employee} from './employee.model';


@Injectable()
export class LoginService {
  public token: string;
  employee: Employee;
  passwordReset: string;
  IsAdmin: string;

  constructor(private http: Http) {
    // set token if saved in local storage
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.token = currentUser && currentUser.token;
  }

  login(username: string, password: string): Observable<boolean> {
    return this.http.post('http://localhost:55000/api/login', {username: username, password: password})
      .map((response: Response) => {
        // login successful if there's a jwt token in the response
        const token = response.json() && response.json().token;
        this.passwordReset = response.json().passwordreset;
        this.IsAdmin = response.json().isadmin;
        const id = response.json().id;
        if (token) {
          // set token property
          this.token = token;
          // store username and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentUser', JSON.stringify({id: id, passwordreset: this.passwordReset, isadmin: this.IsAdmin ,  username: username, token: token}));

          // return true to indicate successful login
          return true;

        } else {
          // return false to indicate failed login
          return false;
        }
      });
  }

  logout(): void {
    // clear token remove user from local storage to log user out
    this.token = null;
    localStorage.removeItem('currentUser');
  }
}
