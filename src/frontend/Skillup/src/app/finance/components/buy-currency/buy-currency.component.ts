import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputMaskModule } from 'primeng/inputmask';
import { ButtonModule } from 'primeng/button';
import { WalletService } from '../../services/wallet.service';

@Component({
  selector: 'app-buy-currency',
  standalone: true,
  imports: [InputTextModule, FormsModule, InputMaskModule, ButtonModule],
  templateUrl: './buy-currency.component.html',
  styleUrl: './buy-currency.component.css'
})
export class BuyCurrencyComponent {
  //Services
  walletService = inject(WalletService);

  //Variables
  cardNumber = 0;
  expirationDate = '';
  CVC = 0;
  ownerName = '';

  purchaseCurrency(event: Event){
    this.walletService.addBalance(100).subscribe();
  }
}
