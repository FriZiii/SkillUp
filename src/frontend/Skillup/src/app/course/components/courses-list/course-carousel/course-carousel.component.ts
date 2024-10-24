import { Component, input } from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { CourseListItem } from '../../../models/course.model';
import { CarouselModule } from 'primeng/carousel';

@Component({
  selector: 'app-course-carousel',
  standalone: true,
  imports: [CourseItemComponent, CarouselModule],
  templateUrl: './course-carousel.component.html',
  styles: []
})
export class CourseCarouselComponent {
  courses = input.required<CourseListItem[]>();
}
