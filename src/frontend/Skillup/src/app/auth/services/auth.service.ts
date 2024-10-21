import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private httpClient = inject(HttpClient);
  private accessTokenKey = 'auth-token';

  signIn(email: string, password: string) {
    return this.httpClient.post(environment.apiUrl + '/auth/account/sign-in', {
      email: email,
      password: password,
    });
  }

  signUp(email: string, password: string) {
    return this.httpClient.post(environment.apiUrl + '/auth/account/sign-up', {
      email: email,
      password: password,
    });
  }

  signOut() {
    // return this.httpClient.post(environment + '/auth/account/sign-out');
    localStorage.removeItem(this.accessTokenKey);
  }

  refreshTokens() {
    // return this.httpClient.post(environment + '/auth/token/refresh');
  }

  setAccessToken(acessToken: string): void {
    localStorage.setItem(this.accessTokenKey, acessToken);
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }
}
