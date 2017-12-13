import { Component, OnInit } from '@angular/core';
import {Customer} from '../shared/customer.model';
import {CustomerService} from '../shared/customer.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers: Customer[];
  query: string;

  constructor(private customerService: CustomerService, private router: Router) {

  }

  ngOnInit() {
    this.customerService.getCustomers().subscribe(Customers => this.customers = Customers);
  }

  details(customer: Customer) {
    this.router.navigateByUrl('/customer/' + customer.id);
  }

  createCustomer() {
    this.router.navigateByUrl('/customers/create');
  }

  createProposition() {
    this.router.navigateByUrl('propositions/create');
  }
  createVisit() {
    this.router.navigateByUrl('visits/create');
  }

  search() {
    this.customerService.searchQuery(this.query).subscribe(Customers => this.customers = Customers);
  }

  showCalendar() {
    this.router.navigateByUrl('/calendar');
  }
}
