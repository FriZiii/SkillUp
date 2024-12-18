import { Component, computed, inject, input, output } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { Course } from '../../../models/course.model';
import { CoursePercentage } from '../../../models/user-progress.model';
import { RatingModule } from 'primeng/rating';
import { CoursesService } from '../../../services/course.service';
import { FormsModule } from '@angular/forms';
import { CircleProgressComponent } from "../../../../utils/components/circle-progress/circle-progress.component";
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-student-course-item',
  standalone: true,
  imports: [RatingModule, FormsModule, CircleProgressComponent, RouterModule],
  templateUrl: './student-course-item.component.html',
  styleUrl: './student-course-item.component.css'
})
export class StudentCourseItemComponent {
  course = input.required<Course>();
  onRating = output<string>();
  percentages = input<CoursePercentage[]>([])
  //Services
  courseService = inject(CoursesService);

  //Variables
  courseListItem = computed(() => this.courseService.mapCourseToCourseItem(this.course()));
  percentage = computed(() => this.percentages().find(x => x.courseId===this.course().id))

  addRating(courseId: string){
    this.onRating.emit(courseId);
  }
}
