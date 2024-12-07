import { Component, inject, signal } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { ButtonModule } from 'primeng/button';
import { WalletService } from '../../services/wallet.service';
import { CommonModule } from '@angular/common';
import { tap } from 'rxjs';
import { ToastHandlerService } from '../../../core/services/toast-handler.service';
import { ConfirmationDialogHandlerService } from '../../../core/services/confirmation-handler.service';
import { ConfirmDialogModule } from 'primeng/confirmdialog';

@Component({
  selector: 'app-buy-currency',
  standalone: true,
  imports: [InputTextModule, ReactiveFormsModule, InputMaskModule, ButtonModule, CommonModule, ConfirmDialogModule],
  templateUrl: './buy-currency.component.html',
  styleUrl: './buy-currency.component.css'
})
export class BuyCurrencyComponent {
  //Services
  walletService = inject(WalletService);
  toastService = inject(ToastHandlerService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);

  //Variables
  cardform = new FormGroup({
    cardNumber: new FormControl('', {
      validators: [Validators.minLength(16), Validators.required],
    }),
    expirationDate: new FormControl('', {
      validators: [Validators.minLength(4), Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\d{2}$/)],
    }),
    CVC: new FormControl('', {
      validators: [Validators.minLength(3), Validators.required],
    }),
    ownerName: new FormControl('', {
      validators: [Validators.required],
    })
  });

  purchaseCurrency(event: Event, balance: number){
    this.confirmationDialogService.confirmSave(event, () => {
      this.walletService.addBalance(balance).pipe(
        tap((res) => {
          this.toastService.showSuccess('Successfully added: ' + balance + ' $');
        })).subscribe();
    })
  }
}
