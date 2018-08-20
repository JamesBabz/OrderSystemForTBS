import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {Receipt} from './receipt.model';
import {Customer} from '../../customers/shared/customer.model';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

const url = environment.ApiEndPoint + '/receipts/';

@Injectable()
export class ReceiptService {



  constructor(private http: HttpClient) {
  }

  getReceiptsByCustomerId(id: number): Observable<Receipt[]> {
    return this.http.get<Receipt[]>(url + id);
  }

  createReceipt(receipt: Receipt) {
    return this.http.post<Receipt>(url, receipt);
  }


  deleteReceipt(id: number) {
    return this.http.delete<Receipt>(url + id);
  }
  updateReceipt(receipt: Receipt) {
    return this.http.put<Receipt>(url + receipt.id, receipt);
  }

}
