import { Component, inject, input, OnInit, signal } from '@angular/core';
import { CoursesCarouselsComponent } from '../courses-carousels/courses-carousels.component';
import { CoursesService } from '../../services/course.service';
import { CourseDetail } from '../../models/course.model';

@Component({
  selector: 'app-course-detail',
  standalone: true,
  imports: [],
  templateUrl: './course-detail.component.html',
  styleUrl: './course-detail.component.css'
})
export class CourseDetailComponent implements OnInit {
  //Variables
  courseId = input.required<string>();
  course = signal<CourseDetail | null>(null);

  //Services
  courseService = inject(CoursesService);

  ngOnInit(): void {
    this.courseService.fetchCourseById(this.courseId()).subscribe({
      next: (res) => {
        this.course.set(res);
        console.log(this.course());
      }
    })
    
  }
}
