import { Component, computed, inject, OnInit } from '@angular/core';
import { FilterForCoursesComponent } from "../../filter-for-courses/filter-for-courses.component";
import { CoursesService } from '../../../services/course.service';
import { CourseListItem } from '../../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { AllCoursesCourseItemComponent } from "./all-courses-course-item/all-courses-course-item.component";
import { ScrollingModule } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-all-courses',
  standalone: true,
  imports: [FilterForCoursesComponent, ProgressSpinnerModule, AllCoursesCourseItemComponent, ScrollingModule],
  templateUrl: './all-courses.component.html',
  styleUrl: './all-courses.component.css'
})
export class AllCoursesComponent implements OnInit {
  //Services
  courseService = inject(CoursesService);

  //Variables
  courses = this.courseService.publishedCourses;
  filteredCourses: CourseListItem[] = [];

    ngOnInit(): void {
      this.courseService.courses$.subscribe((res) => {
        this.filteredCourses = res.map(x => this.courseService.mapCourseToCourseItem(x));
      })
    }

  onFilteredCourses(filteredCourses: CourseListItem[]){
    this.filteredCourses = filteredCourses;
  }
}
