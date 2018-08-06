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

  customers: Customer[];
  visitGroup: FormGroup;
  customer: Customer;
  model: NgbDateStruct;
  date: { year: number, month: number, day: number };
  employee: Employee;
  hours: Array <string> = [];
  minutes: Array <string> = [];

  constructor(private visitService: VisitService, private router: Router, private formBuilder: FormBuilder, private customerService: CustomerService, private employeeService: EmployeeService) {

    this.customer = this.visitService.getCurrentCustomer();
    this.visitGroup = this.formBuilder.group({
      customerSelector: new FormControl(this.customer === null ? '' : this.customer.id, Validators.required),
      title: ['', Validators.required],
      description: ['', Validators.required],
      datePicked: ['', Validators.required],
      fromHours: ['´00', Validators.required],
      fromMinutes: ['00', Validators.required],
      toHours: ['00', Validators.required],
      toMinutes: ['00', Validators.required]
    });

    for (var i = 0; i <= 23; i++) {
      this.hours.push(i.toString());
    }
    this.minutes.push('0');
    this.minutes.push('15');
    this.minutes.push('30');
    this.minutes.push('45');




  }

  ngOnInit() {
    this.customerService.getCustomers().subscribe(c => this.customers = c);
    this.employeeService.getCurrentEmployee().subscribe(Employee => this.employee = Employee);
  }

  cancel() {
    window.history.back();
  }

  createVisit() {
    const values = this.visitGroup.value;

    // if (values.fromHours.startsWith(0)) {
    //   values.fromHours.slice(1);
    // }
    // if (values.toHours.startsWith(0)) {
    //   values.toHours.slice(1);
    // }

    const newStartDate = new Date(this.model.year, this.model.month - 1, this.model.day, Number(values.fromHours) + 2, Number(values.fromMinutes));
    const newEndDate = new Date(this.model.year, this.model.month - 1, this.model.day, Number(values.toHours) + 2, Number(values.toMinutes));
    const visit: Visit = {
      dateTimeOfVisitStart: newStartDate,
      dateTimeOfVisitEnd: newEndDate,
      title: values.title,
      description: values.description,
      isDone: false,
      employeeId: this.employee.id,
      customerId: Number(values.customerSelector)
    };
    this.visitService.createVisit(visit).subscribe(newVisit => this.router.navigateByUrl('customer/' + Number(values.customerSelector)));
    console.log(visit);

  }
}
