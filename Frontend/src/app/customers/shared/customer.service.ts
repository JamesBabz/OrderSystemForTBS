import {Injectable} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Customer} from './customer.model';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import 'rxjs/add/operator/map';
import {Proposition} from '../../propositions/shared/proposition.model';

const url = environment.ApiEndPoint + '/customers';

@Injectable()
export class CustomerService {


  constructor(private http: HttpClient) {
  }

  getCustomers(): Observable<Customer[]> {

    return this.http
      .get<Customer[]>(url);
  }

  getCustomerById(id: number): Observable<Customer> {
    return this.http.get<Customer>(url + '/' + id);
  }

  createCustomer(cust: Customer): Observable<Customer> {
    return this.http.post<Customer>(url, cust);
  }

  updateCustomerById(id: number, cust: Customer): Observable<Customer> {
    return this.http.put<Customer>(url + '/' + id, cust);
  }

  deleteCustomerById(id: Number): Observable<Customer> {
    return this.http.delete<Customer>(url + '/' + id);
  }

  searchQuery(q: string): Observable<Customer[]> {
    return this.http.get<Customer[]>(url + '/search?q=' + q);
  }


}
