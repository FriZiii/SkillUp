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
    /*  if(this.lastError === error.message){
      return;
    } */
    this.lastError = error.message;
    
    if(error.status === 0){
      if(this.lastToast === 'Api not working'){
        return;
      }
      this.toastService.showError('Api not working');
      this.lastToast = 'Api not working'
    }
    else if(error.status === 415){
      if(this.lastToast === error.statusText){
        return;
      }
      this.toastService.showError( error.statusText);
      this.lastToast =  error.statusText
    }
    else if(error.status === 400 && error.error?.errors?.[0]?.code){
      this.toastService.showError(error.error.errors[0].message);  //badRequest
    }
    else if(error.status === 401 && error.error?.errors?.[0]?.code){
      this.toastService.showError(error.error.errors[0].message);  //unAthorized
    }
    else if(error.status === 45 && error.error?.errors?.[0]?.code){
      this.toastService.showError(error.error.errors[0].message);
    }
    else if(error.status === 500){
      this.toastService.showError(error.error.errors[0].message);
    }
    else{
      if(this.lastError === error.message){
      return;
      }
      this.lastError = error.message;
      console.log(error);
    }
  }
}
