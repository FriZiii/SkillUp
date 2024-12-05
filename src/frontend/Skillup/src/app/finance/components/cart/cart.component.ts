import { Component, computed, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CoursesService } from '../../../course/services/course.service';
import { CourseItemShortComponent } from "../../../course/components/course-item-short/course-item-short.component";
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { catchError, pipe } from 'rxjs';
import { CartItemComponent } from "../cart-item/cart-item.component";
import { CategoryService } from '../../../course/services/category.service';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [ButtonModule, InputTextModule, FormsModule, CartItemComponent],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
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
      console.log(cartItem);
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
  discountCode = '';
  invalidCode = false;

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

}
