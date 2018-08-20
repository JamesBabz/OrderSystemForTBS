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
import {NotifierService} from 'angular-notifier';
import {NotificationsService} from 'angular2-notifications';
import {SharedService} from '../../shared/shared.service';

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
  isFileFound = false;
  @Output()
  eDeleted = new EventEmitter();
  editedProp: Proposition;

  modalString: string;
  editPropGroup: FormGroup;
  unsavedChanges: boolean;
  base64textString: string;
  upLoadedAImage = false;
  isNewFileSelected = false;
  prenstFile: string;
  doDeleteFile = false;
  correctFile = true;


  constructor(private propositionService: PropositionService, private sharedService: SharedService ) {

  }

  ngOnInit() {
    this.editedProp = Object.assign(Object.create(this.proposition), this.proposition);
    this.createFormGroup(this.proposition);
    this.sharedService.getFileById(this.proposition.fileId).subscribe(file => this.prenstFile = file);
  }


  getEUString(date: Date) {
    return this.sharedService.getCreationDateAsEUString(date);
  }

  getFileById(event) {
    if (event.target.tagName === 'I') {
      return;
    }
    this.sharedService.getFileById(this.proposition.fileId).subscribe(File => {
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
    var windo = window.open('q', '' );
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

  onFileChange(event) {

    var files = event.target.files;
    var file = files[0];
    if (files && file && file.type.indexOf('pdf') > -1) {
      var reader = new FileReader();

      reader.onload = this._handleReaderLoaded.bind(this);

      reader.readAsBinaryString(file);
      this.upLoadedAImage = true;
      this.correctFile = true;
    } else {
      this.correctFile = false;
    }
  }

  _handleReaderLoaded(readerEvt) {

    var binaryString = readerEvt.target.result;
    this.base64textString = btoa(binaryString);

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
        this.editedProp.title = values.title;
        this.editedProp.description = values.description;
        this.unsavedChanges = true;

      }
    } else if (!$event.srcElement.classList.contains('shouldKeepInput') && $event.srcElement.classList.contains('shouldClose')) {
      // resets the input values
      this.createFormGroup(this.proposition);
      this.unsavedChanges = false;
      this.isNewFileSelected = false;
    }
    if ($event.srcElement.classList.contains('shouldClose')) {
      document.getElementsByTagName('BODY')[0].classList.remove('disableScroll');
      this.modalString = '';
    }
    this.isFileFound = false;

  }
  save($event) {
    const timeStamp = Date.now();
    const oldTimeStamp = this.proposition.fileId;
    this.closeModal($event);
    this.editedProp.id = this.proposition.id;
    this.editedProp.fileId = timeStamp;
    this.unsavedChanges = false;
    this.propositionService.updateProposition(this.editedProp)
      .subscribe(prop => {
          this.proposition = prop,
          this.editedProp = Object.assign(Object.create(this.proposition), this.proposition);
      });
    if (this.upLoadedAImage) {
      this.sharedService.getFileById(oldTimeStamp).subscribe(file => {
        if (file) {
          this.deleteFileById(oldTimeStamp);
        }
      });
      this.sharedService.upLoadImage(this.base64textString +  'å' + timeStamp).subscribe();
      this.prenstFile = this.base64textString;
    }
  }

  delete() {
    this.propositionService.deleteProposition(this.proposition.id)
      .subscribe(prop =>  this.eDeleted.emit(prop));
    console.log(this.proposition.fileId);
    if (this.proposition.fileId !== 0) {
      this.deleteFileById(this.proposition.fileId);
    }
  }
  deleteFileById(id: number) {
    this.sharedService.deleteFileById(id).subscribe();
  }
  confirmedDeleteFile() {
    this.deleteFileById(this.proposition.fileId);
    this.doDeleteFile = false;
    this.prenstFile = null;
  }



}
