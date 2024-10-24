import { Component, computed, inject, input} from '@angular/core';
import { CourseItemComponent } from "../courses-list/course-item/course-item.component";
import { CoursesService } from '../../services/course.service';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CourseItemComponent],
  templateUrl: './courses.component.html',
  styleUrl: './courses.component.css'
})
export class CoursesComponent {
  //FromURL
  category = input.required<string>();
  subcategory = input.required<string>();

  //Services
  courseService = inject(CoursesService);

  //Variables
  courses = this.courseService.courses;
  coursesForCategory = computed(() =>  this.courses().filter(course => 
    course.category.name.toLowerCase() === this.category().toLowerCase() && 
    (this.subcategory().toLowerCase() === 'all' || 
     course.category.subcategory.name.toLowerCase() === this.subcategory().toLowerCase())
  )
);

  onClick(){
    console.log("cat" + this.category())
    console.log("sub" + this.subcategory())
    console.log(this.courses())
    console.log(this.coursesForCategory())
  }

}
