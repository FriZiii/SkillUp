import { Component, computed, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ButtonModule } from 'primeng/button';
import { WalletService } from '../../services/wallet.service';
import { RouterModule } from '@angular/router';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationDialogHandlerService } from '../../../core/services/confirmation-handler.service';
import { CoursesService } from '../../../course/services/course.service';
import { CartItemComponent } from '../cart-item/cart-item.component';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [ButtonModule, RouterModule, ConfirmDialogModule, CartItemComponent],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css'
})
export class OrderSummaryComponent {
  //Services
  cartService = inject(CartService);
  walletService = inject(WalletService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);
  courseService = inject(CoursesService)

  //Variables
  cart = this.cartService.cart;
  wallet = this.walletService.currentWallet;
  
  courses = computed(() => this.cart()?.items.flatMap((item) => this.courseService.getCourseById(item.id)));
  cartItems = computed(() => {
    const courseList = this.courses();
  
    return this.cart()?.items?.map(cartItem => {
      const course = courseList?.find(c => c.id === cartItem.id);
      return {
        id: cartItem.id,
        orginalItem: cartItem.orginalItem,
        price: cartItem.price,
        title: course?.title || 'Unknown Title',
        authorName: course?.authorName || 'Unknown Author',
        thumbnailUrl: course?.thumbnailUrl || '',
      };
    }) || null;
  });

  purchaseCart(event: Event){
    this.confirmationDialogService.confirmSave(event, () => {
      this.cartService.checkoutCart().subscribe();
    })
  }
}
