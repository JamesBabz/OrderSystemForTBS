import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SalesmanList} from './salesmanList.model';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';

const url = environment.ApiEndPoint + '/SalesmanList/';
@Injectable()
export class SalesmanListService {

  isP20ListShowed: boolean;

  constructor(private http: HttpClient) { }
  getSalesmanList(id: number): Observable<SalesmanList[]> {
    return this.http
      .get<SalesmanList[]>(url + id);
  }
  createSalesmanList(salesmanList: SalesmanList): Observable<SalesmanList> {
    return this.http.post<SalesmanList>(url, salesmanList);
  }
  removeCustomerFromP20(id: Number): Observable<SalesmanList> {
    return this.http.delete<SalesmanList>(url + id);
  }
  addCustomerToP20(salesmanList: SalesmanList): Observable<SalesmanList> {
    return this.http.post<SalesmanList>(url, salesmanList);
  }
  setP20ListShowed(bool: boolean) {
    this.isP20ListShowed = bool;
  }
  getP20ListShowed() {
    return this.isP20ListShowed;
  }
}


