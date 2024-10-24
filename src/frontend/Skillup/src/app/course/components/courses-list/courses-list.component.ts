import { Component, inject, signal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { CourseListItem } from '../../models/course.model';
import { CoursesService } from '../../services/course.service';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
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
  courses = signal<CourseListItem[]>([]);
  categories = signal<Category[]>([]);


  ngOnInit(): void {
    this.courseService.getCourses().subscribe((data) => {
      this.courses.set(data);
    });
    this.categoryService.getCategories().subscribe((data) => {
      this.categories.set(data);
    });
  }

  getCoursesByCategory(categoryId: string){
    return this.courses().filter(course => course.category.id === categoryId);
  }
}
