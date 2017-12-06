import {Component, Input, OnInit} from '@angular/core';
import {Visit} from '../shared/visit.model';
import {VisitService} from '../shared/visit.service';
import {Employee} from '../../login/shared/employee.model';
import {Customer} from '../../customers/shared/customer.model';

@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.css']
})

export class VisitComponent implements OnInit {
  @Input()
  visit: Visit;
  @Input()
  employee: Employee;
  constructor(private visitService: VisitService) { }

  ngOnInit() {
  }
  getEUString(date: Date) {
    return this.visitService.getDateAsEUString(date);
  }

}
