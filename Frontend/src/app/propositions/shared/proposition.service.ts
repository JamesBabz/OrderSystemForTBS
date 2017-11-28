import { Injectable } from '@angular/core';
import {environment} from '../../../environments/environment';
import {Proposition} from './proposition.model';
import {Observable} from 'rxjs/Observable';
import {HttpClient} from '@angular/common/http';



const url = environment.ApiEndPoint + '/propositions/';
@Injectable()
export class PropositionService {



  constructor(private http: HttpClient) { }

  getPropositionsByCustomerId(id: number): Observable<Proposition[]> {
    return this.http.get<Proposition[]>(url + id);
  }
}
