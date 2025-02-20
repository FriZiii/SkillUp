import { Component, computed, inject, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { CourseCarouselComponent } from './course-carousel/course-carousel.component';
import { CoursesService } from '../../../services/course.service';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-courses-carousels',
  standalone: true,
  imports: [CarouselModule, ButtonModule, CourseCarouselComponent],
  templateUrl: './courses-carousels.component.html',
  styleUrl: './courses-carousels.component.css'
})
export class CoursesCarouselsComponent{
  //Services
  courseService = inject(CoursesService);
  categoryService = inject(CategoryService);

  //Variables
  categories = this.categoryService.categories;
  categoriesWithCourses = computed(() => {
    return this.categories().map(category => {
      return { category, courses: this.courseService.getCouresByCategoryId(category.id) };
    });
  });
}
