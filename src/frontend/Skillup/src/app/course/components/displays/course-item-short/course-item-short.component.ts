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
  moderator = input<boolean>(false);
  onReview = output<string>();

  bought = input<boolean>(false);

  review(courseId: string){
    this.onReview.emit(courseId);
  }
}
