import { inject, Injectable, NgZone } from "@angular/core";
import { ConfirmationService, MessageService } from "primeng/api";
import { ToastHandlerService } from "./toast-handler.service";

@Injectable({ providedIn: 'root' })
export class ConfirmationDialogHandlerService {
    confirmationService = inject(ConfirmationService)
  toastService = inject(ToastHandlerService);
  ngZone = inject(NgZone);

  confirmSave(event: Event, acceptCallback: () => void) {
    this.confirmationService.confirm({
        target: event.target as EventTarget,
        message: 'Are you sure that you want to proceed?',
        header: 'Confirmation',
        closable: true,
        closeOnEscape: true,
        icon: 'pi pi-info-circle',
        rejectButtonProps: {
            label: 'Cancel',
            severity: 'secondary',
            outlined: true,
        },
        acceptButtonProps: {
            label: 'Save',
        },
        accept: () => {
            acceptCallback();
        },
        reject: () => {
          this.toastService.showError('You have rejected');
        },
    });
}

confirmDelete(event: Event, acceptCallback: () => void) {
  this.confirmationService.confirm({
      target: event.target as EventTarget,
      message: 'Do you want to delete this record?',
      header: 'Danger Zone',
      icon: 'pi pi-exclamation-triangle',
      rejectLabel: 'Cancel',
      rejectButtonProps: {
          label: 'Cancel',
          severity: 'secondary',
          outlined: true,
      },
      acceptButtonProps: {
          label: 'Delete',
          severity: 'danger',
      },

      accept: () => {
        acceptCallback();
      },
      reject: () => {
        this.toastService.showError('You have rejected');
      },
  });
}
}