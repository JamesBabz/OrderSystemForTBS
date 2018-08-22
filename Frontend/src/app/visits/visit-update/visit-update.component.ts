import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Visit} from '../shared/visit.model';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NgbCalendar, NgbDatepickerConfig, NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';
import {NgbDate} from '@ng-bootstrap/ng-bootstrap/datepicker/ngb-date';
import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';
import {Customer} from '../../customers/shared/customer.model';
import {VisitService} from '../shared/visit.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-visit-update',
  templateUrl: './visit-update.component.html',
  styleUrls: ['./visit-update.component.css']
})
export class VisitUpdateComponent implements OnInit {

  @Input()
  visit: Visit;
  employee: Employee;
  @Input()
    customer: Customer;
  visitGroup: FormGroup;
  model: NgbDateStruct;
  hours: Array <string> = [];
  minutes: Array <string> = [];

  @Output()
  cancelSelected = new EventEmitter();

  constructor(private formBuilder: FormBuilder,  private calendar: NgbCalendar, private config: NgbDatepickerConfig, private employeeService: EmployeeService, private visitService: VisitService, private router: Router) {
    this.visitGroup = this.formBuilder.group({
      description: ['', Validators.required],
      datePicked: ['', Validators.required],
      fromHours: ['00', Validators.required],
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

    this.model = this.calendar.getToday();
    this.config.showWeekNumbers = true;
    this.config.markDisabled = (date: NgbDate) => calendar.getWeekday(date) >= 6;
    this.config.outsideDays = 'hidden';
  }

  ngOnInit() {
    this.employeeService.getCurrentEmployee().subscribe(Employee => this.employee = Employee);
  }

  cancel() {
    this.cancelSelected.emit(this.visit);
  }

  createVisit() {
    const values = this.visitGroup.value;

    // if (values.fromHours.startsWith(0)) {
    //   values.fromHours.slice(1);
    // }
    // if (values.toHours.startsWith(0)) {
    //   values.toHours.slice(1);
    // }

    console.log(this.customer.id);

    const newStartDate = new Date(this.model.year, this.model.month - 1, this.model.day, Number(values.fromHours) + 2, Number(values.fromMinutes));
    const newEndDate = new Date(this.model.year, this.model.month - 1, this.model.day, Number(values.toHours) + 2, Number(values.toMinutes));
    const visit: Visit = {
      dateTimeOfVisitStart: newStartDate,
      dateTimeOfVisitEnd: newEndDate,
      title: this.visit.title + ' DEL 2',
      description: values.description,
      isDone: false,
      employeeId: this.employee.id,
      customerId: this.customer.id
    };
    this.visitService.createVisit(visit).subscribe(newVisit => this.router.navigateByUrl('customer/' + this.customer.id));

  }

}
