import { Component, inject, input, OnInit, signal } from '@angular/core';
import { MenuModule } from 'primeng/menu';
import { CourseDetail } from '../../models/course.model';
import { Router, RouterModule, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-edit-course',
  standalone: true,
  imports: [MenuModule, RouterModule, RouterOutlet, ButtonModule],
  templateUrl: './edit-course.component.html',
  styleUrl: './edit-course.component.css'
})
export class EditCourseComponent{
//Variables
courseId = input.required<string>();
course = signal<CourseDetail | null>(null);
router = inject(Router);
}

