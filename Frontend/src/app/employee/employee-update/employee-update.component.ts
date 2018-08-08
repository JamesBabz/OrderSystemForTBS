import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';

import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';

@Component({
  selector: 'app-employee-update',
  templateUrl: './employee-update.component.html',
  styleUrls: ['./employee-update.component.css']
})
export class EmployeeUpdateComponent implements OnInit {

  employee: Employee;
  editEmployee: Employee;
  isSaved = false;
  changes = false;

  constructor(private employeeService: EmployeeService) {
  }

  ngOnInit() {
  }

  updateEmployee() {
    if (this.changes) {
      this.employeeService.updateEmployeeById(this.employee.id, this.editEmployee).subscribe(Employee => {
        this.employee = Employee;
        this.editEmployee = Object.assign({}, this.employee);
        this.isSaved = true;
      });
    }
    this.changes = false;
  }

}
