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
  errormessage = '';

  constructor(
    private router: Router,
    private loginService: LoginService) { }

  ngOnInit() {

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
  }
}
