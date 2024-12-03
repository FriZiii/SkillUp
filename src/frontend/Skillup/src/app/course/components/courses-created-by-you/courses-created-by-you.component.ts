import { Component, inject, input, OnInit, signal } from '@angular/core';
import { CoursesService } from '../../services/course.service';
import { Course } from '../../models/course.model';
import { CourseItemShortComponent } from "../course-item-short/course-item-short.component";

@Component({
  selector: 'app-courses-created-by-you',
  standalone: true,
  imports: [CourseItemShortComponent],
  templateUrl: './courses-created-by-you.component.html',
  styleUrl: './courses-created-by-you.component.css'
})
export class CoursesCreatedByYouComponent implements OnInit {
  //Variables
  authorId = input<string>();
  courses = signal<Course[]>([]);

  //Services
  courseService = inject(CoursesService);

  
  ngOnInit(): void {
    this.courseService.getCourseByAuthorId(this.authorId()!).subscribe(
      (res) => {
        this.courses.set(res);
        this.courses.set(this.courses().slice(0, 30));
      }
    )
  }
}
