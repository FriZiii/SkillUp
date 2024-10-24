import {
  ChangeDetectorRef,
  ErrorHandler,
  inject,
  Injectable,
  NgZone,
} from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService implements ErrorHandler {
  messageService = inject(MessageService);
  ngZone = inject(NgZone);

  handleError(error: any): void {
    console.error('Error occurred:', error);
    this.ngZone.run(() => {
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: error.error.errors[0].message,
        life: 5000,
      });
    });
  }
}
