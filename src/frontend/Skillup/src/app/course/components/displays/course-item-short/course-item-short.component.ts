import { Component, input, output } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { Course } from '../../../models/course.model';
import { TruncatePipe } from '../../../../utils/pipes/truncate.pipe';

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


  review(courseId: string){
    this.onReview.emit(courseId);
  }

  addRating(courseId: string){
    this.onRating.emit(courseId);
  }
}