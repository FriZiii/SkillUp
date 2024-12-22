import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AddNewDiscountCodeComponent } from "../../../finance/components/add-new-discount-code/add-new-discount-code.component";
import { DiscountCodeService } from '../../../finance/services/discountCode.service';
import { DiscountCode } from '../../../finance/models/discountCodes.model';
import { DiscountCodeItemComponent } from '../../../finance/components/discount-code-item/discount-code-item.component';
import { CourseItemShortComponent } from '../displays/course-item-short/course-item-short.component';
import { TabsModule } from 'primeng/tabs';
import { CourseStatus } from '../../models/course-status.model';

@Component({
  selector: 'app-courses-created-by-you',
  standalone: true,
  imports: [CourseItemShortComponent, ProgressSpinnerModule, AddNewDiscountCodeComponent, DiscountCodeItemComponent, TabsModule ],
  templateUrl: './courses-created-by-you.component.html',
  styleUrl: './courses-created-by-you.component.css'
})
export class CoursesCreatedByYouComponent implements OnInit {
  //Variables
  authorId = input<string>();
  courses = signal<Course[]>([]);
  publishedCourses = signal<Course[]>([]);
  draftCourses = signal<Course[]>([]);
  reviewCourses = signal<Course[]>([]);
  loading = true;
  discountCodes = signal<DiscountCode[]>([]);
  numOfPublishedCourses = computed(() => this.publishedCourses().length);

  //Services
  courseService = inject(CoursesService);
  discountCodeService = inject(DiscountCodeService);

  
  ngOnInit(): void {
    this.courseService.getCourseByAuthorId(this.authorId()!).subscribe(
      (res) => {
        this.courses.set(res);
        this.publishedCourses.set(this.courses().filter(c => c.status === CourseStatus.Published))
        this.reviewCourses.set(this.courses().filter(c => c.status === CourseStatus.SubmitedForReview || c.status === CourseStatus.PendingReview || c.status === CourseStatus.ChangesRequired))
        this.draftCourses.set(this.courses().filter(c => c.status === CourseStatus.Draft))
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
