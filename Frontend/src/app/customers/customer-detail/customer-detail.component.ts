import {Component, OnInit} from '@angular/core';
import {CustomerService} from '../shared/customer.service';
import {Customer} from '../shared/customer.model';
import {ActivatedRoute, Router} from '@angular/router';
import 'rxjs/add/operator/switchMap';
import {stringify} from 'querystring';
import {Proposition} from '../../propositions/shared/proposition.model';
import {PropositionService} from '../../propositions/shared/proposition.service';
import {ModalDismissReasons, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {isChangedDate} from '@ng-bootstrap/ng-bootstrap/datepicker/datepicker-tools';

@Component({
  selector: 'app-customer-detail',
  templateUrl: './customer-detail.component.html',
  styleUrls: ['./customer-detail.component.css']
})
export class CustomerDetailComponent implements OnInit {

  modalString: string;
  propTab: string;
  equipTab: string;
  visitTab: string;
  lastPage: string;

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
    this.modalString = '';
    switch (this.customerService.getTab()) {
      case 1:
        this.propTab = '1';
        break;
      case 2:
        this.equipTab = '1';
        break;
      case 3:
        // this.propTab = '1';
        break;
      case 4:
        this.visitTab = '1';
        break;
    }
    this.customerService.setTab(0);

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


  openModal(toDo: string) {
    document.getElementsByTagName('BODY')[0].classList.add('disableScroll');
    this.modalString = toDo;
  }

  /**
   * closes the modal.
   * reads css classes from the clicked element.
   * shouldKeepInput class lets the changed input stay in the fields
   * shouldClose class is to prevent child elements from closing
   * @param $event
   */
  closeModal($event) {
    if ($event.srcElement.classList.contains('shouldClose')) {
      document.getElementsByTagName('BODY')[0].classList.remove('disableScroll');
      this.modalString = '';
    }
  }


}
