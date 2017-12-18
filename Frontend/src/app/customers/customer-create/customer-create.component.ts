import {Component, OnInit, Sanitizer} from '@angular/core';
import {Router} from '@angular/router';
import {Customer} from '../shared/customer.model';
import {CustomerService} from '../shared/customer.service';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {waitForMap} from '@angular/router/src/utils/collection';
import {DawaService} from '../shared/dawa.service';
import {CVRService} from '../shared/cvr.service';


@Component({
  selector: 'app-customer-create',
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.css']
})
export class CustomerCreateComponent implements OnInit {


  customerGroup: FormGroup;


  constructor(private customerService: CustomerService, private dawaService: DawaService, private cvrService: CVRService, private router: Router, private formBuilder: FormBuilder) {
    this.customerGroup = this.formBuilder.group({
      companyname: '',
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      address: '',
      zipCode: '',
      city: '',
      email: ['', Validators.email],
      phone: '',
      cvr: ''
    });
  }

  ngOnInit() {
  }

  close() {
    this.router.navigateByUrl('/customers');
  }

  getCvr() {

    if ((<HTMLInputElement>document.getElementsByName('cvr')[0]).value == '') {
      return;
    }

    this.cvrService.getCVR(this.customerGroup.value.cvr)
      .subscribe(res => {
        this.customerGroup.patchValue(
          {
            companyname: res[1],
            address: res[2],
            zipCode: res[4],
            city: res[3],
            phone: res[5],
            email: res[6]
          });
      });
  }

  createCustomer() {
    const values = this.customerGroup.value;
    let phoneAsNumber;
    if (values.phone === '') {
      phoneAsNumber = 0;
    } else {
      phoneAsNumber = values.phone;
    }
    const customer: Customer = {

      firstname: values.firstname,
      lastname: values.lastname,
      address: values.address,
      zipCode: Number(values.zipCode),
      city: values.city,
      phone: phoneAsNumber,
      email: values.email,
      companyname: values.companyname,
      cvr: Number(values.cvr),
    };

    this.customerService.createCustomer(customer).subscribe(newCustomer => {
      this.router.navigateByUrl('customer/' + newCustomer.id);
    });
  }
}
