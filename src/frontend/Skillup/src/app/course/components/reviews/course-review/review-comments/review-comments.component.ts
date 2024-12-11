import { AfterViewInit, Component, computed, inject, input, OnInit, signal } from '@angular/core';
import { Review, ReviewComment } from '../../../../models/review.model';
import { CourseReviewService } from '../../../../services/course-review.service';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-review-comments',
  standalone: true,
  imports: [InputTextModule, ButtonModule, FormsModule],
  templateUrl: './review-comments.component.html',
  styleUrl: './review-comments.component.css'
})
export class ReviewCommentsComponent {
  elementId = input.required<string>();
  courseId = input.required<string>();
  
  reviewService = inject(CourseReviewService);
  
  //Variables
  allReviews = computed(() => this.reviewService.allReviewsForCourse());
  latestReview = computed(() => this.reviewService.latestReviewForCourse());
  latestComment = computed(() => this.latestReview()?.comments.find(comment => comment.courseElementId === this.elementId()) || null)
  comments = computed(() => this.allReviews()?.flatMap(review => review.comments).filter(comment => comment.courseElementId === this.elementId() && comment.id !== this.latestComment()?.id) || null)
  newComment = '';
  //latestComment = signal<ReviewComment | null>(null);
  //comments = signal<ReviewComment[] | null>(null)

  addComment(){
    this.reviewService.addComment(this.latestReview()!.id, this.elementId(), this.newComment).subscribe(
      (res) => {
      this.reviewService.latestReviewForCourse.set(res);
    });
  }

  deleteComment(commentId: string){
    this.reviewService.deleteComment(commentId).subscribe(
      (res) => {
      });
  }
}
