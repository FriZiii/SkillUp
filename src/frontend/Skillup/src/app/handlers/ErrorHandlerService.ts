import { ChangeDetectorRef, ErrorHandler, inject, Injectable, NgZone } from "@angular/core";
import { MessageService } from 'primeng/api';

@Injectable({ providedIn: 'root' })
export class ErrorHandlerService implements ErrorHandler{
    messageService = inject(MessageService);
    ngZone = inject(NgZone);

    handleError(error: any): void {
        this.ngZone.run(() => {
            this.messageService.add({
                severity: 'error',
                summary: 'Error',
                detail: error.message,
                life: 5000 
            });
        });
        console.error('Error occurred:', error);
    }
}