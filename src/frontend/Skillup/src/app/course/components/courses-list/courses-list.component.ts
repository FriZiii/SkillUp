import { Component, computed, inject, input} from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { CoursesService } from '../../services/course.service';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CourseItemComponent],
  templateUrl: './courses-list.component.html',
  styleUrl: './courses-list.component.css'
})
export class CoursesListComponent {
  //FromURL
  category = input.required<string>();
  subcategory = input.required<string>();

  //Services
  courseService = inject(CoursesService);

  //Variables
  courses = this.courseService.courses;
  coursesForCategory = computed(() =>  this.courses().filter(course => 
    course.category.slug === this.category() && 
    (this.subcategory().toLowerCase() === 'all' || 
     course.category.subcategory.slug === this.subcategory())
  )
);

  onClick(){
    console.log("cat" + this.category())
    console.log("sub" + this.subcategory())
    console.log(this.courses())
    console.log(this.coursesForCategory())
  }

}
