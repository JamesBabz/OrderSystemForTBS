import {Component, EventEmitter, Input, OnInit, Output, Sanitizer} from '@angular/core';
import {ActivatedRoute, Router} from '@angular/router';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {EmployeeService} from "../../login/shared/employee.service";
import {Employee} from '../../login/shared/employee.model';
import {Customer} from '../../customers/shared/customer.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  employees: Employee[];
  id: number;
  name: string;
  colorCode: string;
  colorDone = false;

  editEmployee: Employee;
  employeeGroup: FormGroup;

  @Input()
  employee: Employee;
  @Output()
  eDeleted = new EventEmitter();


  constructor(private employeeService: EmployeeService, private router: Router, private formBuilder: FormBuilder, private route: ActivatedRoute) {
    this.employeeGroup = this.formBuilder.group({
      id:[''],
      firstname: ['', ],
      lastname: ['', ],
      colorCode: ['']
    });
  }

  updateEmployee() {
    const values = this.employeeGroup.value;
    const employee: Employee = {

      id: this.id,
      firstname: values.firstname,
      lastname: values.lastname,
      colorCode: values.colorCode,
    };


    this.employeeService.updateEmployeeById(this.id, employee).subscribe(Employee => {
      this.employee = Employee;
    });
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

  getInfo(employee: Employee)
  {
    this.id = employee.id;
    this.name = employee.firstname + " " + employee.lastname;
    this.colorCode = employee.colorCode;
  }

  setColors() {
    const spans = document.getElementsByClassName('colorBox');
    for (let i = 0; i < spans.length; i++) {
      spans.item(i).setAttribute('style', 'background-color: ' + spans.item(i).getAttribute('class').substr(0, 7));
    }
    this.colorDone = true;
  }

  deleteEmployeeById() {
    this.employeeService.deleteEmployeeById(this.id).subscribe(Employee => {
      this.showEmployees();
      this.id = null;
    });
  }



}

