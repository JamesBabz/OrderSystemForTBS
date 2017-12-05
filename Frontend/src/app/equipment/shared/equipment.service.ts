import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Equipment} from './equipment.model';
import {environment} from '../../../environments/environment';

const url = environment.ApiEndPoint + '/equipments';

@Injectable()
export class EquipmentService {

  constructor(private http: HttpClient) { }

  getEquipment(): Observable<[Equipment]> {

    return this.http
      .get<Equipment[]>(url);
  }

  getEquipmentById(id: number): Observable<Equipment[]> {
    return this.http.get<Equipment[]>(url + '/' + id);
  }
}
