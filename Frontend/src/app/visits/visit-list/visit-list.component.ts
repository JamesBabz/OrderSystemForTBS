import {Component, Input, OnInit} from '@angular/core';
import {Customer} from '../../customers/shared/customer.model';
import {VisitService} from '../shared/visit.service';
import {ActivatedRoute, Router} from '@angular/router';
import {Visit} from '../shared/visit.model';
import {viewClassName} from '@angular/compiler';

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

  constructor(private visitService: VisitService,  private router: Router, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.paramMap
      .switchMap(params => this.visitService.getVisitsByCustomerId(+params.get('id')))
      .subscribe(Visit => this.visits = Visit );
  }

}
