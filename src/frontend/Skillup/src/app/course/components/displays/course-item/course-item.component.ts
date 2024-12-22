import { Component, inject, Input, input, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { TruncatePipe } from '../../../../utils/pipes/truncate.pipe';
import { CourseListItem } from '../../../models/course.model';
import { Router } from '@angular/router';
import { CartService } from '../../../../finance/services/cart.service';
import { CoursesService } from '../../../services/course.service';
import { PurchasedItemsService } from '../../../services/purchasedItems.service';
import { UserService } from '../../../../user/services/user.service';
import { UserRole } from '../../../../user/models/user-role.model';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { BuyButtonComponent } from "../../buy-button/buy-button.component";

@Component({
  selector: 'app-course-item',
  standalone: true,
  imports: [ButtonModule, CardModule, TruncatePipe, TooltipModule, RatingModule, FormsModule, BuyButtonComponent],
  templateUrl: './course-item.component.html',
  styleUrl: './course-item.component.css',
})
export class CourseItemComponent{
  @Input() course!: CourseListItem;
  router = inject(Router);

  navigate(whereTo: string){
    switch (whereTo){
      case 'detail' :
        this.router.navigate(['/course-detail', this.course.id])
        break;
    }
  }

  
}
