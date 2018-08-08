import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Customer} from '../shared/customer.model';
import {SalesmanListService} from '../shared/salesman-list.service';
import {SalesmanList} from '../shared/salesmanList.model';
import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  isP20Showed: boolean;
  wantToDeleteCustomerFromP20 = false;
  disableBtn = false;
  @Input()
  customer: Customer;
  @Input()
  employee: Employee;
  @Output()
  eDeleted = new EventEmitter();
  constructor(private salesmanListService: SalesmanListService) {
    this.isP20Showed = this.salesmanListService.getP20ListShowed();
  }

  ngOnInit() {
  }

  clickDeleteCustomerFromP20() {
    this.wantToDeleteCustomerFromP20 = true;

  }
  removeCustomerFromP20() {
    this.disableBtn = true;
    this.showSnackBar('snackbarDelete');
      this.salesmanListService.getSalesmanList(this.employee.id).subscribe(y => {
        for (let v of y)
        {
          if (v.employeeId === this.employee.id && v.customerId === this.customer.id) {
            this.salesmanListService.removeCustomerFromP20(v.id).subscribe(sml => {
              this.eDeleted.emit(sml);
              this.disableBtn = false;
            });
          }
        }
      });
  }
  addCustomerToP20() {
    this.disableBtn = true;
    const custToP20: SalesmanList = {customerId: this.customer.id, employeeId: this.employee.id};
    this.salesmanListService.getSalesmanList(this.employee.id).subscribe(y => {
      for (let v of y)
      {
        if (this.customer.id === v.customer.id ) {
          this.showSnackBar('snackbarError');
          this.disableBtn = false;
          return;
        }
      }
      this.salesmanListService.addCustomerToP20(custToP20).subscribe(() => this.disableBtn = false);
      this.showSnackBar('snackbarSucces');

    });
  }
  showSnackBar(snackBarToOpen: string) {
    const x = document.getElementById(snackBarToOpen)
    x.className = 'show';
    setTimeout(function(){ x.className = x.className.replace('show', ''); }, 3000);
  }
}
