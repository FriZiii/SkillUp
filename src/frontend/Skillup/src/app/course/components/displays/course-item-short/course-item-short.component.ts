import { Component, computed, input, output } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { Course, CourseListItem } from '../../../models/course.model';
import { TruncatePipe } from '../../../../utils/pipes/truncate.pipe';
import { CoursePercentage } from '../../../models/user-progress.model';

@Component({
  selector: 'app-course-item-short',
  standalone: true,
  imports: [ButtonModule, RouterLink, TruncatePipe],
  templateUrl: './course-item-short.component.html',
  styleUrl: './course-item-short.component.css'
})
export class CourseItemShortComponent {
  course = input.required<Course>();
  editable = input<boolean>(false);
  rating = input<boolean>(false);
  moderator = input<boolean>(false);
  onReview = output<string>();
  onRating = output<string>();

  percentages = input<CoursePercentage[]>([])

  bought = input<boolean>(false);

  percentage = computed(() => this.percentages().find(x => x.courseId===this.course().id))

  review(courseId: string){
    this.onReview.emit(courseId);
  }

  addRating(courseId: string){
    this.onRating.emit(courseId);
  }
}
