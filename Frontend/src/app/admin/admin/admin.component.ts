import {Component, OnInit, Sanitizer} from '@angular/core';
import {Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from "../../login/shared/employee.service";
import {Employee} from '../../login/shared/employee.model';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {


  constructor(private employeeService: EmployeeService, private router: Router, private formBuilder: FormBuilder) {

  }

  ngOnInit() {
  }

  close() {
    this.router.navigateByUrl('/customers');
  }

  createEmployee() {
    this.router.navigateByUrl('employees/create');
  }

  }

