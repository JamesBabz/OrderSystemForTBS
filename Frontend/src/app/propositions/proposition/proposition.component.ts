import {Component, Input, OnInit} from '@angular/core';
import {Proposition} from '../shared/proposition.model';
import {stringify} from 'querystring';
import {Employee} from '../../login/shared/employee.model';
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

  getFileById() {
    this.propositionService.getFileById(this.proposition.fileId).subscribe(File => this.openPdf(File) );
  }

  openPdf(base64: string) {
    var windo = window.open('q', '');
    var objbuilder = '';
    objbuilder += ('<embed width=\'100%\' height=\'100%\'  src="data:application/pdf;base64,');
    objbuilder += (base64);
    objbuilder += ('" type="application/pdf" />');
    windo.document.write(objbuilder);
  }
}
