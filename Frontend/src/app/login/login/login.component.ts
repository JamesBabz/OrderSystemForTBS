import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from '../shared/login.service';
import {Employee} from '../shared/employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {toString} from '@ng-bootstrap/ng-bootstrap/util/util';


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
  localStorageRole;

  constructor(private router: Router, private loginService: LoginService) {
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
      this.checkIfAdmin();
    } else {
      document.getElementById('headerContainer').style.display = 'none';
    }
  }

  public checkIfAdmin() {
    this.localStorageRole = toString(localStorage.getItem('currentUser').split('"')[7].substr(0));

    if (localStorage.getItem('currentUser') != null && this.localStorageRole === 'User') {
      var adminLink = document.getElementById("admin");
      (<HTMLElement>adminLink).remove();
      return true;
    }
    return false;
  }
}

