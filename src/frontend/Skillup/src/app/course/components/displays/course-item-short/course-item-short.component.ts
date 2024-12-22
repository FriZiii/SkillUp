import { Component, computed, inject, input, output } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { Course } from '../../../models/course.model';
import { TruncatePipe } from '../../../../utils/pipes/truncate.pipe';
import { RatingModule } from 'primeng/rating';
import { CourseRatingService } from '../../../services/course-rating.service';
import { FormsModule } from '@angular/forms';
import { CoursesService } from '../../../services/course.service';

@Component({
  selector: 'app-course-item-short',
  standalone: true,
  imports: [ButtonModule, RouterLink, TruncatePipe, RatingModule, FormsModule],
  templateUrl: './course-item-short.component.html',
  styleUrl: './course-item-short.component.css'
})
export class CourseItemShortComponent {
  course = input.required<Course>();
  editable = input<boolean>(false);
  moderator = input<boolean>(false);
  onReview = output<string>();

  review(courseId: string){
    this.onReview.emit(courseId);
  }

  //Services
  courseService = inject(CoursesService);
  courseListItem = computed(() => this.courseService.mapCourseToCourseItem(this.course()));
}
