import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { OrderService } from '../../services/order.service';
import { Order } from '../../models/order.model';
import { CommonModule } from '@angular/common';
import { CoursesService } from '../../../course/services/course.service';
import { CartItemComponent } from "../cart-item/cart-item.component";
import { ItemType } from '../../models/finance.model';

@Component({
  selector: 'app-order-page',
  standalone: true,
  imports: [CommonModule, CartItemComponent],
  templateUrl: './order-page.component.html',
  styleUrl: './order-page.component.css'
})
export class OrderPageComponent implements OnInit{
  orderId = input.required<string>();

  //Services
  orderService = inject(OrderService)
  courseService = inject(CoursesService);

  //Variables
  order = signal<Order | null>(null)
  courses = this.courseService.publishedCourses;
  filteredCourses = computed(() => this.courses().filter(course => 
    this.order()?.items.some(item => item.itemId === course.id)
  ).map((course) => ({
      id: course.id,
      orginalItem: {
        id: course.id,
        authorid: course.authorId,
        type: ItemType.Course,
        price:  course.price,
      },
      price: course.price,
      title: course.title,
      authorName:  course.authorName,
      thumbnailUrl: course.thumbnailUrl,
      averageRating: course.averageRating,
      ratingsCount: course.ratingsCount,
  })));
  

  ngOnInit(): void {
    this.orderService.getOrderByBalanceHistoryId(this.orderId()).subscribe(
      (res) => {
        this.order.set(res);
        console.log(this.order)
      }
    )
  }
}
