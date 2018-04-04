import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from '../shared/login.service';
import {Employee} from '../shared/employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {toString} from '@ng-bootstrap/ng-bootstrap/util/util';
import {isSuccess} from '@angular/http/src/http_utils';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = Employee;
  loading = false;
  username: string;
  errormessage = '';
  currentUser;
  localStorageId;
  localStorageBool;

  constructor(private router: Router, private loginService: LoginService) {
  }

  ngOnInit() {
    this.showHeader(false);
    this.loginService.logout();
  }

  login() {
    this.loading = true;
    this.loginService.login(this.model.username, this.model.password)
      .subscribe(
        success => {
          setTimeout(() => success, 4000);
          this.localStorageId = (localStorage.getItem('currentUser').split(',')[0].substr(6));
          this.localStorageBool = toString(localStorage.getItem('currentUser').split(',')[1].substr(16));

          console.log(this.localStorageBool);

          if(this.localStorageBool == "true")
          {
            this.router.navigateByUrl('/passwordreset/' + this.localStorageId);
          }
          else if(this.localStorageBool == "false")
          {
            this.router.navigateByUrl('/customers');
          }

          this.showHeader(true);

        },
        error => {
          this.errormessage = 'Wrong username or password!';
          this.loading = false;
          this.showHeader(false);
        });
  }

  private showHeader(b: boolean) {
    if (b) {
      document.getElementById('headerContainer').style.display = 'flex';
    } else {
      document.getElementById('headerContainer').style.display = 'none';
    }
  }

}
