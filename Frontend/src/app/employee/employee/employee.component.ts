import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';
import {ActivatedRoute} from '@angular/router';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  @Input()
  employee: Employee;
  @Output()
  eDeleted = new EventEmitter();

  modalString: string;


  constructor(private employeeService: EmployeeService, private route: ActivatedRoute) {
  }


  ngOnInit() {

  }



  deleteEmployeeById() {
    this.employeeService.deleteEmployeeById(this.employee.id).subscribe(Employee => {
      this.eDeleted.emit(Employee);
  })
    this.refresh();
  }

  refresh() {
    this.route.paramMap
      .switchMap(params => this.employeeService.getEmployeeById(+params.get('id')))
      .subscribe(Employee => this.employee = Employee);
  }




}
