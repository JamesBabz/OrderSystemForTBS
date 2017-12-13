import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Equipment} from './equipment.model';
import {environment} from '../../../environments/environment';
import {Customer} from '../../customers/shared/customer.model';

const url = environment.ApiEndPoint + '/equipments';

@Injectable()
export class EquipmentService {
  private currentCust: Customer;


  constructor(private http: HttpClient) {
    this.currentCust = null;
  }

  createEquipment(equip: Equipment) {
    return this.http.post<Equipment>(url, equip);
  }

  getEquipment(): Observable<[Equipment]> {

    return this.http
      .get<Equipment[]>(url);
  }

  getEquipmentById(id: number): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(url + '/' + id);
  }

  deleteEquipmentById(id: Number): Observable<Equipment> {
    return this.http.delete<Equipment>(url + '/' + id);
  }
  getCurrentCustomer() {
    return this.currentCust;
  }
}
