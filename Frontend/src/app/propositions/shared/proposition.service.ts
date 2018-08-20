import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {Proposition} from './proposition.model';
import {Observable} from 'rxjs/Observable';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Customer} from '../../customers/shared/customer.model';


const url = environment.ApiEndPoint + '/propositions/';

@Injectable()
export class PropositionService {

  constructor(private http: HttpClient) {
  }

  getPropositionsByCustomerId(id: number): Observable<Proposition[]> {
    return this.http.get<Proposition[]>(url + id);
  }

  createProposition(prop: Proposition) {
    return this.http.post<Proposition>(url, prop);
  }

  deleteProposition(id: number) {
    return this.http.delete<Proposition>(url + id);
  }

  updateProposition(proposition: Proposition) {
    return this.http.put<Proposition>(url + proposition.id, proposition);
  }
}
