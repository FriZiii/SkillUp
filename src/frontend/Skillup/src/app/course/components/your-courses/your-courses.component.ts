import { Component, inject, input, OnInit, signal } from '@angular/core';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PurchasedItemsService } from '../../services/purchasedItems.service';
import { CourseItemShortComponent } from '../displays/course-item-short/course-item-short.component';

@Component({
  selector: 'app-your-courses',
  standalone: true,
  imports: [ProgressSpinnerModule, CourseItemShortComponent],
  templateUrl: './your-courses.component.html',
  styleUrl: './your-courses.component.css'
})
export class YourCoursesComponent implements OnInit {
   //From URL
   userId = input.required<string>();

   //Services
   courseService = inject(PurchasedItemsService);
  
  //Variables
  loading = true;
  courses = this.courseService.purchasedCourses
  
  ngOnInit(): void {
    this.loading = false;
  }
}
