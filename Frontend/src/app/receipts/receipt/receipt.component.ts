import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Employee} from '../../login/shared/employee.model';
import {Receipt} from '../shared/receipt.model';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ReceiptService} from '../shared/receipt.service';
import {SharedService} from '../../shared/shared.service';

@Component({
  selector: 'app-receipt',
  templateUrl: './receipt.component.html',
  styleUrls: ['./receipt.component.css']
})
export class ReceiptComponent implements OnInit {


  @Input()
  receipt: Receipt;
  @Input()
  employee: Employee;
  @Output()
  eDeleted = new EventEmitter();
  editedProp: Receipt;


  modalString: string;
  editPropGroup: FormGroup;
  unsavedChanges: boolean;
  base64textString: string;
  upLoadedAImage = false;
  isNewFileSelected = false;
  prenstFile: string;
  doDeleteFile = false;
  correctFile = true;
  isFileFound = false;


  constructor(private receiptService: ReceiptService, private sharedService: SharedService) {
  }


  ngOnInit() {
    this.editedProp = Object.assign(Object.create(this.receipt), this.receipt);
    this.createFormGroup(this.receipt);
    this.sharedService.getFileById(this.receipt.fileId).subscribe(file => this.prenstFile = file);
  }


  getEUString(date: Date) {
    return this.sharedService.getDateAsEUString(date);
  }

  getFileById(event) {
    if (event.target.tagName === 'I') {
      return;
    }
    this.sharedService.getFileById(this.receipt.fileId).subscribe(File => {
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

  createFormGroup(prop: Receipt) {
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
      this.createFormGroup(this.receipt);
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
    const oldTimeStamp = this.receipt.fileId;
    this.closeModal($event);
    this.editedProp.id = this.receipt.id;
    this.editedProp.fileId = timeStamp;
    this.unsavedChanges = false;
    this.receiptService.updateReceipt(this.editedProp)
      .subscribe(prop => {
        this.receipt = prop,
          this.editedProp = Object.assign(Object.create(this.receipt), this.receipt);
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
    this.receiptService.deleteReceipt(this.receipt.id)
      .subscribe(prop =>  this.eDeleted.emit(prop));
    if (this.receipt.fileId !== 0) {
      this.deleteFileById(this.receipt.fileId);
    }
  }
  deleteFileById(id: number) {
    this.sharedService.deleteFileById(id).subscribe();
  }
  confirmedDeleteFile() {
    this.deleteFileById(this.receipt.fileId);
    this.doDeleteFile = false;
    this.prenstFile = null;
  }



}
