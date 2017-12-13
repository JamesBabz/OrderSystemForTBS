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


  @Input()
  visits: Visit[];
  @Input()
  customer: Customer;
  pastVisits: Visit[];
  futureVisits: Visit[];


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

    var today = new Date();
    var currentDate = new Date(today.getTime());
    for (let visit of list) {
      var date = new Date(visit.dateOfVisit);
      var dateFromVisit = new Date(date.getTime());

      if (dateFromVisit > currentDate) {
        this.addToFutureVisits(visit);
      } else {
        this.addToPastVisits(visit);
      }
    }
  }

  addToFutureVisits(visit: Visit) {
    this.futureVisits.push(visit);
  }

  addToPastVisits(visit: Visit) {
    this.pastVisits.push(visit);
  }
  createVisit() { this.visitService.setCurrentCustomer(this.customer);
    this.router.navigateByUrl('visits/create');
  }

}
