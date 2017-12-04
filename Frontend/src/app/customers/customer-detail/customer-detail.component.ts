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
  editCustomer: Customer;
  isSaved = false;
  changes = false;
  constructor(private customerService: CustomerService, private router: Router, private route: ActivatedRoute, private modalService: NgbModal) {
  }

  ngOnInit() {
    this.route.paramMap
      .switchMap(params => this.customerService
        .getCustomerById(+params.get('id')))
      .subscribe(Customer => {
        this.customer = Customer;
        this.editCustomer = Object.assign({}, this.customer);
      });
  }
  open(content) {
    this.modalService.open(content);
  }
  openEdit(editContent) {
    this.modalService.open(editContent);
    if (this.isSaved) {
      this.isSaved = false;
    }
  }
  openDelete(deleteContent) {
    this.modalService.open(deleteContent);
  }
  cancel() {
  this.changes = false;
    this.editCustomer = Object.assign({}, this.customer);
  }
  updateCustomer() {
    if (this.changes) {
      this.customerService.updateCustomerById(this.customer.id, this.editCustomer).subscribe(Customer => {
        this.customer = Customer;
        this.editCustomer = Object.assign({}, this.customer);
        this.isSaved = true;
      });
    }
    this.changes = false;
  }
  deleteCustomer() {
    this.customerService.deleteCustomerById(this.customer.id).subscribe(Customer => this.router.navigate(['/customers']));
  }



}
