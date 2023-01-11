import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/enviroments/environment';
import { authenticationResponse } from '../models/security';
import { userCredentials } from '../models/security';
import { resetPwCredentials } from '../models/security';
import { newPwCredentials } from '../models/security';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private apiURL = environment.apiURL + "/account"
  private readonly tokenKey: string = 'token'
  private readonly expirationTokenKey: string = 'token-expiration'
  private readonly roleFiled = "role"

  isAuthenticated(): boolean {
    const token = localStorage.getItem(this.tokenKey);
    if (!token) {
      return false;
    }
    const expiration: any = localStorage.getItem(this.expirationTokenKey);
    const expirationDate = new Date(expiration)

    if (expirationDate <= new Date()) {
      this.logout();
      return false;
    }
    else {
      return true;
    }
  }

  logout() {
    localStorage.removeItem(this.tokenKey)
    localStorage.removeItem(this.expirationTokenKey)
  }

  getFieldFromJWT(field: string): string {
    const token = localStorage.getItem(this.tokenKey);
    if (!token) {
      return ''
    }
    const dataToken = JSON.parse(atob(token.split('.')[1]))
    return dataToken[field];
  }

  getRole(): string {
    return this.getFieldFromJWT(this.roleFiled);
  }

  register(userCredentials: userCredentials): Observable<authenticationResponse> {
    return this.http.post<authenticationResponse>(this.apiURL + "/register", userCredentials)
  }

  login(userCredentials: userCredentials): Observable<authenticationResponse> {
    return this.http.post<authenticationResponse>(this.apiURL + "/authenticate", userCredentials)
  }

  forgotpw(resetPwCredentials: resetPwCredentials) {
    return this.http.post<authenticationResponse>(this.apiURL + "/forgotpw", resetPwCredentials)
  }

  resetpassword(resetPwCredentials: newPwCredentials) {
    return this.http.post<authenticationResponse>(this.apiURL + "/resetpassword", resetPwCredentials)
  }

  saveToken(authenticationResponse: authenticationResponse) {
    localStorage.setItem(this.tokenKey, authenticationResponse.token)
    localStorage.setItem(this.expirationTokenKey, authenticationResponse.expiration.toString())
  }

  getToken() {
    return localStorage.getItem(this.tokenKey)
  }

  isTheSameUser(id: string): boolean {
    return id === this.getFieldFromJWT('uid');
  }
}