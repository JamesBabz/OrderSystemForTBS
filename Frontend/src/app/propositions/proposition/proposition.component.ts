import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {stringify} from 'querystring';
import {Employee} from '../../login/shared/employee.model';
import {getDayOfWeek} from 'ngx-bootstrap/bs-moment/utils/date-getters';
import {PropositionService} from '../shared/proposition.service';
import {debounceTime} from 'rxjs/operator/debounceTime';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {Button} from 'selenium-webdriver';

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
  isFileFound = true;
  @Output()
  eDeleted = new EventEmitter();
  editedProp: Proposition;

  modalString: string;
  editPropGroup: FormGroup;
  unsavedChanges: boolean;

  constructor(private propositionService: PropositionService, private router: Router) {
  }

  ngOnInit() {
    this.editedProp = Object.assign(Object.create(this.proposition), this.proposition);
    this.createFormGroup(this.proposition);
  }


  getEUString(date: Date) {
    return this.propositionService.getCreationDateAsEUString(date);
  }

  getFileById(event) {
    console.log(event.target.tagName);
    if (event.target.tagName === 'I') {
      return;
    }
    this.propositionService.getFileById(this.proposition.fileId).subscribe(File => {
      if (File !== null) {
        this.openPdf(File);
      } else {
        this.showNoFileAlert();
      }
    });
  }
  showNoFileAlert() {

    this.isFileFound = false;
    var x = document.getElementById('snackbar')
    x.className = 'show';
    setTimeout(function(){ x.className = x.className.replace('show', ''); }, 3000);
  }

  openPdf(base64: string) {
    var windo = window.open('q', '');
    var objbuilder = '';
    objbuilder += ('<embed width=\'100%\' height=\'100%\'  src="data:application/pdf;base64,');
    objbuilder += (base64);
    objbuilder += ('" type="application/pdf" />');
    windo.document.write(objbuilder);
  }

  createFormGroup(prop: Proposition) {
    this.editPropGroup = new FormGroup({
      title: new FormControl(prop.title, Validators.required),
      description: new FormControl(prop.description, Validators.required),
      file: new FormControl()
    });
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
      const values = this.editPropGroup.value;
      if (this.editedProp.title !== values.title || this.editedProp.description !== values.description) {
        // sets the temporary object to contain the input values
        console.log(this.editedProp.title);
        this.editedProp.title = values.title;
        this.editedProp.description = values.description;
        this.unsavedChanges = true;
      }
    } else if (!$event.srcElement.classList.contains('shouldKeepInput') && $event.srcElement.classList.contains('shouldClose')) {
      // resets the input values
      this.createFormGroup(this.proposition);
      this.unsavedChanges = false;
    }
    if ($event.srcElement.classList.contains('shouldClose')) {
      document.getElementsByTagName('BODY')[0].classList.remove('disableScroll');
      this.modalString = '';
    }
  }
  save($event) {
    this.closeModal($event);
    this.editedProp.id = this.proposition.id;
    this.unsavedChanges = false;
    this.propositionService.updateProposition(this.editedProp)
      .subscribe(prop => {
          this.proposition = prop,
          this.editedProp = Object.assign(Object.create(this.proposition), this.proposition);
      });
  }

  delete() {
    this.propositionService.deleteProposition(this.proposition.id)
      .subscribe(prop =>  this.eDeleted.emit(prop));
    if (this.isFileFound) {
      this.deleteFileById();
    }
  }
  deleteFileById() {
    this.propositionService.deleteFileById(this.proposition.fileId).subscribe();
  }

}
