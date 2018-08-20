import {Component, OnInit, ViewChild} from '@angular/core';
import {CalendarComponent} from 'ng-fullcalendar';
import {Options} from 'fullcalendar';
import {VisitService} from '../../visits/shared/visit.service';
import {Visit} from '../../visits/shared/visit.model';
import {EmployeeService} from '../../login/shared/employee.service';
import {Employee} from '../../login/shared/employee.model';
import {CustomerService} from '../../customers/shared/customer.service';
import {Router} from '@angular/router';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';
import {environment} from '../../../environments/environment';

const tbsLogo = environment.logoDataUrl;

@Component({
  selector: 'app-calendars',
  templateUrl: './calendars.component.html',
  styleUrls: ['./calendars.component.css']
})
export class CalendarsComponent implements OnInit {

  visits: Visit[];
  employees: Employee[];
  currentEmployee: Employee;
  employeeIdsToShow: number[];
  colorDone = false;

  private data: any[];
  dataToPrint: any[];

  calendarOptions: Options;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;


  constructor(private visitService: VisitService, private employeeService: EmployeeService, private customerService: CustomerService, private router: Router) {
    this.employeeService.getCurrentEmployee().subscribe(Employee => this.currentEmployee = Employee);

  }

  ngOnInit() {
    this.visitService.getAllVisits().subscribe(Visit => {
      this.visits = Visit;
    });
    this.employeeService.getEmployees().subscribe(emp => this.employees = emp);

    this.employeeIdsToShow = [];
    setTimeout(() => this.updateEmployeeIdsToShow(), 1000);
    setTimeout(() => this.addEvents(), 1000);
    // this.getSampleEvents();
    this.setCalendarOptions();
  }

  setColors() {
    const spans = document.getElementsByClassName('bullet');
    for (let i = 0; i < spans.length; i++) {
      spans.item(i).setAttribute('style', 'color: ' + spans.item(i).getAttribute('class').substr(0, 7));
    }
    // this.colorDone = true;
  }

  eventClick($event) {
    this.customerService.setTab(4);
    this.router.navigateByUrl('customer/' + $event.valueOf().event.customerId);

  }

  clickButton($event) {

  }

  updateEvent($event) {

  }

  addEvents() {
    this.ucCalendar.fullCalendar('removeEvents');
    this.data = [];
    let k = 0;
    for (let i = 0; i < this.visits.length; i++) {
      const currVisit = this.visits[i];

      if (!this.shouldThisShow(currVisit.employeeId)) {
        continue;
      }
      this.data[k] = ({
        id: currVisit.id,
        title: currVisit.customer.firstname + ' ' + currVisit.customer.lastname + '/ ' + currVisit.customer.companyName,
        start: currVisit.dateTimeOfVisitStart.toString(),
        end: currVisit.dateTimeOfVisitEnd.toString(),
        color: currVisit.employee.colorCode,
        customerId: currVisit.customerId,
        employee: currVisit.employee,
        customer: currVisit.customer,
        titleOfVisit: currVisit.title,
        className: 'clickable'
      });
      k++;
    }
    if (k === 0) {
      return;
    }
    this.dataToPrint = this.data;
    this.ucCalendar.fullCalendar('addEventSource', this.data);
  }

  getSampleEvents() {
    this.data = [
      {
        title: 'All Day Event',
        start: '2017-12-01',
        color: 'red'
      },
      {
        title: 'Long Event',
        start: '2017-12-07'
      },
      {
        id: 999,
        title: 'Long Event',
        start: '2017-12-12T16:00:00',
        end: '2017-12-12T18:00:00'
      },
      {
        id: 999,
        title: 'Repeating Event',
        start: '2017-12-16T16:00:00'
      },
      {
        title: 'Conference',
        start: '2017-12-11'
      },
      {
        title: 'Meeting',
        start: '2017-12-12T10:30:00'
      },
      {
        title: 'Lunch',
        start: '2017-12-12T12:00:00'
      },
      {
        title: 'Meeting',
        start: '2017-12-12T14:30:00'
      },
      {
        title: 'Happy Hour',
        start: '2017-12-12T17:30:00'
      },
      {
        title: 'Dinner',
        start: '2017-12-12T20:00:00'
      },
      {
        title: 'Birthday Party',
        start: '2017-12-13T07:00:00'
      },
      {
        title: 'Click for Google',
        url: 'http://google.com/',
        start: '2017-12-28'
      }
    ];
  }


