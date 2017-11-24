import { Injectable } from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Customer} from './customer.model';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import 'rxjs/add/operator/map';

@Injectable()
export class CustomerService {

  constructor(private http: HttpClient) {
  }

  getCustomers(): Observable<Customer[]> {

    return this.http
      .get<Customer[]>(environment.ApiEndPoint + '/customers');
  }
}
