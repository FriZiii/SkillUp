import { Component, input } from '@angular/core';
import { UserRatingDetail } from '../../../../models/rating.model';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-course-rating',
  standalone: true,
  imports: [
    RatingModule,
    FormsModule,
  CommonModule, RouterModule],
  templateUrl: './course-rating.component.html',
  styleUrl: './course-rating.component.css'
})
export class CourseRatingComponent {
  rating = input.required<UserRatingDetail>()
}
