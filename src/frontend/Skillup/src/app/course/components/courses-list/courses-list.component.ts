import { Component, inject } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { CoursesService } from '../../services/course.service';
import { CategoryService } from '../../services/category.service';
import { CourseCarouselComponent } from './course-carousel/course-carousel.component';

@Component({
  selector: 'app-courses-list',
  standalone: true,
  imports: [CarouselModule, ButtonModule, CourseCarouselComponent],
  templateUrl: './courses-list.component.html',
  styleUrl: './courses-list.component.css'
})
export class CoursesListComponent {
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
