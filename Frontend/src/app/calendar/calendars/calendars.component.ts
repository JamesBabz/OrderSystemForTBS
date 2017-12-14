import {Component, OnInit, ViewChild} from '@angular/core';
import {CalendarComponent} from 'ng-fullcalendar';
import {Options} from 'fullcalendar';
import {VisitService} from '../../visits/shared/visit.service';
import {Visit} from '../../visits/shared/visit.model';
import {waitForMap} from '@angular/router/src/utils/collection';
import {EmployeeService} from '../../login/shared/employee.service';
import {Employee} from '../../login/shared/employee.model';
import {CustomerService} from '../../customers/shared/customer.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-calendars',
  templateUrl: './calendars.component.html',
  styleUrls: ['./calendars.component.css']
})
export class CalendarsComponent implements OnInit {

  visits: Visit[];

  private data: any[];

  calendarOptions: Options;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;


  constructor(private visitService: VisitService, private employeeService: EmployeeService, private customerService: CustomerService, private router: Router) {
  }

  ngOnInit() {
    this.visitService.getAllVisits().subscribe(Visit => {
      this.visits = Visit;
      // this.addEvents();
    });
    setTimeout(() => this.addEvents(), 500);
    // this.getSampleEvents();
    this.setCalendarOptions();
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
    this.data = [];
    for (let i = 0; i < this.visits.length; i++) {
      const currVisit = this.visits[i];
      this.data[i] = ({
        id: currVisit.id,
        title: currVisit.title,
        start: currVisit.dateTimeOfVisitStart.toString(),
        end: currVisit.dateTimeOfVisitEnd.toString(),
        color: currVisit.employee.colorCode,
        customerId: currVisit.customerId
      });
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
      dayNames: ['Søndag', 'Mandag', 'Tirsdag', 'Onsdag',
        'Torsdag', 'Fredag', 'Lørdag'],
      dayNamesShort: ['Søn', 'Man', 'Tirs', 'Ons',
        'Tors', 'Fre', 'Lør'],
      allDayText: 'Hele dagen',
      buttonText: {today: 'Idag', month: 'Måned', week: 'Uge', day: 'Dag', list: 'Liste'},
      header: {left: 'prev,next today', center: 'title', right: 'month,agendaWeek,agendaDay,listMonth'},
      events: this.data
    };
  }
}
