import { Component, computed, inject, input, OnChanges, SimpleChanges} from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { ProgressSpinnerModule } from 'primeng/progressspinner'; 
import { CoursesService } from '../../../services/course.service';
import { FilterForCoursesComponent } from "../../filter-for-courses/filter-for-courses.component";
import { CourseListItem } from '../../../models/course.model';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CourseItemComponent, ProgressSpinnerModule, FilterForCoursesComponent],
  templateUrl: './courses-list.component.html',
  styleUrl: './courses-list.component.css'
})
export class CoursesListComponent implements OnChanges {
 
  //FromURL
  category = input.required<string>();
  subcategory = input.required<string>();

  //Services
  courseService = inject(CoursesService);

  //Variables
  allCoursesForCategory = computed(() =>  {
    return this.courseService.getCoursesBySlug(this.category(), this.subcategory())
  });
  filteredCoursesForCategory = computed(() =>  {
    return this.courseService.getCoursesBySlug(this.category(), this.subcategory())
  });

ngOnChanges(changes: SimpleChanges): void {
  if((changes['category'] && changes['category'].currentValue) || (changes['subcategory'] && changes['subcategory'].currentValue )){
    this.allCoursesForCategory = computed(() =>  {
      return this.courseService.getCoursesBySlug(this.category(), this.subcategory())
  });
  this.filteredCoursesForCategory = computed(() =>  {
    return this.courseService.getCoursesBySlug(this.category(), this.subcategory())
  });
  }
}

onFilteredCourses(filteredCourses: CourseListItem[]){
  this.filteredCoursesForCategory = computed(() =>  filteredCourses);
}
}