  private setCalendarOptions() {
    this.calendarOptions = {
      editable: false,
      locale: 'da-dk',
      timeFormat: 'H:mm',
      eventLimit: false,
      slotLabelFormat: 'H:mm',
      monthNames: ['Januar', 'Februar', 'Marts', 'April', 'Maj', 'Juni', 'Juli',
        'August', 'September', 'Oktober', 'November', 'December'],
      monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'Maj', 'Jun', 'Jul',
        'Aug', 'Sep', 'Okt', 'Nov', 'Dec'],
      dayNames: [' Søndag', ' Mandag', ' Tirsdag', ' Onsdag',
        ' Torsdag', ' Fredag', ' Lørdag'],
      dayNamesShort: ['Søn', 'Man', 'Tirs', 'Ons',
        'Tors', 'Fre', 'Lør'],
      allDayText: 'Hele dagen',
      buttonText: {today: 'Idag', month: 'Måned', week: 'Uge', day: 'Dag', list: 'Liste'},
      header: {left: 'prev,next today', center: 'title', right: 'ExcelButton month,agendaWeek,agendaDay,listMonth'},
      events: this.data
    };
  }
  private shouldThisShow(currId) {
    for (let x = 0; x < this.employeeIdsToShow.length; x++) {
      if (currId === this.employeeIdsToShow[x]) {
        return true;
      }
    }
    return false;
  }

  private updateEmployeeIdsToShow() {
    for (let x = 0; x < this.employees.length; x++) {
      this.employeeIdsToShow.push(this.employees[x].id);
    }
  }

  private toggle(employeeId) {
    const index = this.employeeIdsToShow.indexOf(employeeId);
    if (index > -1) {
      this.employeeIdsToShow.splice(index, 1);
    } else {
      this.employeeIdsToShow.push(employeeId);
    }
    this.addEvents();
  }

  printPdf() {
    let rowNumb = 1;
    const salesMen = [];
    const customers = [];
    const visitTitles = [];
    const dates = [];
    const times = [];
    const customFirmNames = [];
    const rowNumber = [];
    for (let x of this.data){
      dates.push(x.start.substring(0, 10));
      const startTime = x.start.substring(11);
      const finallyStartTime = startTime.substring(0, 5);
      const endTime = x.end.substring(11);
      const finallyEndTime = endTime.substring(0, 5);
      const time = finallyStartTime + '-' + finallyEndTime;
      times.push(time);
      salesMen.push(x.employee.firstname + ' ' + x.employee.lastname);
      customers.push(x.customer.firstname + ' ' + x.customer.lastname);
      customFirmNames.push(x.customer.companyName);
      visitTitles.push(x.titleOfVisit);
      rowNumber.push(rowNumb ++);
    }
    this.generatePdfFile(rowNumber, dates, times, salesMen, customers, customFirmNames, visitTitles);
  }

  generatePdfFile(rowNumber: string[], dates: string[], times: string[], salesMen: string[], customers: string[], customFirmNames: string[], visitTitles: string[] ) {
    pdfMake.vfs = pdfFonts.pdfMake.vfs;
    const date = new Date;
    const docDefinition = {
      pageOrientation: 'landscape',
      pageMargins: [40, 60, 40, 0],
      pageBreak: 'before',
      footer: {
        columns: [
          { text: this.customerService.getDateAsEUString(date), margin: [ 50, -50, 10, 20 ] },
          {
            text: 'Printet af: ' + this.currentEmployee.firstname + ' ' + this.currentEmployee.lastname, margin: [ 10, -50, 10, 20 ]
          }
        ]
      },
      header: {
        columns: [

          {
            margin: 10,
            image: 'data:image/png;' + tbsLogo + ',...encodedContent...',
            width: 120
          },
          {
            margin: 20,
            text: 'Besøgsoversigt', style: 'header',
          }
        ]
      },
      content: [
        {
          layout: 'lightHorizontalLines',
          margin: [ 0, 25, 10, 0 ],
          table: {
            headerRows: 1,
            widths: ['auto', 'auto', 'auto', 'auto', 'auto', 'auto', '*'],

            body: [
              ['Nr', 'Dato', 'Tid', 'Sælger', 'Kunde', 'Virksomhed', 'Besøgstitel'],
              [ rowNumber, dates, times, salesMen, customers, customFirmNames, visitTitles ]
            ]
          }
        }
      ],
      styles: {
        header: {
          fontSize: 22,
          bold: true
        },
        headLine: {
          bold: true
        }
      }
    };
    pdfMake.createPdf(docDefinition).download('Kalender_print_' + date);
  }
}
