import { Component, computed, inject, Input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { CourseListItem } from '../../../models/course.model';
import { Router } from '@angular/router';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { BuyButtonComponent } from "../../buy-button/buy-button.component";
import { DiscountCodeService } from '../../../../finance/services/discountCode.service';
import { DiscountCode, DiscountCodeType } from '../../../../finance/models/discountCodes.model';
import { MiniCodeComponent } from "../../../../finance/components/mini-code/mini-code.component";

@Component({
  selector: 'app-course-item',
  standalone: true,
  imports: [ButtonModule, CardModule, TooltipModule, RatingModule, FormsModule, BuyButtonComponent, MiniCodeComponent],
  templateUrl: './course-item.component.html',
  styleUrl: './course-item.component.css',
})
export class CourseItemComponent{
  @Input() course!: CourseListItem;
  router = inject(Router);
  
  discountCodeService = inject(DiscountCodeService);
  
  discountCodes = computed(() => this.discountCodeService.findDiscountCodesByItemId(this.course.id).slice(0, 1));

  navigate(whereTo: string){
    switch (whereTo){
      case 'detail' :
        this.router.navigate(['/course-detail', this.course.id])
        break;
    }
  }
}
