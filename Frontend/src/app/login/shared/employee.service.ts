import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Employee} from './employee.model';
import {toNumber} from 'ngx-bootstrap/timepicker/timepicker.utils';
import {environment} from '../../../environments/environment';
import {Observable} from 'rxjs/Observable';
import {Customer} from '../../customers/shared/customer.model';

const url = environment.ApiEndPoint + '/employees';

@Injectable()
export class EmployeeService {

  constructor(private http: HttpClient) {
  }


  getCurrentEmployee(): Observable<Employee> {
    const localStorageId = toNumber(localStorage.getItem('currentUser').split(',')[0].substr(6));
    return this.http.get<Employee>(url + "/" + localStorageId);
  }

  createEmployee(emp: Employee): Observable<Employee> {
    return this.http.post<Employee>(url, emp);
  }

  updateEmployeeById(id: number, emp: Employee): Observable<Employee> {
    return this.http.put<Employee>(url + "/" + id, emp);
  }

  getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<Employee>(url + "/" + id);
  }

  getEmployees(): Observable<Employee[]>{
    return this.http
      .get<Employee[]>(url);
  }

  deleteEmployeeById(id: number): Observable<Employee> {
    return this.http.delete<Employee>(url + '/' + id);
  }
}
