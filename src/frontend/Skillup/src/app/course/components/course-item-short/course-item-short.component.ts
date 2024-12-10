import { Component, input } from '@angular/core';
import { Course } from '../../models/course.model';
import { ButtonModule } from 'primeng/button';
import { RouterLink } from '@angular/router';
import { TruncatePipe } from "../../../utils/pipes/truncate.pipe";

@Component({
  selector: 'app-course-item-short',
  standalone: true,
  imports: [ButtonModule, RouterLink, TruncatePipe],
  templateUrl: './course-item-short.component.html',
  styleUrl: './course-item-short.component.css'
})
export class CourseItemShortComponent {
  course = input.required<Course>();
  editable = input<boolean>(false);
}
