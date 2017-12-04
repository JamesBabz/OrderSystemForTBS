import {Component, OnInit} from '@angular/core';
import {PropositionService} from '../shared/proposition.service';
import {Customer} from '../../customers/shared/customer.model';
import {CustomerService} from '../../customers/shared/customer.service';
import {stringify} from 'querystring';

@Component({
  selector: 'app-proposition-create',
  templateUrl: './proposition-create.component.html',
  styleUrls: ['./proposition-create.component.css']
})
export class PropositionCreateComponent implements OnInit {

  customer: Customer;
  customers: Customer[];

  constructor(propositionService: PropositionService, customerService: CustomerService) {
    this.customer = propositionService.getCurrentCustomer();
    customerService.getCustomers().subscribe(Customers => this.customers = Customers);
  }

  ngOnInit() {
  }

  showAll() {
    console.log(stringify(this.customers));
  }

  createNewProposition() {
    title

  }

  cancel(){
    window.history.back();
  }
}
