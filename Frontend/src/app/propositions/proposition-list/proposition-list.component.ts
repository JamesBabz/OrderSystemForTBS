import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerService} from '../../customers/shared/customer.service';
import {PropositionService} from '../shared/proposition.service';
import {Customer} from '../../customers/shared/customer.model';
import {SharedService} from '../../shared/shared.service';

@Component({
  selector: 'app-proposition-list',
  templateUrl: './proposition-list.component.html',
  styleUrls: ['./proposition-list.component.css']
})

export class PropositionListComponent implements OnInit {

  @Input()
  customer: Customer;


  propositions: Proposition[];

  constructor(private propositionService: PropositionService, private router: Router, private route: ActivatedRoute, private sharedService: SharedService) {
  this.refresh();
  }

  ngOnInit() {
  }
  create() {
    this.sharedService.setCurrentCustomer(this.customer);
    this.router.navigateByUrl('/propositions/create');
  }

  refresh() {
    this.route.paramMap
      .switchMap(params => this.propositionService.getPropositionsByCustomerId(+params.get('id')))
      .subscribe(props => this.propositions = props);
  }

}
