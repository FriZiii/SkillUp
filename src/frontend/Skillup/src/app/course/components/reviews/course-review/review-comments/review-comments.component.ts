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
  courseId = input<string>();
  moderator = input<boolean>(false);
  
  reviewService = inject(CourseReviewService);
  
  //Variables
  latestReview = computed(() => this.reviewService.latestReviewForCourse());
  allReviews = computed(() => this.reviewService.allReviewsForCourse()?.filter(r => r.id != this.latestReview()?.id));
  latestComment = computed(() => this.latestReview()?.comments.find(comment => comment.courseElementId === this.elementId()) || null)
  comments = computed(() => this.allReviews()?.flatMap(review => review.comments).filter(comment => comment.courseElementId === this.elementId()) || null)
  newComment = '';

  addComment(){
    this.reviewService.addComment(this.latestReview()!.id, this.elementId(), this.newComment).subscribe(
      (res) => {
      this.reviewService.latestReviewForCourse.set(res);
      this.newComment = '';
    });
  }

  deleteComment(commentId: string){
    this.reviewService.deleteComment(commentId).subscribe(
      (res) => {
        this.reviewService.getLatestReviewByCourse(this.courseId()!).subscribe((res) => {
          this.reviewService.latestReviewForCourse.set(res);
        })
      });
  }
}
