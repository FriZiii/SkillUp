import { Component, computed, inject, input } from '@angular/core';
import { CourseListItem } from '../../../../models/course.model';
import { Router, RouterModule } from '@angular/router';
import { CartService } from '../../../../../finance/services/cart.service';
import { CoursesService } from '../../../../services/course.service';
import { PurchasedItemsService } from '../../../../services/purchasedItems.service';
import { UserService } from '../../../../../user/services/user.service';
import { UserRole } from '../../../../../user/models/user-role.model';
import { CommonModule } from '@angular/common';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { DiscountCodeService } from '../../../../../finance/services/discountCode.service';
import { DiscountCode, DiscountCodeType } from '../../../../../finance/models/discountCodes.model';
import { MiniCodeComponent } from "../../../../../finance/components/mini-code/mini-code.component";

@Component({
  selector: 'app-all-courses-course-item',
  standalone: true,
  imports: [CommonModule, RouterModule, RatingModule, FormsModule, MiniCodeComponent],
  templateUrl: './all-courses-course-item.component.html',
  styleUrl: './all-courses-course-item.component.css'
})
export class AllCoursesCourseItemComponent {
  course = input.required<CourseListItem>();
  router = inject(Router);
  cartService = inject(CartService);  
  courseService = inject(CoursesService);
  purchasedItemsService = inject(PurchasedItemsService);
  userService = inject(UserService);
  discountCodeService = inject(DiscountCodeService);

  discountCodes = computed(() => this.discountCodeService.findDiscountCodesByItemId(this.course().id));

  navigate(whereTo: string){
    switch (whereTo){
      case 'detail' :
        this.router.navigate(['/course-detail', this.course().id])
        break;
      case 'edit':
        this.router.navigate(['/course-edit', this.course().id, 'creator'])
        break;
      case 'cart':
        this.router.navigate(['/cart'])
        break;
    }
  }
}
