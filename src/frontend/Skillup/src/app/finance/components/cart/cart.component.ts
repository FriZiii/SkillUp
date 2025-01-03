import { AfterContentInit, Component, computed, inject, OnInit } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CoursesService } from '../../../course/services/course.service';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CartItemComponent } from "../cart-item/cart-item.component";
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CartEmptyComponent } from "../cart-empty/cart-empty.component";

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [ButtonModule, InputTextModule, FormsModule, CartItemComponent, RouterModule, CommonModule, CartEmptyComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent implements OnInit {
  //Services
  cartService = inject(CartService);
  courseService = inject(CoursesService);

  //Variables
  cart = this.cartService.cart;
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
  discountCode = '';
  invalidCode = false;
  numberOfCartItems = computed(() => this.cart()?.items.length ?? 0);

  
  ngOnInit(): void {
    new Promise(resolve => setTimeout(resolve, 1000))
    .then(() => {
        this.discountCode = this.cart()?.discountCode?.code ?? '';
    });
  }

  removeItem(itemId: string){
    this.cartService.deleteItemFromCart(itemId).subscribe();
  }

  applyCode(){
    this.invalidCode = false;
    this.cartService.toggleDiscountCodeForCart(this.discountCode).subscribe({
      error: (error) => {
        this.invalidCode = true;
      },
    }
    ); 
  }

  delCode(){
    this.cartService.delCode().subscribe();
  }

}
