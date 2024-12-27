import { Component, computed, inject, input, OnChanges, OnInit, signal, SimpleChanges} from '@angular/core';
import { ProgressSpinnerModule } from 'primeng/progressspinner'; 
import { CoursesService } from '../../../services/course.service';
import { FilterForCoursesComponent } from "../../filter-for-courses/filter-for-courses.component";
import { CourseListItem } from '../../../models/course.model';
import { AllCoursesCourseItemComponent } from "../all-courses/all-courses-course-item/all-courses-course-item.component";
import { ScrollingModule } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [ProgressSpinnerModule, FilterForCoursesComponent, AllCoursesCourseItemComponent, ScrollingModule],
  templateUrl: './courses-list.component.html',
  styleUrl: './courses-list.component.css'
})
export class CoursesListComponent implements OnChanges, OnInit {
  //FromURL
  category = input.required<string>();
  subcategory = input.required<string>();

  //Services
  courseService = inject(CoursesService);

  //Variables
  loading = true;
  allCoursesForCategory = signal<CourseListItem[]>([]);
  filteredCoursesForCategory = signal<CourseListItem[]>([]);

  ngOnInit(): void {
    console.log('init')
    this.courseService.getCoursesBySlug(this.category(), this.subcategory()).subscribe((res) => {
      this.allCoursesForCategory.set(res);
      this.filteredCoursesForCategory.set(res);
      this.loading = false;
    })
  }
 

ngOnChanges(changes: SimpleChanges): void {
  if((changes['category'] && changes['category'].currentValue) || (changes['subcategory'] && changes['subcategory'].currentValue )){
    console.log('changes')
    this.courseService.getCoursesBySlug(this.category(), this.subcategory()).subscribe((res) => {
      this.allCoursesForCategory.set(res);
      this.filteredCoursesForCategory.set(res);
      this.loading = false;
    })
  }
}

onFilteredCourses(filteredCourses: CourseListItem[]){
  this.filteredCoursesForCategory.set(filteredCourses);
}
}
