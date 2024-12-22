import { Component, computed, inject, input } from '@angular/core';
import { CartItemForDisplay } from '../../../../finance/models/cart.model';
import { CartItemComponent } from "../../../../finance/components/cart-item/cart-item.component";
import { TruncatePipe } from "../../../../utils/pipes/truncate.pipe";
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { CartService } from '../../../../finance/services/cart.service';
import { CoursesService } from '../../../../course/services/course.service';

@Component({
  selector: 'app-mini-cart',
  standalone: true,
  imports: [TruncatePipe, CommonModule, RouterModule],
  templateUrl: './mini-cart.component.html',
  styleUrl: './mini-cart.component.css'
})
export class MiniCartComponent {
  cartService = inject(CartService)
  courseService = inject(CoursesService)
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
}
