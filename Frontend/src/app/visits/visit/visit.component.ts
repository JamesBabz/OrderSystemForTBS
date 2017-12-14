import {Component, Input, OnInit} from '@angular/core';
import {Visit} from '../shared/visit.model';
import {VisitService} from '../shared/visit.service';
import {Employee} from '../../login/shared/employee.model';
import {Customer} from '../../customers/shared/customer.model';

@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.css']
})

export class VisitComponent implements OnInit {
  editVisit: Visit;
  modalString: string;
  @Input()
  visit: Visit;
  @Input()
  employee: Employee;
  constructor(private visitService: VisitService) {
  }

  ngOnInit() {
  }
  getEUString(date: Date) {
    return this.visitService.getDateAsEUString(date);
  }

  updateVisit() {
    this.visitService.updateVisit(this.visit.id, this.visit).subscribe(Visit => this.visit = Visit);
  }
  deleteVisit(){
    this.visitService.deleteVisit(this.visit.id).subscribe();
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
}
