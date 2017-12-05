import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {inject} from '@angular/core/testing';
import {Employee} from '../../login/shared/employee.model';
import {PropositionService} from '../shared/proposition.service';
import {Router} from '@angular/router';
import {FormControl, FormGroup, Validators} from '@angular/forms';

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

  editedProp: Proposition;

  modalString: string;
  editPropGroup: FormGroup;

  constructor(private propositionService: PropositionService, private router: Router) {
  }

  ngOnInit() {
    this.proposition = this.propositionService.getCurrentProposition();
    this.modalString = '';
    this.editedProp = this.proposition;
    this.editPropGroup = new FormGroup({
      title: new FormControl(this.editedProp.title, Validators.required),
      description: new FormControl(this.editedProp.description, Validators.required),
      file: new FormControl()
    });
  }

  goBack() {
    this.router.navigateByUrl('customer/' + this.proposition.customerId);
  }

  delete() {
    this.propositionService.deleteProposition(this.proposition.id)
      .subscribe(prop => this.router.navigateByUrl('customer/' + prop.customerId));
  }

  openModal(toDo: string) {
    this.modalString = toDo;
    if (this.modalString === 'delete') {

    } else if (this.modalString === 'edit') {
      this.editPropGroup.reset();
    }
  }

  closeModal($event) {

  }

  getEUString(date: Date) {
    return this.propositionService.getCreationDateAsEUString(date);
  }

  save() {
    this.modalString = '';
  }
}

