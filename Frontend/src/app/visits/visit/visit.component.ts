import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Visit} from '../shared/visit.model';
import {VisitService} from '../shared/visit.service';
import {Employee} from '../../login/shared/employee.model';


@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.css']
})

export class VisitComponent implements OnInit {
 updateVisitSelected: boolean;
  editVisit: Visit;
  modalString: string;
  @Input()
  visit: Visit;
  @Input()
  employee: Employee;
  @Input()
  outDatedVisit: string;
  @Output()
  vDeleted = new EventEmitter();
  constructor(private visitService: VisitService) {
    this.updateVisitSelected = false;
  }

  ngOnInit() {
  }
  getEUString(date: Date) {
    return this.visitService.getDateAsEUString(date);
  }

  getTimeOfDate(date: Date) {
    return date.toString().substring(11);
  }
  cancel() {
    this.editVisit = Object.assign({}, this.visit);
  }
  updateVisit() {
    this.updateVisitSelected = true;
  }
  deleteVisit() {
    this.visitService.deleteVisit(this.visit.id).subscribe(Visit => {
      this.vDeleted.emit(Visit);
    });
  }
  openModal(toDo: string) {
    document.getElementsByTagName('BODY')[0].classList.add('disableScroll');
    this.modalString = toDo;
  }
  closeModal($event) {
    if ($event.srcElement.classList.contains('shouldClose')) {
      document.getElementsByTagName('BODY')[0].classList.remove('disableScroll');
      this.modalString = '';
    }
  }
  delete($event) {
    this.closeModal($event);
    this.deleteVisit();
  }

}
