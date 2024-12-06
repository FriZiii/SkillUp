import { Component, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ButtonModule } from 'primeng/button';
import { WalletService } from '../../services/wallet.service';
import { RouterModule } from '@angular/router';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationDialogHandlerService } from '../../../core/services/confirmation-handler.service';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [ButtonModule, RouterModule, ConfirmDialogModule],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css'
})
export class OrderSummaryComponent {
  //Services
  cartService = inject(CartService);
  walletService = inject(WalletService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);

  //Variables
  cart = this.cartService.cart;
  wallet = this.walletService.currentWallet;

  purchaseCart(event: Event){
    this.confirmationDialogService.confirmSave(event, () => {
      this.cartService.checkoutCart().subscribe();
    })
  }
}
