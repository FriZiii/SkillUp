import { Component, computed, inject, signal } from '@angular/core';
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
import { DialogModule } from 'primeng/dialog';
import { InputNumber, InputNumberModule } from 'primeng/inputnumber';

@Component({
  selector: 'app-balance',
  standalone: true,
  imports: [InputTextModule, ReactiveFormsModule, InputMaskModule, ButtonModule, CommonModule, ConfirmDialogModule, DialogModule, InputNumberModule],
  templateUrl: './balance.component.html',
  styleUrl: './balance.component.css'
})
export class BalanceComponent {
  //Services
  walletService = inject(WalletService);
  toastService = inject(ToastHandlerService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);

  //Variables
  addBalanceDialogVisible = false;
  wallet = computed(() => this.walletService.currentWallet());
  cardform = new FormGroup({
    value: new FormControl(0, {
      validators: [Validators.required],
    }),
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

  lol(){
    console.log(this.wallet())
  }
  purchaseCurrency(event: Event){
    const balance = this.cardform.controls.value.value;
    this.confirmationDialogService.confirmSave(event, () => {
      this.walletService.addBalance(balance!).pipe(
        tap((res) => {
          this.toastService.showSuccess('Successfully added: ' + balance + ' $');
        })).subscribe();
    })
  }
}
