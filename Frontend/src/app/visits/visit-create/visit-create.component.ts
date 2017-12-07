import {Component, Input, OnInit} from '@angular/core';
import {VisitService} from '../shared/visit.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {CustomerService} from '../../customers/shared/customer.service';
import {Customer} from '../../customers/shared/customer.model';
import {Visit} from '../shared/visit.model';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';

@Component({
  selector: 'app-visit-create',
  templateUrl: './visit-create.component.html',
  styleUrls: ['./visit-create.component.css']
})
export class VisitCreateComponent implements OnInit {

  @Input()
  customers: Customer[];
  visitGroup: FormGroup;
  customer;
  model: NgbDateStruct;
  date: {year: number, month: number, day: number};
  employee: Employee;
  constructor(private visitService: VisitService, private router: Router, private formBuilder: FormBuilder, private customerService: CustomerService, private employeeService: EmployeeService) {
    this.customer = Customer;
    this.visitGroup = this.formBuilder.group({
      customerSelector: new FormControl(this.customer === null ? '' : this.customer.id, Validators.required),
      title: ['', Validators.required],
      description: ['', Validators.required],
      datePicked: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.customerService.getCustomers().subscribe(c => this.customers = c);
    this.employeeService.getEmployee().subscribe(Employee => this.employee = Employee);
  }

  cancel() {
    this.router.navigateByUrl('/customers');
  }

  createVisit() {
    const newDate = new Date(this.model.year, this.model.month - 1, this.model.day);
    const values = this.visitGroup.value;
    const visit: Visit = {
      dateOfVisit: newDate,
      title: values.title,
      description: values.description,
      isDone: false,
      employeeId: this.employee.id,
      customerId:  Number(values.customerSelector)
    };
    this.visitService.createVisit(visit).subscribe(newVist => console.log(visit.dateOfVisit));
  }
}
