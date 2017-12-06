import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Visit} from './visit.model';
import {Observable} from 'rxjs/Observable';

const url = environment.ApiEndPoint + '/visits/';
@Injectable()
export class VisitService {

  constructor(private http: HttpClient) { }

  getVisitsByCustomerId(id: number): Observable<Visit[]> {
    return this.http.get<Visit[]>(url + id);
  }

  getDateAsEUString(date: Date) {
    const newDate = new Date(date);
    let dateString;
    const options = {year: 'numeric', month: 'numeric', day: 'numeric'};
    dateString = newDate.toLocaleString('en-GB', options);
    return dateString;
  }
}
