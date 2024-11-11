import {
  ErrorHandler,
  inject,
  Injectable,
} from '@angular/core';
import { ToastHandlerService } from './toast-handler.service';

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService implements ErrorHandler {
  toastService = inject(ToastHandlerService);
  lastError: string | null = null;

  handleError(error: any): void {
    const errorMessage = this.getErrorMessage(error);
    if (this.lastError === errorMessage) {
      return;
    }
    this.lastError = errorMessage;

    console.error('Error occurred:', error);
    this.toastService.showError(errorMessage);

    setTimeout(() => (this.lastError = null), 5000);
  }

  private getErrorMessage(error: any): string {
    if (error.status === 502) {
      return 'Api not working';
    }
    if (error.error?.errors?.[0]?.code) {
      return error.error.errors[0].message;
    }
    if (error && error.message && !error.message.includes('offsetWidth')) {
      return error.message;
    }
    return 'An unknown error occurred';
  }
}
