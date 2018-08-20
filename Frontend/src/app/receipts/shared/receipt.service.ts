import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {Receipt} from './receipt.model';
import {Customer} from '../../customers/shared/customer.model';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

const url = environment.ApiEndPoint + '/receipts/';

@Injectable()
export class ReceiptService {

  private currentReceipt: Receipt;
  private currentCust: Customer;


  constructor(private http: HttpClient) {
    this.currentReceipt = null;
    this.currentCust = null;
  }

  getReceiptsByCustomerId(id: number): Observable<Receipt[]> {
    return this.http.get<Receipt[]>(url + id);
  }

  createReceipt(receipt: Receipt) {
    return this.http.post<Receipt>(url, receipt);
  }

  getCreationDateAsEUString(date: Date) {
    const newDate = new Date(date);
    let dateString;
    const options = {year: 'numeric', month: 'numeric', day: 'numeric'};
    dateString = newDate.toLocaleString('en-GB', options);
    return dateString;
  }

  setCurrentReceipt(receipt: Receipt) {
    this.currentReceipt = receipt;
  }

  setCurrentCustomer(customer: Customer) {
    this.currentCust = customer;
  }

  getCurrentCustomer() {
    return this.currentCust;
  }

  deleteReceipt(id: number) {
    return this.http.delete<Receipt>(url + id);
  }
  updateReceipt(receipt: Receipt) {
    return this.http.put<Receipt>(url + receipt.id, receipt);
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
}
