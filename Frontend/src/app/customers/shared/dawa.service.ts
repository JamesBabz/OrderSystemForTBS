import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class DawaService {

  constructor(private http: HttpClient) {
  }


  getCity(zipCode: number){
    // return this.http.get('https://dawa.aws.dk/postnumre?nr=' + zipCode);
    return this.http.get(environment.ApiEndPoint + '/dawas/' + zipCode);
  }
}
