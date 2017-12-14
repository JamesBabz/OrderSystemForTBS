import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';

@Injectable()
export class CVRService {

  constructor(private http: HttpClient) { }



  getCVR(query: string) {
    return this.http.get(environment.ApiEndPoint + '/CVR/' + query);
  }
}
