import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Customer} from '../shared/customer.model';
import {SalesmanListService} from '../shared/salesman-list.service';
import {SalesmanList} from '../shared/salesmanList.model';
import {Employee} from '../../login/shared/employee.model';
import {EmployeeService} from '../../login/shared/employee.service';
import {NotificationsService} from 'angular2-notifications/dist';
import { defaultIcons } from 'angular2-notifications';

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

  private _notifiService: NotificationsService;

  constructor(private notifiService: NotificationsService, private salesmanListService: SalesmanListService) {
    this.isP20Showed = this.salesmanListService.getP20ListShowed();
    this._notifiService = notifiService;

  }

  ngOnInit() {
  }

  clickDeleteCustomerFromP20() {
    this.wantToDeleteCustomerFromP20 = true;

  }
  removeCustomerFromP20() {
    this.disableBtn = true;
    this._notifiService.error(this.customer.firstname + this.customer.lastname, "Er blevet fjernet fra P20");
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
          this._notifiService.alert(this.customer.firstname + " " + this.customer.lastname, "Er allerede tilføjet til P20", {
            icons: {
              alert: defaultIcons.info
            }
          });
          this.disableBtn = false;
          return;
        }
      }
      this.salesmanListService.addCustomerToP20(custToP20).subscribe(() => this.disableBtn = false);
      this._notifiService.success(this.customer.firstname + " " + this.customer.lastname, "Tilføjet til P20", {
        preventDuplicates: false
      });
    });
  }
}
