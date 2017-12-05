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

  editedProp: Proposition;

  modalString: string;
  editPropGroup: FormGroup;

  constructor(private propositionService: PropositionService, private router: Router) {
  }

  ngOnInit() {
    this.proposition = this.propositionService.getCurrentProposition();
    this.modalString = '';
    this.editedProp = Object.assign(Object.create(this.proposition), this.proposition);
    this.createFormGroup(this.editedProp);
  }

  goBack() {
    this.router.navigateByUrl('customer/' + this.proposition.customerId);
  }

  delete() {
    this.propositionService.deleteProposition(this.proposition.id)
      .subscribe(prop => this.router.navigateByUrl('customer/' + prop.customerId));
  }

  openModal(toDo: string) {
    document.getElementsByTagName('BODY')[0].classList.add('disableScroll');
    this.modalString = toDo;
  }

  /**
   * closes the modal.
   * reads css classes from the clicked element.
   * shouldKeepInput class lets the changed input stay in the fields
   * shouldClose class is to prevent child elements from closing
   * @param $event
   */
  closeModal($event) {
    if ($event.srcElement.classList.contains('shouldKeepInput') && $event.srcElement.classList.contains('shouldClose')) {
      // sets the temporary object to contain the input values
      const values = this.editPropGroup.value;
      this.editedProp.title = values.title;
      this.editedProp.description = values.description;
    } else if (!$event.srcElement.classList.contains('shouldKeepInput') && $event.srcElement.classList.contains('shouldClose')) {
      // resets the input values
      this.createFormGroup(this.proposition);
    }
    if ($event.srcElement.classList.contains('shouldClose')) {
      document.getElementsByTagName('BODY')[0].classList.remove('disableScroll');
      this.modalString = '';
    }
  }

  getEUString(date: Date) {
    return this.propositionService.getCreationDateAsEUString(date);
  }


  save($event) {
    this.closeModal($event);
    this.editedProp.id = this.proposition.id;
    this.propositionService.updateProposition(this.editedProp)
      .subscribe(prop => {
        prop.employee = this.propositionService.getCurrentProposition().employee,
          this.proposition = prop,
          this.editedProp = prop;
      });
  }

  createFormGroup(prop: Proposition) {
    this.editPropGroup = new FormGroup({
      title: new FormControl(prop.title, Validators.required),
      description: new FormControl(prop.description, Validators.required),
      file: new FormControl()
    });
  }
}

