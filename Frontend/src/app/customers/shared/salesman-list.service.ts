import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {SalesmanList} from './salesmanList.model';
import {Observable} from 'rxjs/Observable';
import {environment} from '../../../environments/environment';

const url = environment.ApiEndPoint + '/SalesmanList/';
@Injectable()
export class SalesmanListService {

  constructor(private http: HttpClient) { }
  getSalesmanList(id: number): Observable<SalesmanList[]> {
    return this.http
      .get<SalesmanList[]>(url + id);
  }
}
