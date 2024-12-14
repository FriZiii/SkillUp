import { Component, inject, input, OnInit, signal } from '@angular/core';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { PurchasedItemsService } from '../../services/purchasedItems.service';
import { CourseItemShortComponent } from '../displays/course-item-short/course-item-short.component';
import { UserProgressService } from '../../services/user-progress-service';
import { CoursePercentage } from '../../models/user-progress.model';

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
   userProgressService = inject(UserProgressService);
  
  //Variables
  loading = true;
  courses = this.courseService.purchasedCourses
  coursePercentages = signal<CoursePercentage[]>([]);
  
  ngOnInit(): void {
    this.loading = false;

    this.userProgressService.getPercentage().subscribe((res) => {
      this.coursePercentages.set(res);
      console.log(res)
    })
  }

  getPercentage(courseId: string){
    console.log( this.coursePercentages())
    return this.coursePercentages().find(p => p.courseId === courseId);
  }
}
