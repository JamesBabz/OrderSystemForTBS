import {Component, OnInit} from '@angular/core';
import {CustomerService} from '../shared/customer.service';
import {Customer} from '../shared/customer.model';
import {ActivatedRoute, Router} from '@angular/router';
import 'rxjs/add/operator/switchMap';
import {stringify} from 'querystring';
import 'TabModule' from 'angular-tabs-component';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  customer: Customer;

  constructor(private customerService: CustomerService, private router: Router, private route: ActivatedRoute) {
    this.route.paramMap
      .switchMap(params => this.customerService.getCustomerById(+params.get('id')))
      .subscribe(Customer => this.customer = Customer);
  }

  ngOnInit() {
  }


  alertC() {
    alert(stringify(this.customer));
  }
}
