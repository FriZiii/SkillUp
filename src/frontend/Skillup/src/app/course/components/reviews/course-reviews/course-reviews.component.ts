import { Component, computed, inject } from '@angular/core';
import { CoursesService } from '../../../services/course.service';
import { CourseStatus } from '../../../models/course-status.model';
import { CourseItemShortComponent } from "../../displays/course-item-short/course-item-short.component";
import { ButtonModule } from 'primeng/button';
import { CourseReviewService } from '../../../services/course-review.service';

@Component({
  selector: 'app-course-reviews',
  standalone: true,
  imports: [CourseItemShortComponent, ButtonModule],
  templateUrl: './course-reviews.component.html',
  styleUrl: './course-reviews.component.css'
})
export class CourseReviewsComponent {
  //Services
  courseService = inject(CoursesService);
  courseReviewService = inject(CourseReviewService)

  coursesWaitingForReview = computed(() => this.courseService.courses().filter(c => c.status === CourseStatus.SubmitedForReview)
  .map((course) => this.courseService.mapCourseItemToCourse(course)));

  click(){
    console.log(this.courseService.courses().find(c => c.id === '06ab3291-5b7c-4894-916d-e00d259fd0d4'));
    console.log(this.courseService.courses().find(c => c.id === '0818c1a5-d6da-4072-9108-8773d25f2c61'));
    console.log(this.courseService.courses());
  }

  startReview(courseId: string){
    this.courseReviewService.startReview(courseId).subscribe();
  }
}
