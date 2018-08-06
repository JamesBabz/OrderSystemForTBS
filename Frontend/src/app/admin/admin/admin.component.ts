import {Component, EventEmitter, Input, OnInit, Output, Sanitizer} from '@angular/core';
import {Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from "../../login/shared/employee.service";
import {Employee} from '../../login/shared/employee.model';
import {Customer} from '../../customers/shared/customer.model';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  employees: Employee[];



  constructor(private employeeService: EmployeeService, private router: Router, private formBuilder: FormBuilder) {
  }

  ngOnInit() {
  this.showEmployees();
  }

  createEmployee() {
    this.router.navigateByUrl('employees/create');
  }

  showEmployees() {
    this.employeeService.getEmployees().subscribe(Employees => this.employees = Employees);
  }

  details(employee: Employee, event) {
    if (event.target.tagName === 'I') {
      return;
    }
    this.router.navigateByUrl('/employee/' + employee.id);
  }
}

