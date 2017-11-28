import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import {Customer} from '../shared/customer.model';
import {CustomerService} from '../shared/customer.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';


@Component({
  selector: 'app-customer-create',
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.css']
})
export class CustomerCreateComponent implements OnInit {


  customerGroup: FormGroup;

  constructor(private customerService: CustomerService, private router: Router, private formBuilder: FormBuilder) {
    this.customerGroup = this.formBuilder.group({
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      address: '',
      zipCode: '',
      city: '',
      email: '',
      phone: '',
      cvr: ''
    });
  }

  ngOnInit() {
  }

  close() {
    this.router.navigateByUrl('/customers');
  }



    createCustomer() {
    const values = this.customerGroup.value;
    const customer: Customer = {firstname: values.firstname,
      lastname: values.lastname,
      address: values.address,
      zipCode: Number(values.zipCode),
      city: values.city,
      phone: Number(values.phone),
      email: values.email,
      cvr: Number(values.cvr)};
    this.customerService.createCustomer(customer).subscribe(newCustomer => {
      this.router.navigateByUrl('customer/' + newCustomer.id);
    });
  }
}
