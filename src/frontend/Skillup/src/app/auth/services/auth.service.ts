import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { map, tap } from 'rxjs';
import { MessageService } from 'primeng/api';
import { UserService } from '../../user/services/user.service';
import { Router } from '@angular/router';
import { ToastHandlerService } from '../../core/services/ToastHandlerService';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private toastService = inject(ToastHandlerService);
  private httpClient = inject(HttpClient);
  private userService = inject(UserService);
  private router = inject(Router);

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
            this.toastService.showSuccessToast('Signed in successful');
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

  signUp(email: string, password: string, allowEmails: boolean) {
    return this.httpClient
      .post(
        environment.apiUrl + '/auth/account/sign-up',
        {
          email: email,
          password: password,
          allowMarketingEmails: allowEmails
        },
        { observe: 'response' }
      )
      .pipe(
        tap((response: any) => {
          if (response.status >= 200 && response.status < 300) {
            this.toastService.showSuccessToast('Signed up successful');
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
            this.toastService.showSuccessToast('Account has been activated');
          }
        })
      );
  }

  signOut() {
    return this.httpClient
      .delete(environment.apiUrl + '/auth/account/sign-out')
      .pipe(
        tap(() => {
          this.removeAccessToken();
          this.router.navigate(['/']);
        })
      );
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

  autoSignIn() {
    const token = this.getAccessToken();
    if (!token) {
      return;
    }
    this.userService.setUserFromToken(token);
  }

  private handleAuthentication(token: string) {
    this.removeAccessToken();
    this.setAccessToken(token);

    this.userService.setUserFromToken(token);

    this.handleRedirecting();
  }

  private handleRedirecting() {
    this.router.navigate(['/']);
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
