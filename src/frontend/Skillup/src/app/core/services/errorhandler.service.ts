import {
  ChangeDetectorRef,
  ErrorHandler,
  inject,
  Injectable,
  NgZone,
} from '@angular/core';
import { ToastHandlerService } from './toasthandler.service';
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService implements ErrorHandler {
  toastService = inject(ToastHandlerService);

  handleError(error: any): void {
    console.error('Error occurred:', error);
    if (error && error.message && !error.message.includes('offsetWidth')) {
      this.toastService.showError(error.message);
  }
  }
}