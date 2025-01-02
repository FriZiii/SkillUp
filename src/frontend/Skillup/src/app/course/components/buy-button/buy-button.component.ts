import { Component, inject, input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CartService } from '../../../finance/services/cart.service';
import { CourseListItem } from '../../models/course.model';
import { PurchasedItemsService } from '../../services/purchasedItems.service';
import { UserService } from '../../../user/services/user.service';
import { UserRole } from '../../../user/models/user-role.model';
import { CoursesService } from '../../services/course.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-buy-button',
  standalone: true,
  imports: [ButtonModule, CommonModule],
  templateUrl: './buy-button.component.html',
  styleUrl: './buy-button.component.css'
})
export class BuyButtonComponent {
  course = input.required<CourseListItem>();
  detail = input<boolean>(false);
  white = input<boolean>(false);
  //Services
  cartService = inject(CartService);
  purchasedItemsService = inject(PurchasedItemsService);
  userService = inject(UserService);
  courseService = inject(CoursesService)
  router = inject(Router);
  
  //Variables
  coursesByAuthor: CourseListItem[] = [] 

  ngOnInit(){
      if(this.userService.currentUser()){
        if(this.userService.currentUser()?.role === UserRole.Instructor){
          this.coursesByAuthor = this.courseService.courses().filter(c => c.authorId === this.userService.currentUser()?.id);
        }
      }
    }

  addToCart(){
    this.cartService.addToCart(this.course().id).subscribe();
  }

  isInCart(){
    if(this.cartService.cart()?.items.find(i => i.id === this.course().id)){
      return true;
    }
    else{
      return false;
    }
  }

  isPurchased(){
    if(this.purchasedItemsService.purchasedCourses().find(c => c.id === this.course().id)){
      return true;
    }
    else{
      return false;
    }
  }

  isAuthor(){
    if(this.coursesByAuthor.length !== 0 && this.coursesByAuthor.find(c => c.id === this.course().id)){
      return true;
    }
    else{
      return false
    }
  }

  navigate(whereTo: string){
    switch (whereTo){
      case 'detail' :
        this.router.navigate(['/course-detail', this.course().id])
        break;
      case 'walk-through' :
        this.router.navigate(['/course', this.course().id, 'walk-through'])
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
