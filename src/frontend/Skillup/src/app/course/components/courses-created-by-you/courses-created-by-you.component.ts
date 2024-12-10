import { Component, inject, input, OnInit, signal } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AddNewDiscountCodeComponent } from "../../../finance/components/add-new-discount-code/add-new-discount-code.component";
import { DiscountCodeService } from '../../../finance/services/discountCode.service';
import { DiscountCode } from '../../../finance/models/discountCodes.model';
import { DiscountCodeItemComponent } from '../../../finance/components/discount-code-item/discount-code-item.component';
import { CourseItemShortComponent } from '../displays/course-item-short/course-item-short.component';

@Component({
  selector: 'app-courses-created-by-you',
  standalone: true,
  imports: [CourseItemShortComponent, ProgressSpinnerModule, AddNewDiscountCodeComponent, DiscountCodeItemComponent],
  templateUrl: './courses-created-by-you.component.html',
  styleUrl: './courses-created-by-you.component.css'
})
export class CoursesCreatedByYouComponent implements OnInit {
  //Variables
  authorId = input<string>();
  courses = signal<Course[]>([]);
  loading = true;
  discountCodes = signal<DiscountCode[]>([]);

  //Services
  courseService = inject(CoursesService);
  discountCodeService = inject(DiscountCodeService);

  
  ngOnInit(): void {
    this.courseService.getCourseByAuthorId(this.authorId()!).subscribe(
      (res) => {
        this.courses.set(res);
        this.courses.set(this.courses().slice(0, 2));
        this.loading = false;
      }
    )
    this.discountCodeService.getDiscountCodesByOwner(this.authorId()!).subscribe(
      (res) => {
        this.discountCodes.set(res);
      }
    )
  }

  editCode(event: DiscountCode){
    this.discountCodeService.editDiscountCode(event.id, {
      code: event.code,
      discountValue: event.discountValue,
      appliesToEntireCart: event.appliesToEntireCart,
      isActive: event.isActive,
      isPublic: event.isPublic
    }).subscribe(
      (res) => {
        this.discountCodes.update((prevCodes) => 
          prevCodes.map(code => code.id === res.id ? {
            ...code, 
            code: res.code, 
            discountValue: res.discountValue, 
            appliesToEntireCart: res.appliesToEntireCart,
          isActive: res.isActive,
        isPublic: res.isPublic} : code));
      }
    );
  }

  deleteCode(codeId: string){
    this.discountCodeService.deleteDiscountCode(codeId).subscribe(
      (res) => this.discountCodes.set(this.discountCodes().filter(c => c.id !== codeId))
    );
  }

  addDiscountCode(code: DiscountCode){
    this.discountCodes.update((list) => [...list, code])
  }
}
