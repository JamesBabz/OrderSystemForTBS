import {Component, Input, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {LoginService} from '../shared/login.service';
import {Employee} from '../shared/employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {toString} from '@ng-bootstrap/ng-bootstrap/util/util';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NotificationsService} from 'angular2-notifications';
import {EmployeeService} from '../shared/employee.service';
import {SharedService} from '../../shared/shared.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


  id: number;

  @Input()
  employee: Employee;


  model: any = Employee;
  loading = false;
  IsHidden;
  errormessage = '';
  currentUser;
  localStorageId;
  localStorageBool;

  employeeGroup: FormGroup;

  private _service: NotificationsService;
  private _employeeService: EmployeeService;
  private _sharedService: SharedService;

  constructor(private sharedService: SharedService, private formBuilder: FormBuilder, private router: Router, private notifiService: NotificationsService, private employeeService: EmployeeService, private loginService: LoginService) {
    this._service = notifiService;
    this._employeeService = employeeService;
    this._sharedService = sharedService;



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

          this.updateEmployee();

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

  updateEmployee() {
    const date = new Date;
    const employee: Employee = {
      id: this.localStorageId,
      lastLogin: new Date(),

    };

    this.employeeService.updateEmployeeById(employee.id, employee).subscribe(Employee => {
      this.employee = Employee;

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

