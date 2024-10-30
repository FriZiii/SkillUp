import { Component, computed, inject, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { CoursesService } from '../../services/course.service';
import { CategoryService } from '../../services/category.service';
import { CourseCarouselComponent } from './course-carousel/course-carousel.component';
import { FinanceService } from '../../../finance/finance.service';
import { Category } from '../../models/category.model';
import { CourseListItem } from '../../models/course.model';

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
  financeService = inject(FinanceService);

  //Variables
  courses = this.courseService.courses;
  categories = this.categoryService.categories;
  items = this.financeService.items;
  num = 0;
  categoriesWithCourses = computed(() => {
    const categories = this.categories();
    return categories.map(category => {
      const coursesForCategory = this.courses().filter(course => course.category.id === category.id);
      const coursesWithPrices = coursesForCategory.map(course => {
        const item = this.items().find(item => item.id === course.id);
        return {
          ...course,
          price: {
            amount: item?.price.amount ?? 0,
          },
        };
      });

      return { category, courses: coursesWithPrices };
    });
  });
}
