import { inject, Injectable, NgZone } from "@angular/core";
import { MessageService } from "primeng/api";

@Injectable({ providedIn: 'root' })
export class ToastHandlerService{
    messageService = inject(MessageService);
    ngZone = inject(NgZone);

    showError(message: string){
        this.ngZone.run(() => {
            this.messageService.add({
            severity: 'error',
            summary: 'Error',
            detail: message,
            life: 5000,
            });
        });
    }

    showSuccess(message: string) {
        this.ngZone.run(() => {
            this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: message,
            life: 5000,
            });
        });
    }

    showInfo(message: string) {
        this.ngZone.run(() => {
            this.messageService.add({
            severity: 'info',
            summary: 'Info',
            detail: message,
            life: 5000,
            });
        });
    }

    showWarn(message: string) {this.ngZone.run(() => {
        this.messageService.add({
        severity: 'warn',
        summary: 'Warn',
        detail: message,
        life: 5000,
        });
    });
    }
}