import { Component, computed, inject, input} from '@angular/core';
import { CourseItemComponent } from "../course-item/course-item.component";
import { CoursesService } from '../../services/course.service';
import { FinanceService } from '../../../finance/finance.service';

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
  financeService = inject(FinanceService);

  //Variables
  courses = this.courseService.courses;
  items = this.financeService.items;

  coursesForCategory = computed(() =>  this.courses().filter(course => 
    course.category.slug === this.category() && 
    (this.subcategory().toLowerCase() === 'all' || 
     course.category.subcategory.slug === this.subcategory())
  )
  .map(course => {
    const item = this.items().find(item => item.id === course.id);
      return {
        ...course,
        price:{
          amount:  item?.price.amount ?? 0,
        }
      };
  })
);
}
