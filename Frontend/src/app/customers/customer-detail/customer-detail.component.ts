import {Component, OnInit} from '@angular/core';
import {CustomerService} from '../shared/customer.service';
import {Customer} from '../shared/customer.model';
import {ActivatedRoute, Router} from '@angular/router';
import 'rxjs/add/operator/switchMap';
import {stringify} from 'querystring';
import {Proposition} from '../../propositions/shared/proposition.model';
import {PropositionService} from '../../propositions/shared/proposition.service';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  propositions: Proposition[];
  customer: Customer;

  constructor(private customerService: CustomerService, private propositionService: PropositionService,
              private router: Router, private route: ActivatedRoute) {
    this.route.paramMap
      .switchMap(params => this.propositionService.getPropositionsByCustomerId(+params.get('id')))
      .subscribe(Proposition => this.propositions = Proposition);
    this.route.paramMap
      .switchMap(params => this.customerService.getCustomerById(+params.get('id')))
      .subscribe(Customer => this.customer = Customer);
  }

  ngOnInit() {
  }

  showProp() {
     console.log(this.propositions);
  }

  showAProp(prop){
    console.log(prop);
  }

}
