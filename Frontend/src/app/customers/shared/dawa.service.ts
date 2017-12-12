import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class DawaService {

  constructor(private http: HttpClient) { }



  getCity(zipCode: number) {
    return this.http.get(environment.ApiEndPoint + '/dawas/' + zipCode);
  }
}
