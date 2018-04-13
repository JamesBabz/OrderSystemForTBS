import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Employee} from '../../login/shared/employee.model';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  @Input()
  employee: Employee;


  constructor() {
  }


  ngOnInit() {
  }

}
