import { Component, inject, input, OnChanges, SimpleChanges } from '@angular/core';
import { ReviewComment } from '../../../../models/review.model';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { CourseReviewService } from '../../../../services/course-review.service';

@Component({
  selector: 'app-comment-dialog-for-instructor',
  standalone: true,
  imports: [CommonModule, ButtonModule],
  templateUrl: './comment-dialog-for-instructor.component.html',
  styleUrl: './comment-dialog-for-instructor.component.css'
})
export class CommentDialogForInstructorComponent {
  comments = input.required<ReviewComment[] | null>();
  latestComment = input.required<ReviewComment | null>();

  reviewService = inject(CourseReviewService);

  resolveComment(commentId: string){
    this.reviewService.resolveComment(commentId).subscribe(
      (res) => this.reviewService.latestReviewForCourse.set(res)
    );
  }
}
