import { Component, inject } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { CoursesService } from '../../services/course.service';
import { CategoryService } from '../../services/category.service';
import { CourseCarouselComponent } from './course-carousel/course-carousel.component';

@Component({
  selector: 'app-courses-carousels',
  standalone: true,
  imports: [CarouselModule, ButtonModule, CourseCarouselComponent],
  templateUrl: './courses-carousels.component.html',
  styleUrl: './courses-carousels.component.css'
})
export class CoursesCarouselsComponent {
  //Services
  courseService = inject(CoursesService);
  categoryService = inject(CategoryService);

  //Variables
  courses = this.courseService.courses;
  categories = this.categoryService.categories;

  getCoursesByCategory(categoryId: string){
    return this.courses().filter(course => course.category.id === categoryId);
  }
}
