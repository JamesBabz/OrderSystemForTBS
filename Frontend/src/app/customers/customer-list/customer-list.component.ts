import { Component, OnInit } from '@angular/core';
import {Customer} from '../shared/customer.model';
import {CustomerService} from '../shared/customer.service';
import {ActivatedRoute, Router} from '@angular/router';
import {SalesmanListService} from '../shared/salesman-list.service';
import {SalesmanList} from '../shared/salesmanList.model';
import {forEach} from '@angular/router/src/utils/collection';
import {EmployeeService} from '../../login/shared/employee.service';
import {Employee} from '../../login/shared/employee.model';

@Component({
  selector: 'app-customer-list',
  templateUrl: './customer-list.component.html',
  styleUrls: ['./customer-list.component.css']
})
export class CustomerListComponent implements OnInit {
  customers: Customer[];
  query: string;
  salemanList: SalesmanList[];
  employeeCustomers: Array<Customer> = [];
  employee: Employee;
  isP20Showed = false;

  constructor(private customerService: CustomerService, private router: Router, private salesmanListService: SalesmanListService, private employeeService: EmployeeService) {
  }

  ngOnInit() {
    this.showCustomers();
    this.employeeService.getCurrentEmployee().subscribe(Employee => this.employee = Employee);
  }

  details(customer: Customer) {
    this.router.navigateByUrl('/customer/' + customer.id);
  }

  createCustomer() {
    this.router.navigateByUrl('/customers/create');
  }

  createProposition() {
    this.router.navigateByUrl('propositions/create');
  }

  createVisit() {
    this.router.navigateByUrl('visits/create');
  }

  search() {
    this.customerService.searchQuery(this.query).subscribe(Customers => this.customers = Customers);
  }

  showCalendar() {
    this.router.navigateByUrl('/calendar');
  }

  test() {
    this.salesmanListService.getSalesmanList(this.employee.id).subscribe(x => this.salemanList = x);
  }


  showP20() {
    this.salesmanListService.getSalesmanList(this.employee.id).subscribe(y => {
      this.salemanList = y;
      this.addEmployeeCustomersToList();
    });
  }

  showCustomers() {
    this.customerService.getCustomers().subscribe(Customers => this.customers = Customers);
  }

  changeList() {
    if (this.isP20Showed) {
      this.showCustomers();
    } else {
      this.showP20();
    }
    this.isP20Showed = !this.isP20Showed;
  }

  addEmployeeCustomersToList() {
    if (this.employeeCustomers != null) {
      this.popAList(this.employeeCustomers);
    }
    for (let x of this.salemanList) {
      this.employeeCustomers.push(x.customer);
    }
    this.customers = this.employeeCustomers;
  }

  popAList(list: any) {
    for (let i = 0; list.length; i++) {
      list.pop();
    }
  }
}

