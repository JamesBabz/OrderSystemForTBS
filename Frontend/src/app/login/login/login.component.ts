import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from '../shared/login.service';
import {Employee} from '../shared/employee-model';

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

  constructor(private router: Router, private loginService: LoginService) {
    const currentUser = JSON.parse(localStorage.getItem('currentUser'));
    this.username = currentUser && currentUser.username;
  }

  ngOnInit() {
    this.loginService.logout();
    this.showHeader(false);
  }

  login() {
    this.loading = true;
    this.loginService.login(this.model.username, this.model.password)
      .subscribe(
        success => {
          this.router.navigate(['/customers']);
        },
        error => {
          this.errormessage = 'Wrong username or password!';
          this.loading = false;
        });
    this.showHeader(true);
  }

  private showHeader(b: boolean) {
    if (b) {
      document.getElementById('headerContainer').style.display = 'block';
    } else {
      document.getElementById('headerContainer').style.display = 'none';
    }
  }

}
