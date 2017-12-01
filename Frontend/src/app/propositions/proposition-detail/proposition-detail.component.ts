import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {inject} from '@angular/core/testing';
import {Employee} from '../../login/shared/employee-model';
import {PropositionService} from '../shared/proposition.service';

@Component({
  selector: 'app-proposition-detail',
  templateUrl: './proposition-detail.component.html',
  styleUrls: ['./proposition-detail.component.css']
})
export class PropositionDetailComponent implements OnInit {

  @Input()
  proposition: Proposition;
  @Input()
  employee: Employee;

  constructor(private propositionService: PropositionService) {
  }

  ngOnInit() {
    this.proposition = this.propositionService.getCurrentProposition();
  }

  goBack(){
    window.history.back();
  }

}