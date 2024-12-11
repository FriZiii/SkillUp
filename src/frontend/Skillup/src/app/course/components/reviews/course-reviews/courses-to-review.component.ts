import { Component, computed, inject } from '@angular/core';
import { CoursesService } from '../../../services/course.service';
import { CourseStatus } from '../../../models/course-status.model';
import { CourseItemShortComponent } from "../../displays/course-item-short/course-item-short.component";
import { ButtonModule } from 'primeng/button';
import { CourseReviewService } from '../../../services/course-review.service';
import { TabsModule } from 'primeng/tabs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-courses-to-review',
  standalone: true,
  imports: [CourseItemShortComponent, ButtonModule, TabsModule],
  templateUrl: './courses-to-review.component.html',
  styleUrl: './courses-to-review.component.css'
})
export class CoursesToReviewComponent {
  //Services
  courseService = inject(CoursesService);
  courseReviewService = inject(CourseReviewService)
  router = inject(Router);

  coursesWaitingForReview = computed(() => this.courseService.courses().filter(c => c.status === CourseStatus.SubmitedForReview)
  .map((course) => this.courseService.mapCourseItemToCourse(course)));

  coursesWithStartedReviews = computed(() => this.courseService.courses().filter(c => c.status === CourseStatus.PendingReview)
  .map((course) => this.courseService.mapCourseItemToCourse(course)));


  startReview(courseId: string){
    this.courseReviewService.startReview(courseId).subscribe(
      (res) => {
        this.toReview(courseId);
      }
    );
  }

  toReview(courseId: string){
    this.router.navigate(['/course/', courseId, 'review']);
  }
}
