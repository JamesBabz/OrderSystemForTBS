import { Injectable } from '@angular/core';
import {Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot} from '@angular/router';
import {LoginComponent} from '../login.component';
import {toString} from '@ng-bootstrap/ng-bootstrap/util/util';

@Injectable()
export class AuthGuard implements CanActivate {
  localStorageRole;

  constructor(private router: Router) {
    this.localStorageRole = toString(localStorage.getItem('currentUser').split('"')[7].substr(0));
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    if (localStorage.getItem('currentUser') != null && this.localStorageRole === 'Administrator') {

      return true;
    }

    this.router.navigate(['/customers'] );
  }
}
