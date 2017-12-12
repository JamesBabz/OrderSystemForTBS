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
import {EmployeeService} from "../../login/shared/employee.service";
import {Employee} from "../../login/shared/employee.model";

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
  employeeId: number;
  employee: Employee;
  selectedFile: File;


  constructor(private employeeService: EmployeeService, private propositionService: PropositionService, private loginService: LoginService, private customerService: CustomerService, private router: Router) {

    this.customer = propositionService.getCurrentCustomer();
    customerService.getCustomers().subscribe(Customers => this.customers = Customers);

  }

  ngOnInit() {
    this.createPropFormGroup = new FormGroup({
      customerSelector: new FormControl(this.customer === null ? '' : this.customer.id, Validators.required),
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      file: new FormControl()
    });
    this.employeeService.getEmployee().subscribe(Employee => this.employee = Employee);
  }

  showAll() {
    console.log(this.employee);
  }

  cancel() {
    window.history.back();
  }

  createNewProposition() {
    const values = this.createPropFormGroup.value;
    this.customerService.getCustomerById(Number(values.customerSelector)).subscribe(cust => this.selectedCust = cust);
    const proposition: Proposition = {
      title: values.title,
      description: values.description,
      creationDate: new Date(),
      customerId: Number(values.customerSelector),
      employeeId: this.employee.id,
      fileId: 0
    };
    this.propositionService.createProposition(proposition).subscribe(
      newProp => {
        newProp.employee = this.employee,
        this.propositionService.setCurrentProposition(newProp);
        this.router.navigateByUrl('proposition/' + newProp.id);
      });
    this.propositionService.upLoadImage(this.selectedFile).subscribe(File => console.log(this.selectedFile));
  }

  onFileChange(event) {
    if (event.target.files && event.target.files.length > 0) {
      this.selectedFile = event.target.files[0];
    }
  }
}
