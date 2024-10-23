import { Component, Input, input } from '@angular/core';
import { CourseListItem } from '../../../../models/course.model';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-course-carousel-item',
  standalone: true,
  imports: [ButtonModule],
  templateUrl: './course-carousel-item.component.html',
  styles: []
})
export class CourseCarouselItemComponent {
  @Input() course!: CourseListItem;
}
