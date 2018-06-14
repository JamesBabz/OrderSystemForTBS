import {Component, OnInit, Sanitizer} from '@angular/core';
import {Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from "../../login/shared/employee.service";
import {Employee} from '../../login/shared/employee.model';


@Component({
  selector: 'app-employee-create',
  templateUrl: './employee-create.component.html',
  styleUrls: ['./employee-create.component.css']
})
export class EmployeeCreateComponent implements OnInit {

  employeeGroup: FormGroup;

  constructor(private employeeService: EmployeeService, private router: Router, private formBuilder: FormBuilder) {
    this.employeeGroup = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      username: ['', Validators.required],
      password: [Math.random().toString(36).substring(7)],
      colorCode: ['']
    });
  }

  ngOnInit() {
  }

  close() {
    this.router.navigateByUrl('/customers');
  }

  createEmployee() {
    const values = this.employeeGroup.value;
    const employee: Employee = {

      firstname: values.firstname,
      lastname: values.lastname,
      username: values.username,
      password: values.password,
      colorCode: values.colorCode,
    };

    this.employeeService.createEmployee(employee).subscribe(newEmployee => {
      this.router.navigateByUrl('/customers');
    });
  }
}
