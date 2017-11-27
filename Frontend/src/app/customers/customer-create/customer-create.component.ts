import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-customer-create',
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.css']
})
export class CustomerCreateComponent implements OnInit {

  constructor(private router: Router) {}

  ngOnInit() {
  }

  cancel() {
    this.router.navigateByUrl('/customers');
  }
}
