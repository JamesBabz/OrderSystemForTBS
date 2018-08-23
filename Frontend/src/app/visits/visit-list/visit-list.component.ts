import {Component, Input, OnInit} from '@angular/core';
import {Customer} from '../../customers/shared/customer.model';
import {VisitService} from '../shared/visit.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Visit} from '../shared/visit.model';
import {viewClassName} from '@angular/compiler';
import {getDate} from 'ngx-bootstrap/bs-moment/utils/date-getters';
import {parseTime} from 'ngx-bootstrap/timepicker/timepicker.utils';

@Component({
  selector: 'app-visit-list',
  templateUrl: './visit-list.component.html',
  styleUrls: ['./visit-list.component.css']
})
export class VisitListComponent implements OnInit {



  visits: Visit[];
  @Input()
  customer: Customer;
  pastVisits: Visit[];
  futureVisits: Visit[];
  futureVistColor = '#AAD48C';
  pastVisitColor = '#fcf82d';

  constructor(private visitService: VisitService, private router: Router, private route: ActivatedRoute) {
    this.pastVisits = [];
    this.futureVisits = [];
    this.customer = this.visitService.getCurrentCustomer();
  }

  ngOnInit() {
    this.route.paramMap
      .switchMap(params => this.visitService.getVisitsByCustomerId(+params.get('id')))
      .subscribe(Visit => this.sortCurrentDate(this.visits = Visit));
}

  sortCurrentDate(list: Visit[]) {
    const today = new Date();
    const currentDate = new Date(today.getTime());
    for (let visit of list) {
      const date = new Date(visit.dateTimeOfVisitStart);
      const dateFromVisit = new Date(date.getTime());

      if (dateFromVisit >= currentDate) {
        this.futureVisits.push(visit);
      } else {
        this.pastVisits.push(visit);
      }
    }
  }
  refresh() {
this.popAList(this.pastVisits);
this.popAList(this.futureVisits);
    this.route.paramMap
      .switchMap(params => this.visitService.getVisitsByCustomerId(+params.get('id')))
      .subscribe(Visit => this.sortCurrentDate(this.visits = Visit));

  }

popAList(list: any) {
  for (let i = 0; list.length; i++) {
    list.pop();
  }
}
  createVisit() { this.visitService.setCurrentCustomer(this.customer);
    this.router.navigateByUrl('visits/create');
  }

  showCalendar() {
    this.router.navigateByUrl('/calendar');
  }

}
