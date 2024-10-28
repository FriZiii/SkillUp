import {
  HTTP_INTERCEPTORS,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { ApplicationConfig, ErrorHandler } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter, withComponentInputBinding } from '@angular/router';
import { routes } from './app.routes';
import { MessageService } from 'primeng/api';
import { ErrorHandlerService } from './core/services/errorhandler.service';
import { AuthInterceptor } from './auth/services/auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(withInterceptors([AuthInterceptor])),
    provideAnimationsAsync(),
    provideRouter(routes, withComponentInputBinding()),
    MessageService,
    { provide: ErrorHandler, useClass: ErrorHandlerService },
  ],
};
