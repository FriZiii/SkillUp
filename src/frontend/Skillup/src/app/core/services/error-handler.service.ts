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
  lastToast: string | null = null;

  handleError(error: any): void {
     if(this.lastError === error.message){
      return;
    }
    this.lastError = error.message;
    
    console.log(error);
    if(error.status === 502){
      if(this.lastToast === 'Api not working'){
        return;
      }
      this.toastService.showError('Api not working');
      this.lastToast = 'Api not working'
    }
    else{
    }
    /*
    //console.error('Error occurred: ', error);
    if (error.status === 502) {
      this.toastService.showError('Api not working');
      console.log(error)
    }
    if (error.error?.errors?.[0]?.code) {
      this.toastService.showError(error.error.errors[0].message);
      console.log(error)
    } else if (
      error &&
      error.message &&
      !error.message.includes('offsetWidth')
    ) {
      this.toastService.showError(error.message);
      console.log(error)
    } */

    /* const errorMessage = this.getErrorMessage(error);
    if (this.lastError === errorMessage) {
      return;
    }
    this.lastError = errorMessage;

    console.error('Error occurred:', error);
    this.toastService.showError(errorMessage);

    setTimeout(() => (this.lastError = null), 5000); */
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
