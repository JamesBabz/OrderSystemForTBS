import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from '../shared/login.service';
import {Employee} from '../shared/employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {toString} from '@ng-bootstrap/ng-bootstrap/util/util';
import {FormBuilder, FormGroup} from '@angular/forms';
import {NotificationsService} from 'angular2-notifications';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = Employee;
  loading = false;
  IsHidden;
  errormessage = '';
  currentUser;
  localStorageId;
  localStorageBool;

  private _service: NotificationsService;

  constructor(private router: Router, private notifiService: NotificationsService, private loginService: LoginService) {
    this._service = notifiService;
  }



  ngOnInit() {
    this.showHeader(false);
    this.loginService.logout();



  }

  login(employee: Employee) {
    this.loading = true;
    this.loginService.login(this.model.username, this.model.password)
      .subscribe(
        success => {
          this.showHeader(true);
          this.localStorageId = toNumber(localStorage.getItem('currentUser').split(',')[0].substr(6));
          this.localStorageBool = toString(localStorage.getItem('currentUser').split(',')[1].substr(16));


          if(this.localStorageBool == "true")
          {
            this.router.navigateByUrl('/passwordreset/' + this.localStorageId);
          }
          else if(this.localStorageBool == "false")
          {
            this.router.navigateByUrl('/customers');

          }



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

