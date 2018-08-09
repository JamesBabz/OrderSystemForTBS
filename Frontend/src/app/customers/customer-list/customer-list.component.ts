import { Component, OnInit } from '@angular/core';
import {Customer} from '../shared/customer.model';
import {CustomerService} from '../shared/customer.service';
import { Router} from '@angular/router';
import {SalesmanListService} from '../shared/salesman-list.service';
import {SalesmanList} from '../shared/salesmanList.model';
import {EmployeeService} from '../../login/shared/employee.service';
import {Employee} from '../../login/shared/employee.model';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import {environment} from '../../../environments/environment';

const tbsLogo = environment.logoDataUrl;
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

  details(customer: Customer, event) {
    if (event.target.tagName === 'I') {
      return;
    }
    this.router.navigateByUrl('/customer/' + customer.id);
  }

  createCustomer() {
    this.router.navigateByUrl('/customers/create');
  }

  createProposition() {
    this.router.navigateByUrl('propositions/create');
  }

  openAdminPage() {
    this.router.navigateByUrl('/admin');
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

  showP20() {
    this.salesmanListService.getSalesmanList(this.employee.id).subscribe(y => {
      this.salemanList = y;
      this.addEmployeeCustomersToList();
    });
    this.salesmanListService.setP20ListShowed(true);
  }

  showCustomers() {
    this.customerService.getCustomers().subscribe(Customers => this.customers = Customers);
    this.salesmanListService.setP20ListShowed(false);
  }

  refresh() {
    this.showP20();
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

  openP20ListInPdf() {
    var rowNumb = 1;
    const cvr = [];
    const name = [];
    const city = [];
    const address = [];
    const phone = [];
    const mail = [];
    const rowNumber = [];
    for (let x of this.employeeCustomers){
      cvr.push(x.cvr);
      name.push(x.firstname + ' ' + x.lastname);
      city.push(x.zipCode + ' ' + x.city);
      address.push(x.address);
      phone.push(x.phone);
      mail.push(x.email);
      rowNumber.push(rowNumb ++);
    }
     this.genereatePdfFile(rowNumber, cvr, name, city, address, phone, mail);
  }

  genereatePdfFile(rowNumber: string[], cvr: string[], name: string[], city: string[], address: string[], phone: string[], mail: string[] ) {
    pdfMake.vfs = pdfFonts.pdfMake.vfs;
    const date = new Date;
    const docDefinition = {
      pageOrientation: 'landscape',
      pageMargins: [40, 60, 40, 0],
      pageBreak: 'before',
      footer: {
        columns: [
          { text: this.customerService.getDateAsEUString(date), margin: [ 50, -50, 10, 20 ] }
        ]
      },
      header: {
        columns: [

          {
            margin: 10,
            image: 'data:image/png;' + tbsLogo + ',...encodedContent...',
            width: 120
          },
          {
            margin: 20,
            text: this.employee.firstname + ' ' + this.employee.lastname + "'s P20 liste", style: 'header',
          }
        ]
      },
      content: [
        {
          layout: 'lightHorizontalLines',
          margin: [ 0, 25, 10, 0 ],
          table: {
            // headers are automatically repeated if the table spans over multiple pages
            // you can declare how many rows should be treated as headers
            headerRows: 1,
            widths: ['auto', 'auto', 'auto', 'auto', 'auto', 'auto', 'auto' ],

            body: [
              ['Nr', 'CVR', 'Navn', 'By', 'Vej', 'Telefon', 'Email' ],
              [ rowNumber, cvr, name, city, address, phone, mail ]
            ]
          }
        }
      ],
      styles: {
        header: {
          fontSize: 22,
          bold: true
        },
        headLine: {
          bold: true
        }
      }
    };
    pdfMake.createPdf(docDefinition).download(this.employee.firstname + '_' + date);

  }

  popAList(list: any) {
    for (let i = 0; list.length; i++) {
      list.pop();
    }
  }
}

