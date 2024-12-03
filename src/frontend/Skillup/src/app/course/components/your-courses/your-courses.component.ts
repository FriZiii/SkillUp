import { Component, input, OnInit, signal } from '@angular/core';
import { Course } from '../../models/course.model';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { CourseItemShortComponent } from "../course-item-short/course-item-short.component";

@Component({
  selector: 'app-your-courses',
  standalone: true,
  imports: [ProgressSpinnerModule, CourseItemShortComponent],
  templateUrl: './your-courses.component.html',
  styleUrl: './your-courses.component.css'
})
export class YourCoursesComponent implements OnInit {
  userId = input<string>();
  loading = true;
  courses = signal<Course[]>([]);
  
  ngOnInit(): void {
    this.loading = false;
  }
}
