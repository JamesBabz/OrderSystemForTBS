import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Employee} from './employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class EmployeeService {

  constructor(private http: HttpClient) {
  }

  getEmployee(): Observable<Employee> {
    const localStorageId = toNumber(localStorage.getItem('currentUser').split(',')[0].substr(6));
    return this.http.get<Employee>('http://localhost:55000/api/employees/' + localStorageId);
  }

}
