import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {inject} from '@angular/core/testing';
import {Employee} from '../../login/shared/employee.model';
import {PropositionService} from '../shared/proposition.service';
import {Router} from '@angular/router';

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

  deleteModal: boolean;

  constructor(private propositionService: PropositionService, private router: Router) {
  }

  ngOnInit() {
    this.proposition = this.propositionService.getCurrentProposition();
    this.deleteModal = false;
  }

  goBack() {
    this.router.navigateByUrl('customer/' + this.proposition.customerId);
  }

  delete() {
    this.propositionService.deleteProposition(this.proposition.id).subscribe(prop => this.router.navigateByUrl('customer/' + prop.customerId));
  }

  edit() {

  }

  openModal() {
    this.deleteModal = true;
  }

  closeModal($event) {
    if ($event.srcElement.classList.contains('shouldClose')) {
      this.deleteModal = false;
    }
  }
}
