import { Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { CourseListItem } from '../../models/course.model';
import { CourseCarouselItemComponent } from "../courses-list/course-carousel/course-carousel-item/course-carousel-item.component";
import { CoursesService } from '../../services/course.service';
import { CoursesListComponent } from '../courses-list/courses-list.component';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CourseCarouselItemComponent],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css'
})
export class CoursesComponent implements OnInit {
  //FromURL
  category = input.required<string>();
  subcategory = input.required<string>();

  //Services
  courseService = inject(CoursesService);

  //Variables
  courses = signal<CourseListItem[]>([]);
  coursesForCategory = computed(() =>  this.courses().filter(course => 
    course.category.name.toLowerCase() === this.category().toLowerCase() && 
    (this.subcategory().toLowerCase() === 'all' || 
     course.category.subcategory.name.toLowerCase() === this.subcategory().toLowerCase())
  )
);

  ngOnInit(): void {
    this.courseService.getCourses().subscribe((data) => {
      this.courses.set(data);
    });
  }

  onClick(){
    console.log("cat" + this.category())
    console.log("sub" + this.subcategory())
    console.log(this.courses())
    console.log(this.coursesForCategory())
  }

}
