import { Component, input } from '@angular/core';
import { CourseCarouselItemComponent } from "./course-carousel-item/course-carousel-item.component";
import { CourseListItem } from '../../../models/course.model';
import { CarouselModule } from 'primeng/carousel';

@Component({
  selector: 'app-course-carousel',
  standalone: true,
  imports: [CourseCarouselItemComponent, CarouselModule],
  templateUrl: './course-carousel.component.html',
  styles: []
})
export class CourseCarouselComponent {
  courses = input.required<CourseListItem[]>();
}
