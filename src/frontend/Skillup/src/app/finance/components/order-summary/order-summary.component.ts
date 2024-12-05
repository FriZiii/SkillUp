import { Component, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css'
})
export class OrderSummaryComponent {
  //Services
  cartService = inject(CartService);

  //Variables
  cart = this.cartService.cart;
}
