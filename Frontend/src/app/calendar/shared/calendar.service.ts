import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Customer} from '../../customers/shared/customer.model';
import {environment} from '../../../environments/environment';

const url = environment.ApiEndPoint + '/calendars';

@Injectable()
export class CalendarService {


  constructor(private http: HttpClient) {

  }

  connectToOutlookApi() {
    return this.http.get('https://outlook.office.com/owa/?realm=calendars.read.shared');
  }

}
