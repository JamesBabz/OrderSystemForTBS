import {Component, Input, OnInit} from '@angular/core';
import {Customer} from '../../customers/shared/customer.model';

@Component({
  selector: 'app-visit-list',
  templateUrl: './visit-list.component.html',
  styleUrls: ['./visit-list.component.css']
})
export class VisitListComponent implements OnInit {

  @Input()
  customer: Customer;
  constructor() { }

  ngOnInit() {
  }

}
