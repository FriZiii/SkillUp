import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AddNewDiscountCodeComponent } from "../../../finance/components/discount-codes/add-new-discount-code/add-new-discount-code.component";
import { DiscountCodeService } from '../../../finance/services/discountCode.service';
import { DiscountCode } from '../../../finance/models/discountCodes.model';
import { DiscountCodeItemComponent } from '../../../finance/components/discount-codes/discount-code-item/discount-code-item.component';
import { CourseItemShortComponent } from '../displays/course-item-short/course-item-short.component';
import { TabsModule } from 'primeng/tabs';
import { CourseStatus } from '../../models/course-status.model';

@Component({
  selector: 'app-courses-created-by-you',
  standalone: true,
  imports: [CourseItemShortComponent, ProgressSpinnerModule, TabsModule ],
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
  requiredChangesCourses = signal<Course[]>([]);
  loading = true;

  //Services
  courseService = inject(CoursesService);

  
  ngOnInit(): void {
    this.courseService.getCourseByAuthorId(this.authorId()!).subscribe(
      (res) => {
        this.courses.set(res);
        this.publishedCourses.set(this.courses().filter(c => c.status === CourseStatus.Published))
        this.reviewCourses.set(this.courses().filter(c => c.status === CourseStatus.SubmitedForReview || c.status === CourseStatus.PendingReview))
        this.requiredChangesCourses.set(this.courses().filter(c => c.status === CourseStatus.ChangesRequired))
        this.draftCourses.set(this.courses().filter(c => c.status === CourseStatus.Draft))
        this.loading = false;
      }
    )
  }
}
