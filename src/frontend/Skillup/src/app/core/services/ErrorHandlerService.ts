import {
  ChangeDetectorRef,
  ErrorHandler,
  inject,
  Injectable,
  NgZone,
} from '@angular/core';
import { ToastHandlerService } from './ToastHandlerService';
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService implements ErrorHandler {
  toastService = inject(ToastHandlerService);
  messageService = inject(MessageService);
  ngZone = inject(NgZone);

  handleError(error: any): void {
    console.error('Error occurred:', error);
    this.toastService.showErrorToast(error.error.errors[0].message);
    this.ngZone.run(() => {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: "errrrrrrr",
        life: 5000,
      });
    });
  }
}
