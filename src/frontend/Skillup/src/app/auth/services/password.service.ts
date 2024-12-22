import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { map, tap } from 'rxjs';
import { ToastHandlerService } from '../../core/services/toast-handler.service';

@Injectable({ providedIn: 'root' })
export class PasswordService {
  private toastService = inject(ToastHandlerService);
  private httpClient = inject(HttpClient);

  changePassword(currentPassword: string, newPassword: string) {
    return this.httpClient
      .post(
        environment.apiUrl + '/auth/password/change',
        {
          currentPassword: currentPassword,
          newPassword: newPassword,
        },
        { observe: 'response' }
      )
      .pipe(
        tap((response: any) => {
          if (response.status >= 200 && response.status < 300) {
            this.toastService.showSuccess('Password changed successful');
          }
        })
      );
  }

  sendPasswordResetInstruction(email: string) {
    return this.httpClient
      .post(
        environment.apiUrl + `/auth/password/reset/request?email=${email}`,
        {
          observe: 'response',
        }
      )
      .pipe(
        tap((response: any) => {
          if (response.status >= 200 && response.status < 300) {
            this.toastService.showSuccess('Password changed successful');
          }
        })
      );
  }

  resetPassword(token: string, newPassword: string) {
    return this.httpClient
      .post(
        environment.apiUrl + '/auth/password/reset/subbmit',
        {
          token: token,
          newPassword: newPassword,
        },
        { observe: 'response' }
      )
      .pipe(
        tap((response: any) => {
          if (response.status >= 200 && response.status < 300) {
            this.toastService.showSuccess('Password changed successful');
          }
        })
      );
  }
}
