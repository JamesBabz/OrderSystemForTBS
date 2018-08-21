import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {Customer} from '../customers/shared/customer.model';

@Injectable()
export class SharedService {

  private currentCust: Customer;
  private tab: number;

  constructor(private http: HttpClient) {
    this.currentCust = null;
  }


  upLoadImage(file: string) {
    return this.http.post(environment.ApiEndPoint + '/files', '\"' + file + '\"');
  }
  getFileById(id: number): Observable<string> {
    return this.http.get(environment.ApiEndPoint + '/files/' + id);
  }
  deleteFileById(id: number) {
    return this.http.delete(environment.ApiEndPoint + '/files/' + id);
  }

  setCurrentCustomer(customer: Customer) {
    this.currentCust = customer;
  }

  getCurrentCustomer() {
    return this.currentCust;
  }

  setTab(tab: number) {
    this.tab = tab;
  }

  getTab() {
    return this.tab;
  }

  getDateAsEUString(date: Date) {
    const newDate = new Date(date);
    let dateString;
    const options = {year: 'numeric', month: 'numeric', day: 'numeric'};
    dateString = newDate.toLocaleString('en-GB', options);
    return dateString;
  }
}
