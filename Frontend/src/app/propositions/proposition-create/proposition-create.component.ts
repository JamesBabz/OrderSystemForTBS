import {Component, OnInit} from '@angular/core';
import {PropositionService} from '../shared/proposition.service';
import {Customer} from '../../customers/shared/customer.model';
import {CustomerService} from '../../customers/shared/customer.service';
import {stringify} from 'querystring';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Proposition} from '../shared/proposition.model';
import {Router} from '@angular/router';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {LoginService} from '../../login/shared/login.service';
import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';
import {ifTrue} from 'codelyzer/util/function';
import {SharedService} from '../../shared/shared.service';

@Component({
  selector: 'app-proposition-create',
  templateUrl: './proposition-create.component.html',
  styleUrls: ['./proposition-create.component.css']
})
export class PropositionCreateComponent implements OnInit {

  customer: Customer;
  selectedCust: Customer;
  customers: Customer[];
  createPropFormGroup: FormGroup;
  employee: Employee;
  base64textString: string;
  upLoadedAImage = false;
  correctFile = true;


  constructor(private employeeService: EmployeeService, private propositionService: PropositionService,
              private sharedService: SharedService, private customerService: CustomerService, private router: Router) {

    this.customer = this.sharedService.getCurrentCustomer();
    customerService.getCustomers().subscribe(Customers => this.customers = Customers);


  }

  ngOnInit() {
    this.createPropFormGroup = new FormGroup({
      customerSelector: new FormControl(this.customer === null ? '' : this.customer.id, Validators.required),
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      file: new FormControl()
    });
    this.employeeService.getCurrentEmployee().subscribe(Employee => this.employee = Employee);

  }

  cancel() {
    window.history.back();
  }

  createNewProposition() {
    let timeStamp = 0;
    if (this.upLoadedAImage) {
      timeStamp = Date.now();
    }

    const values = this.createPropFormGroup.value;
    this.customerService.getCustomerById(Number(values.customerSelector)).subscribe(cust => this.selectedCust = cust);
    const proposition: Proposition = {
      title: values.title,
      description: values.description,
      creationDate: new Date(),
      customerId: Number(values.customerSelector),
      employeeId: this.employee.id,
      fileId: timeStamp
    };
    this.propositionService.createProposition(proposition).subscribe(
      newProp => {
        newProp.employee = this.employee,
        this.router.navigateByUrl('customer/' + this.selectedCust.id);
      });
    if (this.upLoadedAImage) {
      this.sharedService.upLoadImage(this.base64textString +  'å' + timeStamp).subscribe();
    }
  }

  onFileChange(event) {

    var files = event.target.files;
    var file = files[0];
    if (files && file && file.type.indexOf('pdf') > -1) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
      this.upLoadedAImage = true;
      this.correctFile = true;
    } else {
     this.correctFile = false;
    }
  }

  _handleReaderLoaded(readerEvt) {

    var binaryString = readerEvt.target.result;
    this.base64textString = btoa(binaryString);

  }
}
