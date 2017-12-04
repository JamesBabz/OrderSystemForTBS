import {Component, OnInit} from '@angular/core';
import {PropositionService} from '../shared/proposition.service';
import {Customer} from '../../customers/shared/customer.model';
import {CustomerService} from '../../customers/shared/customer.service';
import {stringify} from 'querystring';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Proposition} from '../shared/proposition.model';
import {Router} from '@angular/router';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';

@Component({
  selector: 'app-proposition-create',
  templateUrl: './proposition-create.component.html',
  styleUrls: ['./proposition-create.component.css']
})
export class PropositionCreateComponent implements OnInit {

  customer: Customer;
  customers: Customer[];
  createPropFormGroup: FormGroup;
  employeeId: number;

  constructor(private propositionService: PropositionService, private customerService: CustomerService, private router: Router) {
    this.customer = propositionService.getCurrentCustomer();
    customerService.getCustomers().subscribe(Customers => this.customers = Customers);


  }

  ngOnInit() {
    this.createPropFormGroup = new FormGroup({
      // customerSelector: new FormControl(this.customer === null ? 0 : this.customer.id, Validators.pattern(/^([^0]*)$/)),
      customerSelector: new FormControl(this.customer === null ? '' : this.customer.id, Validators.required),
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      file: new FormControl()
    });
    this.employeeId = toNumber(localStorage.getItem('currentUser').split(',')[0].substr(6));
  }

  showAll() {
    console.log(stringify(this.customers));
  }

  cancel() {
    window.history.back();
  }

  createNewProposition() {
    const values = this.createPropFormGroup.value;

    const proposition: Proposition = {
      title: values.title,
      description: values.description,
      creationDate: new Date(),
      customerId: Number(values.customerSelector),
      EmployeeId: this.employeeId,
      fileId: 0
    };
    this.propositionService.createProposition(proposition).subscribe(
      newProp => {
        this.propositionService.setCurrentProposition(newProp);
        this.router.navigateByUrl('proposition/' + newProp.id);
      });

  }
}
