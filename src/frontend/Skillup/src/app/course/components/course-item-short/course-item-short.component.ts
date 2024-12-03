import { Component, input } from '@angular/core';
import { Course } from '../../models/course.model';

@Component({
  selector: 'app-course-item-short',
  standalone: true,
  imports: [],
  templateUrl: './course-item-short.component.html',
  styleUrl: './course-item-short.component.css'
})
export class CourseItemShortComponent {
  course = input.required<Course>();
}
