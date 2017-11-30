import {Component, OnInit} from '@angular/core';
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

  propositions: Proposition[];

  constructor(private propositionService: PropositionService, private router: Router, private route: ActivatedRoute) {
    this.route.paramMap
      .switchMap(params => this.propositionService.getPropositionsByCustomerId(+params.get('id')))
      .subscribe(Proposition => this.propositions = Proposition);
  }

  ngOnInit() {
  }

}
