import { provideHttpClient } from '@angular/common/http';
import { ApplicationConfig, ErrorHandler } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { ErrorHandlerService } from './handlers/ErrorHandlerService';
import { MessageService } from 'primeng/api';

export const appConfig: ApplicationConfig = {
  providers: [provideHttpClient(), provideAnimationsAsync(), MessageService, {provide: ErrorHandler, useClass: ErrorHandlerService}]
};
