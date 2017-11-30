import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';

@Injectable()
export class AuthService {

  public getToken(): string {
    return localStorage.getItem('token');
  }
}

