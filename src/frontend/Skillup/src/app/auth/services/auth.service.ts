import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { map, tap } from 'rxjs';
import { MessageService } from 'primeng/api';
import { UserService } from '../../user/services/user.service';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private messageService = inject(MessageService);
  private httpClient = inject(HttpClient);
  private userService = inject(UserService);

  private accessTokenKey = 'access-token';

  signIn(email: string, password: string) {
    return this.httpClient
      .post<{ token: string }>(
        environment.apiUrl + '/auth/account/sign-in',
        {
          email: email,
          password: password,
        },
        { observe: 'response', withCredentials: true }
      )
      .pipe(
        map((response: HttpResponse<{ token: string }>) => {
          if (response.status >= 200 && response.status < 300) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Sign in successful',
              life: 5000,
            });
            const token = response.body?.token;
            if (token) {
              this.handleAuthentication(token);
            } else {
              throw new Error('Token not found in response');
            }
          }
        })
      );
  }

  signUp(email: string, password: string) {
    return this.httpClient
      .post(
        environment.apiUrl + '/auth/account/sign-up',
        {
          email: email,
          password: password,
        },
        { observe: 'response' }
      )
      .pipe(
        tap((response: any) => {
          if (response.status >= 200 && response.status < 300) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Sign up successful',
              life: 5000,
            });
          }
        })
      );
  }

  activateAccount(userId: string, activationToken: string) {
    const params = new HttpParams()
      .set('userId', userId)
      .set('activationToken', activationToken);

    return this.httpClient
      .put(
        environment.apiUrl + '/auth/account/activation',
        {},
        { params, observe: 'response' }
      )
      .pipe(
        tap((response: any) => {
          if (response.status >= 200 && response.status < 300) {
            this.messageService.add({
              severity: 'success',
              summary: 'Success',
              detail: 'Account has been activated',
              life: 5000,
            });
          }
        })
      );
  }

  signOut() {
    // return this.httpClient.post(environment + '/auth/account/sign-out');
    localStorage.removeItem(this.accessTokenKey);
  }

  refreshTokens() {
    return this.httpClient
      .post<{ token: string }>(
        environment.apiUrl + '/auth/token/refresh',
        {},
        { observe: 'response', withCredentials: true }
      )
      .pipe(
        map((response: HttpResponse<{ token: string }>) => {
          if (response.status >= 200 && response.status < 300) {
            const token = response.body?.token;
            if (token) {
              this.handleAuthentication(token);
            } else {
              throw new Error('Token not found in response');
            }
          }
        })
      );
  }

  private handleAuthentication(token: string) {
    this.removeAccessToken();
    this.setAccessToken(token);

    this.userService.setUserFromToken(token);
  }

  private setAccessToken(accessToken: string): void {
    localStorage.setItem(this.accessTokenKey, accessToken);
  }

  private removeAccessToken(): void {
    localStorage.removeItem(this.accessTokenKey);
    this.userService.clearUser();
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.accessTokenKey);
  }
}
