import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {stringify} from 'querystring';
import {Employee} from '../../login/shared/employee-model';
import {getDayOfWeek} from 'ngx-bootstrap/bs-moment/utils/date-getters';
import {PropositionService} from '../shared/proposition.service';

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

  constructor(private propositionService: PropositionService) {
  }

  ngOnInit() {
  }


  getEUString(date: Date) {
    return this.propositionService.getCreationDateAsEUString(date);
  }

}
