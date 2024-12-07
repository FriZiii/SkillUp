import { Component, inject, Input, input, OnInit } from '@angular/core';
import { Course, CourseListItem } from '../../models/course.model';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { TruncatePipe } from "../../../utils/pipes/truncate.pipe";
import { Router } from '@angular/router';
import { CartService } from '../../../finance/services/cart.service';
import { CoursesService } from '../../services/course.service';
import { PurchasedItemsService } from '../../services/purchasedItems.service';
import { UserService } from '../../../user/services/user.service';
import { UserRole } from '../../../user/models/user-role.model';

@Component({
  selector: 'app-course-item',
  standalone: true,
  imports: [ButtonModule, CardModule, TruncatePipe, TooltipModule],
  templateUrl: './course-item.component.html',
  styleUrl: './course-item.component.css',
})
export class CourseItemComponent{
  @Input() course!: CourseListItem;
  router = inject(Router);
  cartService = inject(CartService);  
  courseService = inject(CoursesService);
  purchasedItemsService = inject(PurchasedItemsService);
  userService = inject(UserService);
  coursesByAuthor: CourseListItem[] = [] 

  ngOnInit(){
    if(this.userService.currentUser()){
      if(this.userService.currentUser()?.role === UserRole.Instructor){
        this.coursesByAuthor = this.courseService.getCoursesByAuthor(this.userService.currentUser()!.id)
      }
    }
  }

  onClick(){
    this.router.navigate(['/course-detail', this.course.id])
  }

  addToCart(){
    this.cartService.addToCart(this.course.id).subscribe();
  }

  ifCanAddToCart(){
    if(this.cartService.cart()?.items.find(i => i.id === this.course.id)){
      return false;
    }
    if(this.purchasedItemsService.purchasedCourses().find(c => c.id === this.course.id)){
      return false;
    }
    if(this.coursesByAuthor.length !== 0 && this.coursesByAuthor.find(c => c.id === this.course.id)){
      return false;
    }
    else{
      return true
    }
  }
}
