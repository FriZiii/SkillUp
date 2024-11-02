import {
  ChangeDetectorRef,
  ErrorHandler,
  inject,
  Injectable,
  NgZone,
} from '@angular/core';
import { ToastHandlerService } from './toast-handler.service';
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService implements ErrorHandler {
  toastService = inject(ToastHandlerService);

  handleError(error: any): void {
    console.error('Error occurred:', error);
    if (error.status === 502) {
      this.toastService.showError('Api not working');
    }
    if (error.error.errors[0].code) {
      this.toastService.showError(error.error.errors[0].message);
    } else if (
      error &&
      error.message &&
      !error.message.includes('offsetWidth')
    ) {
      this.toastService.showError(error.message);
    }
  }
}
