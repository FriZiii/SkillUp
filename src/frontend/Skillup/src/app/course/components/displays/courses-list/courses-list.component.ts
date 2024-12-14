import { Component, computed, inject, input} from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { ProgressSpinnerModule } from 'primeng/progressspinner'; 
import { CoursesService } from '../../../services/course.service';

@Component({
  selector: 'app-courses',
  standalone: true,
  imports: [CourseItemComponent, ProgressSpinnerModule],
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
}
