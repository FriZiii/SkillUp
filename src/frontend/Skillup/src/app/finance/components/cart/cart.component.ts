import { Component, computed, inject } from '@angular/core';
import { CartService } from '../../services/cart.service';
import { CoursesService } from '../../../course/services/course.service';
import { CourseItemShortComponent } from "../../../course/components/course-item-short/course-item-short.component";
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CourseItemShortComponent, ButtonModule],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  //Services
  cartService = inject(CartService);
  courseService = inject(CoursesService);

  //Variables
  cart = this.cartService.cart;
  cartItems = computed(() => this.cart()?.items.flatMap((item) => this.courseService.getCourseById(item.id)))

  removeItem(event: Event){
    
  }
}
