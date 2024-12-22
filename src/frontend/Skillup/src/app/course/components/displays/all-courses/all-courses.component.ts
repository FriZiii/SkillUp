import { Component, computed, inject } from '@angular/core';
import { FilterForCoursesComponent } from "../../filter-for-courses/filter-for-courses.component";
import { CoursesService } from '../../../services/course.service';
import { CourseListItem } from '../../../models/course.model';
import { CourseItemComponent } from "../course-item/course-item.component";
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AllCoursesCourseItemComponent } from "./all-courses-course-item/all-courses-course-item.component";

@Component({
  selector: 'app-all-courses',
  standalone: true,
  imports: [FilterForCoursesComponent, CourseItemComponent, ProgressSpinnerModule, AllCoursesCourseItemComponent],
  templateUrl: './all-courses.component.html',
  styleUrl: './all-courses.component.css'
})
export class AllCoursesComponent {
  //Services
  courseService = inject(CoursesService);

  //Variables
  courses = this.courseService.publishedCourses;
    filteredCourses = computed(() =>  this.courseService.publishedCourses());

  onFilteredCourses(filteredCourses: CourseListItem[]){
    this.filteredCourses = computed(() =>  filteredCourses);
  }
}
