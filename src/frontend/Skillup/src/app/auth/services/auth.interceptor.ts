import { HttpInterceptorFn, HttpErrorResponse, HttpRequest, HttpHandlerFn, HttpEvent } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable, switchMap, catchError, throwError } from 'rxjs';

export const AuthInterceptor: HttpInterceptorFn = (
  req: HttpRequest<any>,
  next: HttpHandlerFn
): Observable<HttpEvent<any>> => {
  const authService = inject(AuthService);

  const modifiedReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${authService.getAccessToken()}`,
    },
  });

  return next(modifiedReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        console.log(
          '401 Unauthorized - Token has expired. Attempting to refresh token.'
        );

        return authService.refreshTokens().pipe(
          switchMap(() => {
            const newReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${authService.getAccessToken()}`,
              },
            });
            return next(newReq);
          }),
          catchError((err) => {
            console.error(
              'Failed to refresh token. Redirecting to login.',
              err
            );
            return throwError(() => err);
          })
        );
      }
      return throwError(() => error);
    })
  );
};
