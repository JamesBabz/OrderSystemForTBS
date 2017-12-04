import {Component, OnInit} from '@angular/core';
import {PropositionService} from '../shared/proposition.service';
import {Customer} from '../../customers/shared/customer.model';

@Component({
  selector: 'app-proposition-create',
  templateUrl: './proposition-create.component.html',
  styleUrls: ['./proposition-create.component.css']
})
export class PropositionCreateComponent implements OnInit {

  customer: Customer;

  constructor(propositionService: PropositionService) {
    this.customer = propositionService.getCurrentCustomer();
  }

  ngOnInit() {
  }

}
