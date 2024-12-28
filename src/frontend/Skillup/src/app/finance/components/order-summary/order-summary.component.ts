import { Component, computed, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { ButtonModule } from 'primeng/button';
import { WalletService } from '../../services/wallet.service';
import { RouterModule } from '@angular/router';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ConfirmationDialogHandlerService } from '../../../core/services/confirmation-handler.service';
import { CoursesService } from '../../../course/services/course.service';
import { CartItemComponent } from '../cart-item/cart-item.component';
import { CartEmptyComponent } from "../cart-empty/cart-empty.component";
import { DialogModule } from 'primeng/dialog';
import { CommonModule } from '@angular/common';
import { UserService } from '../../../user/services/user.service';

@Component({
  selector: 'app-order-summary',
  standalone: true,
  imports: [ButtonModule, RouterModule, ConfirmDialogModule, CartItemComponent, CartEmptyComponent, DialogModule, CommonModule],
  templateUrl: './order-summary.component.html',
  styleUrl: './order-summary.component.css'
})
export class OrderSummaryComponent {
  //Services
  cartService = inject(CartService);
  walletService = inject(WalletService);
  confirmationDialogService = inject(ConfirmationDialogHandlerService);
  courseService = inject(CoursesService)
  userService = inject(UserService);

  //Variables
  currentUser = this.userService.currentUser;
  cart = this.cartService.cart;
  wallet = this.walletService.currentWallet;
  dialogVisible = false;
  
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
        averageRating: course?.averageRating || 0,
        ratingsCount: course?.ratingsCount || 0,
      };
    }) || null;
  });
  
  numberOfCartItems = computed(() => this.cart()?.items.length ?? 0);
  discount = computed(() => {
    if(this.cart()?.totalBeforeDiscount !== this.cart()?.total){
      return this.cart()?.total! - this.cart()?.totalBeforeDiscount!
    }
    else{
      return 0
    }
  })

  purchaseCart(event: Event){
    this.confirmationDialogService.confirmSave(event, () => {
      this.cartService.checkoutCart().subscribe();
    })
  }
}
