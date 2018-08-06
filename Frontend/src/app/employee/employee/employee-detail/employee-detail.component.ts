import {Component, OnInit, Sanitizer} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from '../../../login/shared/employee.service';
import {Employee} from '../../../login/shared/employee.model';



@Component({
  selector: 'app-employee-detail',
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {
  employee: Employee;
  editEmployee: Employee;


  employeeGroup: FormGroup;


  constructor(private employeeService: EmployeeService, private route: ActivatedRoute, private router: Router, private formBuilder: FormBuilder) {



  }

  ngOnInit() {
    this.route.paramMap
      .switchMap(params => this.employeeService
        .getEmployeeById(+params.get('id')))
      .subscribe(Employee => {
        this.employee = Employee;
        this.editEmployee = Object.assign({}, this.employee);
      });
  }

  close() {
    this.router.navigateByUrl('/customers');
  }


}
