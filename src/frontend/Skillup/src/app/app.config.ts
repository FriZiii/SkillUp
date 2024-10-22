import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, ErrorHandler } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { provideRouter } from '@angular/router';
import { routes } from './app.routes';
import { ErrorHandlerService } from './handlers/ErrorHandlerService';
import { MessageService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [
    provideHttpClient(),
    provideAnimationsAsync(),
    provideRouter(routes),
    MessageService,
    { provide: ErrorHandler, useClass: ErrorHandlerService },
  ],
};
