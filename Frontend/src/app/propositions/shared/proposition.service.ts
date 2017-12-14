import {Injectable} from '@angular/core';
import {environment} from '../../../environments/environment';
import {Proposition} from './proposition.model';
import {Observable} from 'rxjs/Observable';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Customer} from '../../customers/shared/customer.model';


const url = environment.ApiEndPoint + '/propositions/';

@Injectable()
export class PropositionService {

  private currentProp: Proposition;
  private currentCust: Customer;


  constructor(private http: HttpClient) {
    this.currentProp = null;
    this.currentCust = null;
  }

  getPropositionsByCustomerId(id: number): Observable<Proposition[]> {
    return this.http.get<Proposition[]>(url + id);
  }

  createProposition(prop: Proposition) {
    return this.http.post<Proposition>(url, prop);
  }

  uploadFileToDropbox(file: File) {
    return this.http.post('https://content.dropboxapi.com/2/files/upload', 'sdfdsfdsfdsfdsfds', {
      headers: new HttpHeaders()
        .set('Authorization', 'Bearer FIATtzvU2VAAAAAAAAAAI1hnBsvo44i7mRvbvyu4L47dVLhZC_yfiIYt_lozlC7t')
        .set('Content-Type', 'application/octet-stream')
        .set('Dropbox-API-Arg', '{"path": "/Homework/math/Matrices.txt","mode": "add","autorename": true,"mute": false}')
    });
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

  setCurrentCustomer(customer: Customer) {
    this.currentCust = customer;
  }

  getCurrentCustomer() {
    return this.currentCust;
  }

  deleteProposition(id: number) {
    return this.http.delete<Proposition>(url + id);
  }

  updateProposition(proposition: Proposition) {
    return this.http.put<Proposition>(url + proposition.id, proposition);
  }

  upLoadImage(file: string) {
    return this.http.post(environment.ApiEndPoint + '/files', '\"' + file + '\"');
  }

  getAllFileIds(): Observable<number[]> {
    return this.http.get<number[]>(environment.ApiEndPoint + '/files');
  }
  getFileById(id: number): Observable<string> {
    return this.http.get(environment.ApiEndPoint + '/files/' + id);
  }
}
