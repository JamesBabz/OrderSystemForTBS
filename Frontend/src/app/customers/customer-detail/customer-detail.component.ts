import {Component, OnInit} from '@angular/core';
import {CustomerService} from '../shared/customer.service';
import {Customer} from '../shared/customer.model';
import {ActivatedRoute, Router} from '@angular/router';
import 'rxjs/add/operator/switchMap';
import {stringify} from 'querystring';
import {Proposition} from '../../propositions/shared/proposition.model';
import {PropositionService} from '../../propositions/shared/proposition.service';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  customer: Customer;
  closeResult: string;
  constructor(private customerService: CustomerService, private propositionService: PropositionService,
              private router: Router, private route: ActivatedRoute, private modalService: NgbModal) {
    this.route.paramMap
      .switchMap(params => this.customerService.getCustomerById(+params.get('id')))
      .subscribe(Customer => this.customer = Customer);
  }

  ngOnInit() {
  }
  open(content) {
    this.modalService.open(content).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK ) {
      return 'by clicking on a backdrop';
    } else {
      return  `with: ${reason}`;
    }
  }


}
