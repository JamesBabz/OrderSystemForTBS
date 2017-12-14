import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Visit} from './visit.model';
import {Observable} from 'rxjs/Observable';
import {Customer} from '../../customers/shared/customer.model';
import {forEach} from '@angular/router/src/utils/collection';
import {EmployeeService} from '../../login/shared/employee.service';

const url = environment.ApiEndPoint + '/visits/';

@Injectable()
export class VisitService {
  private currentCust: Customer;

  constructor(private http: HttpClient) {
    this.currentCust = null;
  }

  getAllVisits(): Observable<Visit[]> {
    return this.http.get<Visit[]>(url);
  }

  getVisitsByCustomerId(id: number): Observable<Visit[]> {
    return this.http.get<Visit[]>(url + id);
  }

  createVisit(visit: Visit): Observable<Visit> {
    return this.http.post(url, visit);
  }

  getDateAsEUString(date: Date) {
    const newDate = new Date(date);
    let dateString;
    const options = {year: 'numeric', month: 'numeric', day: 'numeric'};
    dateString = newDate.toLocaleString('en-GB', options);
    return dateString;
  }

  setCurrentCustomer(customer: Customer) {
    this.currentCust = customer;
  }

  getCurrentCustomer() {
    return this.currentCust;
  }
}
