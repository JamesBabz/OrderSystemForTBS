import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {stringify} from 'querystring';
import {Employee} from '../../login/shared/employee-model';

@Component({
  selector: 'app-proposition',
  templateUrl: './proposition.component.html',
  styleUrls: ['./proposition.component.css']
})
export class PropositionComponent implements OnInit {

  @Input()
  proposition: Proposition;
  @Input()
  employee: Employee;

  constructor() {
  }

  ngOnInit() {
  }

  showProp() {
    alert(stringify(this.employee));
  }

}
