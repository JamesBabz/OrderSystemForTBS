import {Component, OnInit, ViewChild} from '@angular/core';
import {CalendarComponent} from 'ng-fullcalendar';
import {Options} from 'fullcalendar';
import {VisitService} from '../../visits/shared/visit.service';
import {Visit} from '../../visits/shared/visit.model';
import {forEach, waitForMap} from '@angular/router/src/utils/collection';
import {EmployeeService} from '../../login/shared/employee.service';
import {Employee} from '../../login/shared/employee.model';
import {CustomerService} from '../../customers/shared/customer.service';
import {Router} from '@angular/router';
import {saveAs} from 'file-saver/FileSaver';
import * as pdfMake from 'pdfmake/build/pdfmake';
import * as pdfFonts from 'pdfmake/build/vfs_fonts';

@Component({
  selector: 'app-calendars',
  templateUrl: './calendars.component.html',
  styleUrls: ['./calendars.component.css']
})
export class CalendarsComponent implements OnInit {

  visits: Visit[];
  employees: Employee[];
  employeeIdsToShow: number[];
  colorDone = false;

  private data: any[];

  calendarOptions: Options;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;


  constructor(private visitService: VisitService, private employeeService: EmployeeService, private customerService: CustomerService, private router: Router) {
    pdfMake.vfs = pdfFonts.pdfMake.vfs;
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
    this.colorDone = true;
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
        title: currVisit.title,
        start: currVisit.dateTimeOfVisitStart.toString(),
        end: currVisit.dateTimeOfVisitEnd.toString(),
        color: currVisit.employee.colorCode,
        customerId: currVisit.customerId,
        className: 'clickable'
      });
      k++;
    }
    if (k === 0) {
      return;
    }
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
      customButtons: {
        ExcelButton: {
          text: 'Excel',
          click: function exportToExcel() {
            //   const seperator = ',';
              const headers = [];
              headers[0] = 'Dato';
              headers[1] = 'Dag';
              headers[2] = 'Tidspunkt';
              headers[3] = 'Besøg';
            //
            //   let content = 'sep=' + seperator + '\n';
            //   for (let i = 0; i < headers.length; i++) {
            //     content += headers[i] + seperator;
            //   }
            //   content += '\n' + document.getElementsByClassName('fc-list-table')[0].textContent;
            //   for (let i = 0; i < content.length; i++) {
            //     content = content.replace(' ', seperator);
            //   }
            //
            //   const blob = new Blob([JSON.stringify(content)], {
            //     type: 'application/pdf;charset=utf-8'
            //   });
            //   saveAs(blob, 'Report.pdf');


            const htmltable = document.getElementsByClassName('fc-list-table ');
            const html = htmltable[0].outerHTML;
            var dd = {
              content: [
                {
                  layout: 'lightHorizontalLines', // optional
                  table: {
                    // headers are automatically repeated if the table spans over multiple pages
                    // you can declare how many rows should be treated as headers
                    headerRows: 1,
                    widths: [ '*', 'auto', 100, '*' ],

                    body: [
                      headers,
                      [ 'Value 1', 'Value 2', 'Value 3', 'Value 4' ],
                      [ { text: 'Bold value', bold: true }, 'Val 2', 'Val 3', 'Val 4' ]
                    ]
                  }
                }
              ]
            };
            pdfMake.createPdf(dd).download();

            // const htmltable = document.getElementsByClassName('fc-list-table ');
            // const html = htmltable[0].outerHTML;
            // window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));


            // const htmltable = document.getElementsByClassName('fc-list-table ');
            // const html = htmltable[0].outerHTML;
            // window.open('data:application/vnd.ms-excel,' + encodeURIComponent(html));
          }


        }
      },
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
}
