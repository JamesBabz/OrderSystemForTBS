import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {Proposition} from './proposition.model';
import {Observable} from 'rxjs/Observable';
import {HttpClient} from '@angular/common/http';


const url = environment.ApiEndPoint + '/propositions/';

@Injectable()
export class PropositionService {

  private currentProp: Proposition;


  constructor(private http: HttpClient) {
    this.currentProp = null;
  }

  getPropositionsByCustomerId(id: number): Observable<Proposition[]> {
    return this.http.get<Proposition[]>(url + id);
  }

  getCreationDateAsEUString(date: Date) {
    const newDate = new Date(date);
    let dateString;
    const options = {year: 'numeric', month: 'numeric', day: 'numeric'};
    dateString = newDate.toLocaleString('en-GB', options);
    return dateString;
  }

  setCurrentProposition(prop: Proposition) {
    this.currentProp = prop;
  }

  getCurrentProposition() {
    return this.currentProp;
  }


}
