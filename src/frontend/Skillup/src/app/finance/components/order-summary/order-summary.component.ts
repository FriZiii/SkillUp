import { Component, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ButtonModule } from 'primeng/button';
import { WalletService } from '../../services/wallet.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [ButtonModule, RouterModule],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css'
})
export class OrderSummaryComponent {
  //Services
  cartService = inject(CartService);
  walletService = inject(WalletService);

  //Variables
  cart = this.cartService.cart;
  wallet = this.walletService.currentWallet;
}
