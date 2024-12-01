import { Component, computed, inject, input} from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { CoursesService } from '../../services/course.service';
import { ProgressSpinnerModule } from 'primeng/progressspinner'; 
import { FormsModule } from '@angular/forms';
import { FilterService } from 'primeng/api';
import { FiltersComponent } from '../../../core/components/filters/filters.component';
import { CourseListItem } from '../../models/course.model';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CourseItemComponent, ProgressSpinnerModule, FormsModule, FiltersComponent],
  templateUrl: './courses-list.component.html',
  styleUrl: './courses-list.component.css'
})
export class CoursesListComponent {
  //FromURL
  category = input.required<string>();
  subcategory = input.required<string>();

  //Services
  courseService = inject(CoursesService);

  coursesForCategory = computed(() =>  {
    return this.courseService.getCoursesBySlug(this.category(), this.subcategory())
});
 filteredCourses: CourseListItem[] = [];

filterService = inject(FilterService);
filters = {
  title: '',
  level: null,
  author: ''
};

filter(courses: CourseListItem[]){
  this.filteredCourses = courses;
}
}
