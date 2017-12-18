import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerService} from '../../customers/shared/customer.service';
import {PropositionService} from '../shared/proposition.service';
import {Customer} from '../../customers/shared/customer.model';

@Component({
  selector: 'app-proposition-list',
  templateUrl: './proposition-list.component.html',
  styleUrls: ['./proposition-list.component.css']
})

export class PropositionListComponent implements OnInit {

  @Input()
  customer: Customer;


  propositions: Proposition[];

  constructor(private propositionService: PropositionService, private router: Router, private route: ActivatedRoute) {
    this.route.paramMap
      .switchMap(params => this.propositionService.getPropositionsByCustomerId(+params.get('id')))
      .subscribe(Proposition => this.propositions = Proposition);
  }

  ngOnInit() {
  }


  details(prop: Proposition) {
    this.propositionService.setCurrentProposition(prop);
    localStorage.setItem('currentCustomerId', this.customer.id.toString());
    this.router.navigateByUrl('/proposition/' + prop.id);
  }

  create() {
    this.propositionService.setCurrentCustomer(this.customer);
    this.router.navigateByUrl('/propositions/create');
  }

}
