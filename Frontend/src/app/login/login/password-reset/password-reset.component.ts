import {Component, OnInit, Sanitizer} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from '../../shared/employee.service';
import {Employee} from '../../shared/employee.model';


@Component({
  selector: 'app-employee-create',
  templateUrl: './password-reset.component.html',
  styleUrls: ['./password-reset.component.css']
})
export class PasswordResetComponent implements OnInit {

  public token: string;

  employee: Employee;
  editPassword: Employee;
  isSaved = false;
  changes = false;


  constructor(private employeeService: EmployeeService, private router: Router, private route: ActivatedRoute, private formBuilder: FormBuilder) {

  }

  ngOnInit() {
    this.route.paramMap
      .switchMap(params => this.employeeService
        .getEmployeeById(+params.get('id')))
      .subscribe(Employee => {
        this.employee = Employee;
        this.editPassword = Object.assign({}, this.employee);
      });
  }

  close() {
    this.router.navigateByUrl('/customers');
  }

  updateEmployee() {
    if (this.changes) {
      this.employeeService.updateEmployeeById(this.employee.id, this.editPassword).subscribe(Employee => {
        this.employee = Employee;
        this.editPassword = Object.assign({}, this.employee);
        this.isSaved = true;
        this.logout();
      });
    }

    this.changes = false;
  }

  logout(): void {
    // clear token remove user from local storage to log user out
    this.token = null;
    localStorage.removeItem('currentUser');
  }

}

