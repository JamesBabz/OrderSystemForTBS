import {Component, OnInit, Sanitizer} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from '../../shared/employee.service';
import {Employee} from '../../shared/employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {matchPassword} from './ValidatorFile';


@Component({
  selector: 'app-employee-create',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {

  public token: string;
  inputPassword: string;

  employee: Employee;
  errormessage = 'Adganskoderne skal være ens';
  employeeGroup: FormGroup;

  constructor(private employeeService: EmployeeService, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder) {
    const localStorageId = toNumber(localStorage.getItem('currentUser').split(',')[0].substr(6));
    this.employeeGroup = this.formBuilder.group({
      id:[localStorageId],
      firstname: ['', ],
      lastname: ['', ],
      username: ['', ],
      password: ['', Validators.required],
      confirmPassword:['', [Validators.required, matchPassword()]],
      colorCode: ['']
    });
  }


  ngOnInit() {
  }

  close() {
    this.router.navigateByUrl('/customers');
  }

  updateEmployee() {
    const values = this.employeeGroup.value;
    const employee: Employee = {

      id: values.id,
      firstname: values.firstname,
      lastname: values.lastname,
      username: values.username,
      password: values.confirmPassword,
      colorCode: values.colorCode,
    };

    this.employee = employee;

      this.employeeService.updateEmployeeById(this.employee.id, employee).subscribe(Employee => {
        this.employee = Employee;
        this.logout();
      });
  }

  logout(): void {
    // clear token remove user from local storage to log user out
    this.token = null;
    localStorage.removeItem('currentUser');
    this.router.navigateByUrl('/login');
  }

}

